using Frutos_del_Terraba_Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class DetallesDistribucionViewModel
    {
        public int Id_detalle_distribucion { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        public int Cantidad { get; set; }

        [Required]
        public int Id_distribucion { get; set; }

        [Required]
        public int Id_inventario { get; set; }
        public DistribucionViewModel ? Distribucion { get; set; }

        public InventarioViewModel ? Inventario { get; set; }
    }
}
