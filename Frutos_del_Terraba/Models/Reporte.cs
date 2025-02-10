using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frutos_del_Terraba.Models
{
    public class Reporte
    {
        [Key]
        public int Id_reporte { get; set; }

        [Required]
        public int Cantidad_reportada { get; set; }

        [Required(ErrorMessage = "El motivo es obligatorio.")]
        [StringLength(300, ErrorMessage = "El motivo no pueden exceder los 300 caracteres.")]
        public string Motivo { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [ForeignKey("Producto")]
        public int Id_producto { get; set; }
        public Producto Producto { get; set; } 
    }

}
