using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Frutos_del_Terraba_Api.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var adminRoleId = Guid.NewGuid().ToString();
            var empleadoRoleId = Guid.NewGuid().ToString();
            var distribuidorRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = empleadoRoleId, Name = "Empleado", NormalizedName = "EMPLEADO" },
                new IdentityRole { Id = distribuidorRoleId, Name = "Distribuidor", NormalizedName = "DISTRIBUIDOR" }
            );

            var hasher = new PasswordHasher<IdentityUser>();

            var usuario1Id = Guid.NewGuid().ToString();
            var usuario2Id = Guid.NewGuid().ToString();
            var usuario3Id = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = usuario1Id,
                    UserName = "fabian@gmail.com",
                    NormalizedUserName = "FABIAN@GMAIL.COM",
                    Email = "fabian@gmail.com",
                    NormalizedEmail = "FABIAN@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Fabian123$")
                },
                new IdentityUser
                {
                    Id = usuario2Id,
                    UserName = "cristopher@gmail.com",
                    NormalizedUserName = "CRISTOPHER@GMAIL.COM",
                    Email = "cristopher@gmail.com",
                    NormalizedEmail = "CRISTOPHER@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Cristopher123$")
                },
                new IdentityUser
                {
                    Id = usuario3Id,
                    UserName = "rodolfo@gmail.com",
                    NormalizedUserName = "RODOLFO@GMAIL.COM",
                    Email = "rodolfo@gmail.com",
                    NormalizedEmail = "RODOLFO@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Rodolfo123$")
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = usuario1Id, RoleId = adminRoleId },
                new IdentityUserRole<string> { UserId = usuario2Id, RoleId = empleadoRoleId },
                new IdentityUserRole<string> { UserId = usuario3Id, RoleId = distribuidorRoleId }
            );


            modelBuilder.Entity<Categoria>()
                .HasData(
                    new Categoria { Id_categoria= 1, Nombre="Frutas", Descripcion="Todas las frutas"},
                    new Categoria { Id_categoria = 2, Nombre = "Verduras", Descripcion = "Todas las verduras" },
                    new Categoria { Id_categoria = 3, Nombre = "Legumbres", Descripcion = "Todas las verduras" }
                );

            modelBuilder.Entity<Proveedor>()
                .HasData(
                    new Proveedor { Id_proveedor = 1, Nombre="Gustavo", Apellidos="Mata Rabbe", Email="gustabo@gmial.com", Telefono="85637049"},
                    new Proveedor { Id_proveedor = 2, Nombre = "Elena", Apellidos = "Tencio Solano", Email = "elena@gmial.com", Telefono = "98540244" }
                );

            modelBuilder.Entity<Producto>()
                .HasData(
                    new Producto { Id_producto = 1, Nombre = "Culantro Coyote", Stock = 40, Id_categoria = 3 },
                    new Producto { Id_producto = 2, Nombre = "Tonillo", Stock = 46, Id_categoria = 3 },
                    new Producto { Id_producto = 3, Nombre = "Apio", Stock = 50, Id_categoria = 3 },
                    new Producto { Id_producto = 4, Nombre = "Papaya", Stock = 50, Id_categoria = 1 },
                    new Producto { Id_producto = 5, Nombre = "Manzana Verde", Stock = 50, Id_categoria = 1 },
                    new Producto { Id_producto = 6, Nombre = "Manzana Roja", Stock = 100, Id_categoria = 1 },
                    new Producto { Id_producto = 7, Nombre = "Brocoli", Stock = 100, Id_categoria = 2 }
                );

            modelBuilder.Entity<Pedido>()
                .HasData(
                    new Pedido { Id_pedido = 1, Fecha=DateTime.Now, Id_proveedor = 1, UserId = usuario1Id },
                    new Pedido { Id_pedido = 2, Fecha = DateTime.Now, Id_proveedor = 1, UserId = usuario1Id },
                    new Pedido { Id_pedido = 3, Fecha = DateTime.Now, Id_proveedor = 2, UserId = usuario1Id },
                    new Pedido { Id_pedido = 4, Fecha = DateTime.Now, Id_proveedor = 2, UserId = usuario1Id }
                );

            modelBuilder.Entity<DetallesPedido>()
                .HasData(
                    new DetallesPedido { Id_detalle = 1, Id_pedido = 1, Id_producto = 1, Cantidad = 40, Observaciones=""},
                    new DetallesPedido { Id_detalle = 2, Id_pedido = 1, Id_producto = 2, Cantidad = 46, Observaciones = "" },
                    new DetallesPedido { Id_detalle = 3, Id_pedido = 1, Id_producto = 3, Cantidad = 50, Observaciones = "" },
                    new DetallesPedido { Id_detalle = 4, Id_pedido = 2, Id_producto = 4, Cantidad = 50, Observaciones = "" },
                    new DetallesPedido { Id_detalle = 5, Id_pedido = 3, Id_producto = 5, Cantidad = 50, Observaciones = "" },
                    new DetallesPedido { Id_detalle = 6, Id_pedido = 3, Id_producto = 6, Cantidad = 100, Observaciones = "" },
                    new DetallesPedido { Id_detalle = 7, Id_pedido = 4, Id_producto = 7, Cantidad = 100, Observaciones = "" }
                );
            

            modelBuilder.Entity<Inventario>()
                .HasData(
                    new Inventario { Id_inventario = 1, Id_producto = 1, Cantidad = 30 },
                    new Inventario { Id_inventario = 2, Id_producto = 2, Cantidad = 25 },
                    new Inventario { Id_inventario = 3, Id_producto = 3, Cantidad = 20 },
                    new Inventario { Id_inventario = 4, Id_producto = 4, Cantidad = 50 },
                    new Inventario { Id_inventario = 5, Id_producto = 5, Cantidad = 60 },
                    new Inventario { Id_inventario = 6, Id_producto = 6, Cantidad = 80 },
                    new Inventario { Id_inventario = 7, Id_producto = 7, Cantidad = 90 }
                );

            modelBuilder.Entity<Distribucion>()
                .HasData(
                    new Distribucion { Id_distribucion = 1, Destino = "Supermercado A", Cantidad = 10, Id_inventario = 1 },
                    new Distribucion { Id_distribucion = 2, Destino = "Supermercado B", Cantidad = 15, Id_inventario = 2 },
                    new Distribucion { Id_distribucion = 3, Destino = "Frutería C", Cantidad = 5, Id_inventario = 3 },
                    new Distribucion { Id_distribucion = 4, Destino = "Verdulería D", Cantidad = 25, Id_inventario = 4 },
                    new Distribucion { Id_distribucion = 5, Destino = "Tienda E", Cantidad = 30, Id_inventario = 5 },
                    new Distribucion { Id_distribucion = 6, Destino = "Mercado F", Cantidad = 40, Id_inventario = 6 },
                    new Distribucion { Id_distribucion = 7, Destino = "Comedor G", Cantidad = 50, Id_inventario = 7 }
                );
        }
    }
}
