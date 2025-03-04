using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frutos_del_Terraba_Api.Models
{
    public class Pedido
    {
        [Key]
        public int Id_pedido { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser Usuario { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        [ForeignKey("Proveedor")]
        public int Id_proveedor { get; set; } 

        public Proveedor Proveedor { get; set; }
        public ICollection<DetallesPedido> DetallesPedidos { get; set; }
    }
}
