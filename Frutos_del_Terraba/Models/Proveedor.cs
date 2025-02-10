using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class Proveedor
    {
        [Key]
        public int Id_proveedor { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
