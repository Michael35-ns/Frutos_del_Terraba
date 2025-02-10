using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class Reporte
    {
        [Key]
        public int Id_reporte { get; set; }
        public int Id_producto { get; set; }
        public int Cantidad_reportada { get; set; }
        public string Motivo { get; set; }
        public DateTime Fecha { get; set; }

        public Producto Producto { get; set; } // Relación con Producto
    }

}
