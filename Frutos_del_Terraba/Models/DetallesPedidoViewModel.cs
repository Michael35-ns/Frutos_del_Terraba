using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frutos_del_Terraba.Models
{
    public class DetallesPedidoViewModel
    {
        public int Id_detalle { get; set; }

        public int Cantidad { get; set; }

        [StringLength(300, ErrorMessage = "Las observaciones no pueden exceder los 300 caracteres.")]
        public string Observaciones { get; set; }

        [Required]
        public int Id_pedido { get; set; }

        [Required]
        public int Id_producto { get; set; }

    }
}
