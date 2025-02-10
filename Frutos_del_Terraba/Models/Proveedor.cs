using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class Proveedor
    {
        [Key]
        public int Id_proveedor { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "El nombre no pueden exceder los 70 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Los apellidos no pueden exceder los 100 caracteres.")]
        public string Apellidos { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "El email no pueden exceder los 70 caracteres.")]
        public string Email { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
    }
}
