using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba_Api.DTO
{
    public class DistribucionDTOModel
    {
        public int Id_distribucion { get; set; }

        [Required(ErrorMessage = "El destino es obligatorio.")]
        [StringLength(200, ErrorMessage = "El destino no pueden exceder los 200 caracteres.")]
        public string Destino { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        public int Cantidad { get; set; }

        [Required]
        public int Id_inventario { get; set; }
    }
}
