/**
 * Config
 * -------------------------------------------------------------------------------------
 * ! IMPORTANT: Make sure you clear the browser local storage In order to see the config changes in the template.
 * ! To clear local storage: (https://www.leadshook.com/help/how-to-clear-local-storage-in-google-chrome-browser/).
 */

'use strict';

const { auto } = require("@popperjs/core");

// JS global variables
window.config = {
  colors: {
    primary: '#8c57ff',
    secondary: '#8a8d93',
    success: '#56ca00',
    info: '#16b1ff',
    warning: '#ffb400',
    danger: '#ff4c51',
    dark: '#4b4b4b',
    black: '#2e263d',
    white: '#fff',
    cardColor: '#fff',
    bodyBg: '#f4f5fa',
    bodyColor: '#6D6777',
    headingColor: '#433C50',
    textMuted: '#ABA8B1',
    borderColor: '#E6E5E8',
    chartBgColor: '#F0F2F8'
  }
};


$(document).ready(function () {
    // Aplicamos DataTable a la tabla con id #DataTables_Table_0
    $("#DataTables_Table_0").DataTable({
        "language": {
            "decimal": "",
            "emptyTable": "No hay informacion",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 a 0 de 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "autoWidth": false
    });
});


function confirmDelete(entityName, entityId, deleteUrl) {
    console.log("confirmDelete ejecutada");  // Verifica si la función se está llamando
    Swal.fire({
        title: `Estas seguro de eliminar ${entityName}?`,
        text: "No podras revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: `Si, eliminar ${entityName}!`
    }).then((result) => {
        if (result.isConfirmed) {
            const finalUrl = deleteUrl.replace("{id}", entityId);
            console.log(finalUrl);  // Verifica la URL final
            fetch(finalUrl, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => response.json())
                .then(data => {
                    console.log(data);  // Verifica la respuesta del servidor
                    if (data.success) {
                        Swal.fire(
                            `${entityName.charAt(0).toUpperCase() + entityName.slice(1)} eliminado!`,
                            `La ${entityName} ha sido eliminada.`,
                            'success'
                        ).then(() => {
                            location.reload();
                        });
                    } else {
                        Swal.fire(
                            'Error!',
                            data.message || `No se pudo eliminar la ${entityName}.`,
                            'error'
                        );
                    }
                })
                .catch(error => {
                    console.error('Detalles del error:', error);  // Imprime detalles del error
                    Swal.fire(
                        'Error!',
                        `Ocurrio un error al eliminar la ${entityName}. Detalles: ${error.message || error}`,
                        'error'
                    );
                });
        }
    });
} 

