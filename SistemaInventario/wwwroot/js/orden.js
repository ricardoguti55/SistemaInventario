
var datatable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("aprobado")) {
        loadDataTable("ObtenerOrdenLista?estado=aprobado");
    }
    else {
        if (url.includes("completado")) {
            loadDataTable("ObtenerOrdenLista?estado=completado");
        }
        else {
            if (url.includes("pendiente")) {
                loadDataTable("ObtenerOrdenLista?estado=pendiente");
            }
            else {
                loadDataTable("ObtenerOrdenLista?estado=todas");
            }
        }
       
    }

});

function loadDataTable(url) {
    datatable = $('#tblDatos').DataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ Registros Por Pagina",
            "zeroRecords": "Ningun Registro",
            "info": "Mostrar page _PAGE_ de _PAGES_",
            "infoEmpty": "no hay registros",
            "infoFiltered": "(filtered from _MAX_ total registros)",
            "search": "Buscar",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "ajax": {
            "url": "/Admin/Orden/" + url
        },
        "columns": [
            { "data": "id" },
            { "data": "nombresCliente" },
            { "data": "telefono" },
            { "data": "usuarioAplicacion.email" },
            { "data": "estadoOrden" },
            {
                "data": "fechaOrden",
                "render": function (data) {
                    if (!data) return '';

                    // Crear un objeto de fecha a partir de la cadena de fecha
                    var date = new Date(data);

                    // Extraer partes de la fecha
                    var day = ('0' + date.getDate()).slice(-2);
                    var month = ('0' + (date.getMonth() + 1)).slice(-2); // Los meses son 0-11 en JavaScript
                    var year = date.getFullYear();

                    // Extraer partes del tiempo
                    var hours = ('0' + date.getHours()).slice(-2);
                    var minutes = ('0' + date.getMinutes()).slice(-2);

                    // Formatear la fecha y la hora
                    var formattedDate = `${day}-${month}-${year} ${hours}:${minutes}`;

                    return formattedDate;
                }
            },
            {
                "data": "totalOrden", "className": "text-end",
                "render": function (data) {
                    var d = data.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
                    return d;
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Orden/Detalle/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="bi bi-ticket-detailed"></i>
                            </a>                           
                        </div>
                        `;
                }
            }
        ]
    });
}