$(document).ready(function () {
    //Invoca el llenado del combo de tipo telefono
    LlenarComboProgramas();
    OcultarBotones();
    $("#btnAgregar").click(function () {
        GrabarPrograma();
    });
    $("#btnCancelar").click(function () {
        OcultarBotones();
    });
    $("#btnEliminar").click(function () {
        oTabla.row('.selected').remove().draw(false);
        $("#dvGrabar").show();
        $("#dvModificarEliminar").hide();
    });

    $("#btnModificar").click(function () {
        GrabarPrograma();
        oTabla.row('.selected').remove().draw(false);
        $("#dvGrabar").show();
        $("#dvModificarEliminar").hide();
    });

    $("#btnLimpiar").click(function () {
        $("#btnGrabarPrograma").show();
        $("#btnModificar").hide();
        $("#txtDocumento").val("");
        $("#txtNombres").val("");
        $("#txtColegio").val("");
        $("#txtEstratoVivienda").val("");
        $("#txtEstratoColegio").val("");
    });

    $("#btnGrabarPrograma").click(function () {
        GrabarProgramaBd();
    });

    var oTabla = $("#tblProgramas").DataTable();
    //Seleccionar la fila de la tabla
    $('#tblProgramas tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            OcultarBotones();
        }
        else {
            oTabla.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            MostrarBotones();

            var DatosFila = oTabla.row('.selected').data();
            $("#lstPrograma").val(DatosFila[0]);
            $("#lstOpcion").val(DatosFila[2]);
        }
    });
});

function OcultarBotones() {
    $("#dvGrabar").show();
    $("#dvModificarEliminar").hide();
}

function MostrarBotones() {
    $("#dvGrabar").hide();
    $("#dvModificarEliminar").show();
}

function LlenarComboProgramas() {
    LlenarComboControlador("../Comunes/ControladorCombos.ashx", "TIPOPROGRAMAS", null, "#lstPrograma");
}

function GrabarPrograma() {
    var oTabla = $("#tblProgramas").DataTable();

    //Leer los datos para grabar
    var IdPrograma = $("#lstPrograma").val();
    var Programa = $("#lstPrograma option:selected").html();
    var Opcion = ($("#lstOpcion").val());

    oTabla.row.add([
        IdPrograma,
        Programa,
        Opcion
    ]).draw(false);
}


function GrabarProgramaBd() {
    //Capturar datos del cliente
    var Documento = $("#txtDocumento").val();
    var Nombre = $("#txtNombres").val();
    var Colegio = $("#txtColegio").val();
    var EstratoVivienda = $("#txtEstratoVivienda").val();
    var EstratoColegio = $("#txtEstratoColegio").val();
    var Comando = "GRABAR";

    //Capturar programas del cliente
    //Para el detalle, hay que recorrer la tabla de los detalles y crear un objeto json
    var oTabla = $('#tblProgramas').DataTable();
    var fieldNames = [], lstProgramas = [];
    oTabla.settings().columns()[0].forEach(function (index) {
        fieldNames.push($(oTabla.column(index).header()).text().replace(/ /g, ""));
    });
    oTabla.rows().data().toArray().forEach(function (row) {
        var item = {};
        row.forEach(function (content, index) {
            item[fieldNames[index]] = content;
        });
        lstProgramas.push(item);
    });
    var DatosPrograma = {
        Documento: Documento,
        Nombre: Nombre,
        Colegio: Colegio,
        EstratoVivienda: EstratoVivienda,
        EstratoColegio: EstratoColegio,
        Comando: Comando,
        lstProgramas
    }

    $.ajax({
        type: "POST",
        url: "../Servidor/ControladorInscripcion.ashx",
        contentType: "application/json",
        data: JSON.stringify(DatosPrograma),
        dataType: "html",
        success: function (respuesta) {
            $("#dvMensaje").html(respuesta);
        },
        error: function (respuesta) {
            $("#dvMensaje").html("Error: " + respuesta);
        }
    });

}