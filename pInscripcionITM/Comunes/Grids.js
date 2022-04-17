function LlenarGridControlador(sURL, Comando, lstParametros, TablaLlenar) {
    var DatosGrid = {
        Comando: Comando,
        lstParametros
    }
    var promise;
    promise = $.ajax({
        type: "POST",
        url: sURL,
        contentType: "application/json",
        data: JSON.stringify(DatosGrid),
        dataType: "json",
        success: function (respuesta) {
            var columns = [];
            columnNames = Object.keys(respuesta[0]);
            for (var i in columnNames) {
                columns.push({
                    data: columnNames[i],
                    title: columnNames[i]
                });
            }

            $(TablaLlenar).DataTable({
                data: respuesta,
                columns: columns,
                destroy: true
            });
        }
    });
    return promise;
}