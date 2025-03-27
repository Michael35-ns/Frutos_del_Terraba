using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frutos_del_Terraba.Models
{
    public class PedidoViewModel
    {
        public int Id_pedido { get; set; }

        public int Id_usuario { get; set; } 

        public bool Estado {  get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        public int Id_proveedor { get; set; } 
    }
}
