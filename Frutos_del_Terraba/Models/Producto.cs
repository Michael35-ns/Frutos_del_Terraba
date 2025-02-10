using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class Producto
    {
        [Key]
        public int Id_producto { get; set; }
        public int Id_categoria { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }

        public Categoria Categoria { get; set; }
    }
}
