using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frutos_del_Terraba.Models
{
    public class Producto
    {
        [Key]
        public int Id_producto { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "El nombre no pueden exceder los 70 caracteres.")]
        public string Nombre { get; set; }

        public int Stock { get; set; }

        [Required]
        [ForeignKey("Categoria")]
        public int Id_categoria { get; set; }  

        public Categoria Categoria { get; set; }
        public ICollection<Reporte> Reportes { get; set; }
        public ICollection<DetallesPedido> DetallesPedidos { get; set; }
        public ICollection<Inventario> Inventarios { get; set; }
    }
}
