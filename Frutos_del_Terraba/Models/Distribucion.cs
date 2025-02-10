using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class Distribucion
    {
        [Key]
        public int Id_distribucion { get; set; }
        public int Id_inventario { get; set; }
        public string Destino { get; set; }
        public int Cantidad { get; set; }

        public Inventario Inventario { get; set; }
    }
}
