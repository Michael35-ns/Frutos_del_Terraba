﻿// <auto-generated />
using System;
using Frutos_del_Terraba.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Frutos_del_Terraba.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250210024748_Primera migración con todos los modelos listos")]
    partial class Primeramigracióncontodoslosmodeloslistos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Frutos_del_Terraba.Models.Categoria", b =>
                {
                    b.Property<int>("Id_categoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_categoria"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_categoria");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.DetallesPedido", b =>
                {
                    b.Property<int>("Id_detalle")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_detalle"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("Id_pedido")
                        .HasColumnType("int");

                    b.Property<int>("Id_producto")
                        .HasColumnType("int");

                    b.Property<string>("Observaciones")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PedidoId_pedido")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId_producto")
                        .HasColumnType("int");

                    b.HasKey("Id_detalle");

                    b.HasIndex("PedidoId_pedido");

                    b.HasIndex("ProductoId_producto");

                    b.ToTable("DetallesPedidos");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Distribucion", b =>
                {
                    b.Property<int>("Id_distribucion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_distribucion"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Destino")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id_inventario")
                        .HasColumnType("int");

                    b.Property<int>("InventarioId_inventario")
                        .HasColumnType("int");

                    b.HasKey("Id_distribucion");

                    b.HasIndex("InventarioId_inventario");

                    b.ToTable("Distribuciones");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Inventario", b =>
                {
                    b.Property<int>("Id_inventario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_inventario"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("Id_producto")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId_producto")
                        .HasColumnType("int");

                    b.HasKey("Id_inventario");

                    b.HasIndex("ProductoId_producto");

                    b.ToTable("Inventarios");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Pedido", b =>
                {
                    b.Property<int>("Id_pedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_pedido"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id_proveedor")
                        .HasColumnType("int");

                    b.Property<int>("Id_usuario")
                        .HasColumnType("int");

                    b.Property<int>("ProveedorId_proveedor")
                        .HasColumnType("int");

                    b.HasKey("Id_pedido");

                    b.HasIndex("ProveedorId_proveedor");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Producto", b =>
                {
                    b.Property<int>("Id_producto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_producto"));

                    b.Property<int>("CategoriaId_categoria")
                        .HasColumnType("int");

                    b.Property<int>("Id_categoria")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id_producto");

                    b.HasIndex("CategoriaId_categoria");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Proveedor", b =>
                {
                    b.Property<int>("Id_proveedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_proveedor"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_proveedor");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Reporte", b =>
                {
                    b.Property<int>("Id_reporte")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_reporte"));

                    b.Property<int>("Cantidad_reportada")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id_producto")
                        .HasColumnType("int");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductoId_producto")
                        .HasColumnType("int");

                    b.HasKey("Id_reporte");

                    b.HasIndex("ProductoId_producto");

                    b.ToTable("Reportes");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.DetallesPedido", b =>
                {
                    b.HasOne("Frutos_del_Terraba.Models.Pedido", "Pedido")
                        .WithMany()
                        .HasForeignKey("PedidoId_pedido")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Frutos_del_Terraba.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId_producto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Distribucion", b =>
                {
                    b.HasOne("Frutos_del_Terraba.Models.Inventario", "Inventario")
                        .WithMany()
                        .HasForeignKey("InventarioId_inventario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventario");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Inventario", b =>
                {
                    b.HasOne("Frutos_del_Terraba.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId_producto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Pedido", b =>
                {
                    b.HasOne("Frutos_del_Terraba.Models.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId_proveedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Producto", b =>
                {
                    b.HasOne("Frutos_del_Terraba.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId_categoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Reporte", b =>
                {
                    b.HasOne("Frutos_del_Terraba.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId_producto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");
                });
#pragma warning restore 612, 618
        }
    }
}
