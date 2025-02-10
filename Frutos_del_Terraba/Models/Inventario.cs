using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class Inventario
    {
        [Key]
        public int Id_inventario { get; set; }
        public int Id_producto { get; set; }
        public int Cantidad { get; set; }

        public Producto Producto { get; set; }
    }
}
