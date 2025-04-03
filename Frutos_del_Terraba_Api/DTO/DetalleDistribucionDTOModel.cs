using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba_Api.DTO
{
    public class DetallesDistribucionDTOModel
    {
        public int Id_detalle_distribucion { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        public int Cantidad { get; set; }

        public int Id_distribucion { get; set; }

        [Required]
        public int Id_inventario { get; set; }

    }
}
