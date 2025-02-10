using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frutos_del_Terraba.Models
{
    public class Inventario
    {
        [Key]
        public int Id_inventario { get; set; }

        public int Cantidad { get; set; }

        [Required]
        [ForeignKey("Producto")]
        public int Id_producto { get; set; }
        public Producto Producto { get; set; }
        public ICollection<Distribucion> Distribuciones { get; set; }
    }
}
