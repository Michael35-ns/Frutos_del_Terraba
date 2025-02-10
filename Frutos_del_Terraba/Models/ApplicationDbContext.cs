using Microsoft.EntityFrameworkCore;

namespace Frutos_del_Terraba.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Reporte> Reportes { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Distribucion> Distribuciones { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallesPedido> DetallesPedidos { get; set; }
    }
}
