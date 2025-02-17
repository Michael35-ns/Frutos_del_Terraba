using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Frutos_del_Terraba_Api.Models
{
    public class Categoria
    {
        [Key]
        public int Id_categoria { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "El nombre no pueden exceder los 70 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(200, ErrorMessage = "La descripcion no pueden exceder los 200 caracteres.")]
        public string Descripcion { get; set; }

        [BindNever]
        [JsonIgnore]
        public ICollection<Producto> Productos { get; set; }
    }

}
