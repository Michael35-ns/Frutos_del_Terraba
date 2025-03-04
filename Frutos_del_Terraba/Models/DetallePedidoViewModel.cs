using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class DetallePedidoViewModel
    {
        public int Id_detalle { get; set; }
        public int Id_producto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        // Nombre del producto para mostrar en la vista
        public string NombreProducto { get; set; }
    }
}
