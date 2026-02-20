use chrono::{FixedOffset, Utc};
use serde::{Deserialize, Serialize};
use std::ffi::{CStr, c_char, c_int};
use tera::{Context, Tera};

#[repr(C)]
pub struct PdfResponse {
    pub success: c_int,             // 1 para OK, 0 para Error
    pub data: *mut u8,              // Puntero al stream de bytes del PDF
    pub data_length: usize,         // Tamaño del PDF
    pub error_message: *mut c_char, // Puntero al texto del error
}

#[derive(Deserialize, Serialize, Debug)]
struct Articulo {
    id: i32,
    estudiante: String,
    cargo: i32,
    pendiente: f64,
    abonado: f64,
    f_pagado: String,
    f_vencimiento: String,
    estado: String,
}
#[derive(Deserialize, Serialize, Debug)]
struct DatosReporte {
    titulo: String,
    items: Vec<Articulo>,
}

// #[unsafe(no_mangle)]
// pub unsafe extern "C" fn generate_excel_report(json: *const c_char) -> PdfResponse {
//     let mut response: PdfResponse = PdfResponse {
//         success: 0,
//         data: std::ptr::null_mut(),
//         data_length: 0,
//         error_message: std::ptr::null_mut(),
//     };

//     if json.is_null() {
//         response = PdfResponse {
//             success: 0,
//             data: std::ptr::null_mut(),
//             data_length: 0,
//             error_message: std::ffi::CString::new("Puntero nulo recibido")
//                 .unwrap()
//                 .into_raw(),
//             ..response
//         };
//         return response;
//     }
//     let _incoming_json = incoming_json(unsafe {
//         CStr::from_ptr(json)
//             .to_str()
//             .expect("No es una cadena valida")
//     });
//     let path: &Path = Path::new("./assets/templates/Template.xltx");
//     let mut book: Spreadsheet = reader::xlsx::read(path).expect("Error al abrir la plantilla");
//     let sheet: &mut Worksheet = book
//         .get_sheet_by_name_mut("Sheet1")
//         .expect("No se encotro la hoja");
//     sheet.get_cell_mut("A12").set_value(&_incoming_json.titulo);

//     for (i, articulo) in _incoming_json.items.iter().enumerate() {
//         let row = 13 + i as u32;

//         sheet
//             .get_cell_mut((1, row))
//             .set_value(format!("{}", articulo.id));
//         sheet
//             .get_cell_mut((2, row))
//             .set_value(articulo.producto.to_string());
//         sheet
//             .get_cell_mut((3, row))
//             .set_value(format!("{}", articulo.cantidad));
//         sheet
//             .get_cell_mut((4, row))
//             .set_value(format!("{}", articulo.precio));
//     }

//     //preparando para exportar
//     let output_filename = format!("output/reporte_{}.xlsx", get_date());
//     let _output_path = Path::new(&output_filename);
//     writer::xlsx::write(&book, _output_path).expect("Error al guardar el archivo");
//     println!("Done");
//     response = PdfResponse {
//         success: 1,
//         data: std::ptr::null_mut(),
//         data_length: 0,
//         error_message: std::ptr::null_mut(),
//     };
//     return response;
// }

#[unsafe(no_mangle)]
pub unsafe extern "C" fn generate_pdf_report(json: *const c_char) -> PdfResponse {
    let mut response: PdfResponse = PdfResponse {
        success: 0,
        data: std::ptr::null_mut(),
        data_length: 0,
        error_message: std::ptr::null_mut(),
    };

    if json.is_null() {
        response = PdfResponse {
            success: 0,
            data: std::ptr::null_mut(),
            data_length: 0,
            error_message: std::ffi::CString::new("Json nulo recibido")
                .unwrap()
                .into_raw(),
            ..response
        };
        return response;
    }
    //######################################################################################################################
    //######################################################################################################################
    // Parsear el JSON de entrada
    let json_str = match unsafe { CStr::from_ptr(json).to_str() } {
        Ok(s) => s,
        Err(e) => {
            response.success = 0;
            response.error_message =
                std::ffi::CString::new(format!("Error al convertir el JSON: {}", e))
                    .unwrap()
                    .into_raw();
            return response;
        }
    };

    let mut _json: DatosReporte = match incoming_json(json_str) {
        Ok(data) => data,
        Err(e) => {
            response.success = 0;
            response.error_message =
                std::ffi::CString::new(format!("Error al parsear el JSON: {}", e))
                    .unwrap()
                    .into_raw();
            return response;
        }
    };

    //######################################################################################################################
    //######################################################################################################################
    // Cargar la plantilla HTML
    let _relative_path = std::path::Path::new("assets")
        .join("templates")
        .join("Template.html");

    let _template_content: String = match std::fs::read_to_string(&_relative_path) {
        Ok(content) => content,
        Err(e) => {
            let env_path = std::env::current_dir()
                .map(|p| p.join(&_relative_path))
                .unwrap_or_else(|_| _relative_path.to_path_buf());
            response = PdfResponse {
                success: 0,
                data: std::ptr::null_mut(),
                data_length: 0,
                error_message: std::ffi::CString::new(format!(
                    "Error al leer el archivo de plantilla. Error:{}, ruta{:?}",
                    e, env_path
                ))
                .unwrap()
                .into_raw(),
                ..response
            };
            return response;
        }
    };
    //######################################################################################################################
    //######################################################################################################################
    // crear el motor de plantillas Tera y cargar la plantilla

    let mut _tera = Tera::default();

    match _tera.add_raw_template("Template", &_template_content) {
        Ok(content) => content,
        Err(e) => {
            response = PdfResponse {
                success: 0,
                data: std::ptr::null_mut(),
                data_length: 0,
                error_message: std::ffi::CString::new(format!(
                    "Error al cargar la plantilla en Tera: {}",
                    e
                ))
                .unwrap()
                .into_raw(),
                ..response
            };
            return response;
        }
    };

    let mut _context: Context = tera::Context::new();
    _context.insert("titulo", &_json.titulo);
    _context.insert("items", &_json.items);
    _context.insert("fecha", &get_date());
    _context.insert("total_calculado", &123.45);

    //######################################################################################################################
    //######################################################################################################################
    //renderizar la plantilla con los datos
    let _rendered = match _tera.render("Template", &_context) {
        Ok(r) => r,
        Err(e) => {
            response = PdfResponse {
                success: 0,
                data: std::ptr::null_mut(),
                data_length: 0,
                error_message: std::ffi::CString::new(format!(
                    "Error al renderizar la plantilla: {}",
                    e
                ))
                .unwrap()
                .into_raw(),
                ..response
            };
            return response;
        }
    };

    //######################################################################################################################
    //######################################################################################################################
    // Guardar el HTML renderizado en un archivo (opcional, para depuración)
    match std::fs::write(format!("output/reporte_{}.html", get_date()), &_rendered) {
        Ok(_) => (),
        Err(e) => {
            response = PdfResponse {
                success: 0,
                data: std::ptr::null_mut(),
                data_length: 0,
                error_message: std::ffi::CString::new(format!(
                    "Error al guardar el archivo HTML: {}",
                    e
                ))
                .unwrap()
                .into_raw(),
                ..response
            };
            return response;
        }
    };
    //######################################################################################################################
    //######################################################################################################################
    let mut _vec_data = _rendered.into_bytes();
    let length = _vec_data.len();
    let ptr = _vec_data.as_mut_ptr();

    std::mem::forget(_vec_data); // Evitar que Rust libere la memoria del vector
    response = PdfResponse {
        success: 1,
        data: ptr,
        data_length: length,
        error_message: std::ptr::null_mut(),
    };
    return response;
}

#[unsafe(no_mangle)]
pub unsafe extern "C" fn free_report_data(ptr: *mut u8, length: usize) -> c_int {
    if !ptr.is_null() {
        let _ = unsafe { Vec::from_raw_parts(ptr, length, length) }; // Reconstruir el vector para que Rust pueda liberar la memoria
        return 1; // Éxito
    };
    return 0; // Error 
}
#[unsafe(no_mangle)]
pub unsafe extern "C" fn free_error_message(ptr: *mut c_char) -> c_int {
    if !ptr.is_null() {
        let _ = unsafe { std::ffi::CString::from_raw(ptr) }; // Reconstruir el CString para que Rust pueda liberar la memoria
        return 1; // Éxito
    };
    return 0; // Error 
}

fn get_date() -> String {
    let offset = FixedOffset::west_opt(4 * 3600).expect("Ofsset Invalido");
    let dt = Utc::now().with_timezone(&offset);
    return format!("{}", dt.format("%Y-%m-%d").to_string());
}
fn incoming_json(json: &str) -> Result<DatosReporte, serde_json::Error> {
    return serde_json::from_str(json);
}
