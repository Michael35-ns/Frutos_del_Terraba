using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Frutos_del_Terraba.Models
{
    public class DistribucionViewModel
    {
        public int Id_distribucion { get; set; }

        [Required(ErrorMessage = "El destino es obligatorio.")]
        [StringLength(200, ErrorMessage = "El destino no pueden exceder los 200 caracteres.")]
        public string Destino { get; set; }

        [StringLength(300, ErrorMessage = "La ubicacion no pueden exceder los 300 caracteres.")]
        public string Ubicacion { get; set; }

        [StringLength(300, ErrorMessage = "Las observaciones no pueden exceder los 300 caracteres.")]
        public string Observaciones { get; set; }

        public string UserId { get; set; }

        public UsuarioViewModel ? Usuario { get; set; }

        [JsonIgnore]
        public ICollection<DetallesDistribucionViewModel> ? DetallesDistribuciones { get; set; }
    }
}
