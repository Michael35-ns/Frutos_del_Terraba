using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }


        [Required]
        [StringLength(70, ErrorMessage = "El nombre no pueden exceder los 70 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public int Id_categoria { get; set; }

        public string? NombreCategoria { get; set; }
    }
}
