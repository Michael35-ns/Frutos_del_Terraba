using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }


        [Required]
        [StringLength(70, ErrorMessage = "El nombre no pueden exceder los 70 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        public int Id_categoria { get; set; }

        public string? NombreCategoria { get; set; }

        public Categoria ? Categoria { get; set; }
        public ICollection<ReporteViewModel> ? Reportes { get; set; }
        public ICollection<DetallesPedidoViewModel> ? DetallesPedidos { get; set; }
        public ICollection<InventarioViewModel> ? Inventarios { get; set; }
    }
}
