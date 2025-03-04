using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba_Api.DTO
{
    public class InventarioDTOModel
    {
        public int Id_inventario { get; set; }

        public int Cantidad { get; set; }

        [Required]
        public int Id_producto { get; set; }
    }
}
