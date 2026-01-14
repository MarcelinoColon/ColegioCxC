$(document).ready(function () {

    // ==============================
    // 1. TUTORES (SELECT2)
    // ==============================
    if ($('#selectTutores').length > 0) {
        $('#selectTutores').select2({
            placeholder: "Escribe para buscar un tutor...",
            minimumInputLength: 2,
            ajax: {
                url: '/Tutor/Buscar',
                dataType: 'json',
                delay: 250,
                data: function (params) {
                    return { term: params.term };
                },
                processResults: function (data) {
                    return { results: data.results };
                }
            }
        });
    }

    // ==============================
    // 2. ESTUDIANTES
    // ==============================
    var contenedorEst = $('#contenedor-estudiantes');

    if (contenedorEst.length > 0) {
        var urlObtenerEstudiantes = contenedorEst.data('url');

        $.get(urlObtenerEstudiantes, function (data) {
            contenedorEst.empty();

            if (data.results && data.results.length > 0) {

                contenedorEst.append(`
                    <div class="form-check border-bottom mb-2 pb-2">
                        <input class="form-check-input" type="checkbox" id="chk-todos">
                        <label class="form-check-label fw-bold">Seleccionar todos</label>
                    </div>
                `);

                $.each(data.results, function (index, est) {
                    contenedorEst.append(`
                        <div class="form-check">
                            <input class="form-check-input chk-estudiante" 
                                   type="checkbox" 
                                   value="${est.id}">
                            <label class="form-check-label">${est.text}</label>
                        </div>
                    `);
                });
            }
        });

        contenedorEst.on('change', '#chk-todos', function () {
            contenedorEst.find('.chk-estudiante').prop('checked', this.checked);
        });
    }

    // ==============================
    // 3. MODAL PAY CARGO  ✅ (CORREGIDO)
    // ==============================
    $(document).on("click", ".btn-pay-cargo", function () {

        var cargoId = $(this).data("cargo-id");
        var concepto = $(this).data("cargo-concepto");
        var estudianteId = $(this).data("estudiante-id");
        var estudianteNombre = $(this).data("estudiante-nombre");
        var saldoDisponible = $(this).data("saldo");

        $.get("/Cargo/pagar-cargo", {
            cargoId: cargoId,
            concepto: concepto,
            estudianteId: estudianteId,
            estudianteNombre: estudianteNombre,
            saldoDisponible: saldoDisponible
        }, function (html) {

            $("#modalPayCargoBody").html(html);

            var modal = new bootstrap.Modal(
                document.getElementById("modalPayCargo")
            );

            modal.show();
        });
    });

    // ==============================
    // 4. CONCEPTOS
    // ==============================
    var selectConcepto = $('#select-concepto');

    if (selectConcepto.length > 0) {
        var urlObtenerConceptos = selectConcepto.data('url');

        $.get(urlObtenerConceptos, function (data) {
            selectConcepto
                .empty()
                .append('<option value="">Seleccione</option>');

            if (data.results && data.results.length > 0) {
                $.each(data.results, function (i, item) {
                    selectConcepto.append(
                        `<option value="${item.id}">${item.text}</option>`
                    );
                });
            }
        });
    }

});
