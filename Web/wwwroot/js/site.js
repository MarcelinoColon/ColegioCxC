$(document).ready(function () {
    // ==========================================
    // 1. LÓGICA DE TUTORES (SELECT2)
    // ==========================================
    $('#selectTutores').select2({
        placeholder: "Escribe para buscar un tutor...",
        minimumInputLength: 2, // Espera a que escriba 2 letras para no saturar
        ajax: {
            url: '/Tutor/Buscar', // Tu endpoint del paso 1
            dataType: 'json',
            delay: 250, // "Debounce": Espera 250ms a que deje de escribir para buscar
            data: function (params) {
                return {
                    term: params.term // Envía lo que escribe como variable 'term'
                };
            },
            processResults: function (data) {
                // Select2 espera recibir { results: [] }
                return {
                    results: data.results
                };
            }
        }
    });

    // ==========================================
    // 2. LÓGICA DE ESTUDIANTES (CHECKBOXES)
    // ==========================================
    var contenedorEst = $('#contenedor-estudiantes');

    if (contenedorEst.length > 0) {
        var urlObtenerEstudiantes = contenedorEst.data('url');

        $.ajax({
            url: urlObtenerEstudiantes,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                contenedorEst.empty();

                if (data.results && data.results.length > 0) {
                    // A. Checkbox "Seleccionar todos"
                    var selectAllHtml = `
                        <div class="form-check border-bottom mb-2 pb-2">
                            <input class="form-check-input" type="checkbox" id="chk-todos">
                            <label class="form-check-label fw-bold" for="chk-todos">
                                Seleccionar todos
                            </label>
                        </div>
                    `;
                    contenedorEst.append(selectAllHtml);

                    // B. Lista de estudiantes
                    $.each(data.results, function (index, est) {
                        var checkboxHtml = `
                            <div class="form-check">
                                <input class="form-check-input chk-estudiante" type="checkbox" 
                                       name="EstudiantesIds" 
                                       value="${est.id}" 
                                       id="est_${est.id}">
                                <label class="form-check-label" for="est_${est.id}">
                                    ${est.text}
                                </label>
                            </div>
                        `;
                        contenedorEst.append(checkboxHtml);
                    });

                    // C. Evento click para "Seleccionar todos"
                    contenedorEst.on('change', '#chk-todos', function () {
                        var estaMarcado = $(this).is(':checked');
                        contenedorEst.find('.chk-estudiante').prop('checked', estaMarcado);
                    });

                } else {
                    contenedorEst.html('<p class="text-muted">No hay estudiantes registrados.</p>');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error Estudiantes:', error);
                contenedorEst.html('<p class="text-danger">Error al conectar con el servidor.</p>');
            }
        });
    }

    // ==========================================
    // 3. LÓGICA DE CONCEPTOS (DROPDOWN)
    // ==========================================
    var selectConcepto = $('#select-concepto');

    if (selectConcepto.length > 0) {
        var urlObtenerConceptos = selectConcepto.data('url');

        $.ajax({
            url: urlObtenerConceptos,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                selectConcepto.empty();
                selectConcepto.append('<option value="">-- Seleccione un concepto --</option>');

                if (data.results && data.results.length > 0) {
                    $.each(data.results, function (index, item) {
                        selectConcepto.append(
                            $('<option></option>').val(item.id).text(item.text)
                        );
                    });
                } else {
                    selectConcepto.append('<option value="">No hay conceptos disponibles</option>');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error Conceptos:', error);
                selectConcepto.empty();
                selectConcepto.append('<option value="">Error al cargar datos</option>');
            }
        });
    }

});