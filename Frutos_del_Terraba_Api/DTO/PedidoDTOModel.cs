using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba_Api.DTO
{
    public class PedidoDTOModel
    {
        public int Id_pedido { get; set; }

        [Required]
        public string UserId { get; set; }

        public bool Estado { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        public int Id_proveedor { get; set; }
    }
}
