using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Frutos_del_Terraba_Api.Models
{
    public class Distribucion
    {
        [Key]
        public int Id_distribucion { get; set; }

        [Required(ErrorMessage = "El destino es obligatorio.")]
        [StringLength(200, ErrorMessage = "El destino no pueden exceder los 200 caracteres.")]
        public string Destino { get; set; }

        [Required(ErrorMessage = "La ubicacion es obligatoria.")]
        [StringLength(300, ErrorMessage = "La ubicacion no pueden exceder los 300 caracteres.")]
        public string Ubicacion { get; set; }

        [StringLength(300, ErrorMessage = "Las observaciones no pueden exceder los 300 caracteres.")]
        public string Observaciones { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser Usuario { get; set; }

        [JsonIgnore]
        public ICollection<DetallesDistribucion> DetallesDistribuciones { get; set; }
    }
}
