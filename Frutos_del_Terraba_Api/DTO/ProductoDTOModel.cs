using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba_Api.DTO
{
    public class ProductoDTOModel
    {
        public int IdProducto { get; set; }


        [Required]
        [StringLength(70, ErrorMessage = "El nombre no pueden exceder los 70 caracteres.")]
        public string Nombre { get; set; }


        [Required]
        public int Id_categoria { get; set; }

    }
}
