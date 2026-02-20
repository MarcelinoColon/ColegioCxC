using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Aplicacion.Interfaces.UseCase;
using Aplicacion.Pago.DTOs;
using Aplicacion.Reportes.DTOs;

namespace Aplicacion.Reportes.CasosDeUso
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PdfResponse
    {
        public int success;
        public IntPtr data;
        public UIntPtr dataLength;
        public IntPtr errorMessage;
    }

    public partial class Generador : ICreateReport
    {
        private const string DllName = "rust_reporter";
        [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial PdfResponse generate_pdf_report(string json);

        [LibraryImport(DllName)]
        public static partial int free_report_data(IntPtr prt, UIntPtr length);

        [LibraryImport(DllName)]
        public static partial int free_error_message(IntPtr prt);

        public Generador()
        {
        }

        public void GeneratePdfReportAsync()
        {
            ReporteDto _ = new()
            {
                titulo = "Resumen de pago Estudiantes",
                items = [

                   new()
        {
            id = 4829,
            estudiante = "Juan Pérez",
            cargo = 5000F,
            pendiente = 0.0F,
            f_pagado = "2026-01-05",
            f_vencimiento = "2026-01-10",
            estado = "Saldado"
        },
        new()
        {
            id = 9102,
            estudiante = "Juan Pérez",
            cargo = 5000F,
            pendiente = 1500.0F,
            f_pagado = "2026-02-08",
            f_vencimiento = "2026-02-10",
            estado = "Pendiente"
        },
        new()
        {
            id = 3456,
            estudiante = "Juan Pérez",
            cargo = 5000F,
            pendiente = 5000.0F,
            f_pagado = "N/A",
            f_vencimiento = "2026-03-10",
            estado = "Pendiente"
        }
                ]

            };
            string jsonDto = System.Text.Json.JsonSerializer.Serialize(_);
            PdfResponse result = generate_pdf_report(jsonDto);
            if (result.success == 0)
            {
                string? errorMessage = Marshal.PtrToStringAnsi(result.errorMessage);
                if (errorMessage != null)
                    free_error_message(result.errorMessage);
                throw new Exception(errorMessage);
            }
        }


    }
}
