function LlenarComboControlador(sURL, Comando, lstParametros, ComboLlenar) {
    var DatosCombo = {
        Comando: Comando,
        lstParametros
    }
    var promise;
    promise = $.ajax({
        type: "POST",
        url: sURL,
        contentType: "application/json",
        data: JSON.stringify(DatosCombo),
        dataType: "json",
        success: function (respuesta) {
            for (var op = 0; op < respuesta.length; op++) {
                $(ComboLlenar).append('<option value=' + respuesta[op].Valor + '>' + respuesta[op].Texto + '</option>');
            }
        }
    });
    return promise;
}