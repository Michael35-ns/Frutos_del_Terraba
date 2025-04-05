using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Frutos_del_Terraba_Api.Models
{
    public class DetallesDistribucion
    {
        [Key]
        public int Id_detalle_distribucion { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        public int Cantidad { get; set; }

        [Required]
        [ForeignKey("Distribucion")]
        public int Id_distribucion { get; set; }

        [Required]
        [ForeignKey("Inventario")]
        public int Id_inventario { get; set; }

        [JsonIgnore]
        public Distribucion Distribucion { get; set; }
        public Inventario Inventario { get; set; }


    }
}
