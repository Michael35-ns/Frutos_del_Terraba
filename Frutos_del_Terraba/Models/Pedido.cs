using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class Pedido
    {
        [Key]
        public int Id_pedido { get; set; }
        public int Id_usuario { get; set; } 
        public int Id_proveedor { get; set; }
        public DateTime Fecha { get; set; }

        public Proveedor Proveedor { get; set; }
    }
}
