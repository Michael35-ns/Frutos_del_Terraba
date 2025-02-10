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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Categoria y Producto (Uno a Muchos)
            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Productos)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.Id_categoria)
                .OnDelete(DeleteBehavior.Cascade); 

            // Pedido y DetallesPedido (Uno a Muchos)
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.DetallesPedidos)
                .WithOne(dp => dp.Pedido)
                .HasForeignKey(dp => dp.Id_pedido)
                .OnDelete(DeleteBehavior.Cascade); 

            // Producto y DetallesPedido (Uno a Muchos)
            modelBuilder.Entity<Producto>()
                .HasMany(p => p.DetallesPedidos)
                .WithOne(dp => dp.Producto)
                .HasForeignKey(dp => dp.Id_producto)
                .OnDelete(DeleteBehavior.Cascade); 

            // Inventario y Distribucion (Uno a Muchos)
            modelBuilder.Entity<Inventario>()
                .HasMany(i => i.Distribuciones)
                .WithOne(d => d.Inventario)
                .HasForeignKey(d => d.Id_inventario)
                .OnDelete(DeleteBehavior.Cascade); 

            // Producto e Inventario (Muchos a Uno)
            modelBuilder.Entity<Inventario>()
                .HasOne(i => i.Producto)
                .WithMany(p => p.Inventarios)
                .HasForeignKey(i => i.Id_producto)
                .OnDelete(DeleteBehavior.Restrict); 

            // Proveedor y Pedido (Uno a Muchos)
            modelBuilder.Entity<Proveedor>()
                .HasMany(p => p.Pedidos)
                .WithOne(ped => ped.Proveedor)
                .HasForeignKey(ped => ped.Id_proveedor)
                .OnDelete(DeleteBehavior.Cascade); 

            // Producto y Reporte (Uno a Muchos)
            modelBuilder.Entity<Producto>()
                .HasMany(p => p.Reportes)
                .WithOne(r => r.Producto)
                .HasForeignKey(r => r.Id_producto)
                .OnDelete(DeleteBehavior.Cascade); 

            base.OnModelCreating(modelBuilder);

            modelBuilder.Seed();
        }
    }
}
