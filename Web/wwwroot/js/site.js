$(document).ready(function () {
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
});