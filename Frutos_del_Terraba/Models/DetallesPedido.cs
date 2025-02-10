using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frutos_del_Terraba.Models
{
    public class DetallesPedido
    {
        [Key]
        public int Id_detalle { get; set; }

        public int Cantidad { get; set; }

        [StringLength(300, ErrorMessage = "Las observaciones no pueden exceder los 300 caracteres.")]
        public string Observaciones { get; set; }

        [Required]
        [ForeignKey("Pedido")]
        public int Id_pedido { get; set; }

        [Required]
        [ForeignKey("Producto")]
        public int Id_producto { get; set; }

        public Pedido Pedido { get; set; }
        public Producto Producto { get; set; }

    }
}
