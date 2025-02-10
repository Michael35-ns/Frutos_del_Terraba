using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class DetallesPedido
    {
        [Key]
        public int Id_detalle { get; set; }
        public int Id_pedido { get; set; }
        public int Id_producto { get; set; }
        public int Cantidad { get; set; }
        public string Observaciones { get; set; }

        public Pedido Pedido { get; set; } 
        public Producto Producto { get; set; }
    }
}
