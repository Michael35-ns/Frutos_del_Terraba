using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frutos_del_Terraba.Models
{
    public class InventarioViewModel
    {
        public int Id_inventario { get; set; }

        public int Cantidad { get; set; }

        [Required]
        public int Id_producto { get; set; }
    }
}
