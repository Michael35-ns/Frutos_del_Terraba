﻿// <auto-generated />
using System;
using Frutos_del_Terraba.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Frutos_del_Terraba.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("Id_categoria");

                    b.ToTable("Categorias");

                    b.HasData(
                        new
                        {
                            Id_categoria = 1,
                            Descripcion = "Todas las frutas",
                            Nombre = "Frutas"
                        },
                        new
                        {
                            Id_categoria = 2,
                            Descripcion = "Todas las verduras",
                            Nombre = "Verduras"
                        },
                        new
                        {
                            Id_categoria = 3,
                            Descripcion = "Todas las verduras",
                            Nombre = "Legumbres"
                        });
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
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id_detalle");

                    b.HasIndex("Id_pedido");

                    b.HasIndex("Id_producto");

                    b.ToTable("DetallesPedidos");

                    b.HasData(
                        new
                        {
                            Id_detalle = 1,
                            Cantidad = 40,
                            Id_pedido = 1,
                            Id_producto = 1,
                            Observaciones = ""
                        },
                        new
                        {
                            Id_detalle = 2,
                            Cantidad = 46,
                            Id_pedido = 1,
                            Id_producto = 2,
                            Observaciones = ""
                        },
                        new
                        {
                            Id_detalle = 3,
                            Cantidad = 50,
                            Id_pedido = 1,
                            Id_producto = 3,
                            Observaciones = ""
                        },
                        new
                        {
                            Id_detalle = 4,
                            Cantidad = 50,
                            Id_pedido = 2,
                            Id_producto = 4,
                            Observaciones = ""
                        },
                        new
                        {
                            Id_detalle = 5,
                            Cantidad = 50,
                            Id_pedido = 3,
                            Id_producto = 5,
                            Observaciones = ""
                        },
                        new
                        {
                            Id_detalle = 6,
                            Cantidad = 100,
                            Id_pedido = 3,
                            Id_producto = 6,
                            Observaciones = ""
                        },
                        new
                        {
                            Id_detalle = 7,
                            Cantidad = 100,
                            Id_pedido = 4,
                            Id_producto = 7,
                            Observaciones = ""
                        });
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
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Id_inventario")
                        .HasColumnType("int");

                    b.HasKey("Id_distribucion");

                    b.HasIndex("Id_inventario");

                    b.ToTable("Distribuciones");

                    b.HasData(
                        new
                        {
                            Id_distribucion = 1,
                            Cantidad = 10,
                            Destino = "Supermercado A",
                            Id_inventario = 1
                        },
                        new
                        {
                            Id_distribucion = 2,
                            Cantidad = 15,
                            Destino = "Supermercado B",
                            Id_inventario = 2
                        },
                        new
                        {
                            Id_distribucion = 3,
                            Cantidad = 5,
                            Destino = "Frutería C",
                            Id_inventario = 3
                        },
                        new
                        {
                            Id_distribucion = 4,
                            Cantidad = 25,
                            Destino = "Verdulería D",
                            Id_inventario = 4
                        },
                        new
                        {
                            Id_distribucion = 5,
                            Cantidad = 30,
                            Destino = "Tienda E",
                            Id_inventario = 5
                        },
                        new
                        {
                            Id_distribucion = 6,
                            Cantidad = 40,
                            Destino = "Mercado F",
                            Id_inventario = 6
                        },
                        new
                        {
                            Id_distribucion = 7,
                            Cantidad = 50,
                            Destino = "Comedor G",
                            Id_inventario = 7
                        });
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

                    b.HasKey("Id_inventario");

                    b.HasIndex("Id_producto");

                    b.ToTable("Inventarios");

                    b.HasData(
                        new
                        {
                            Id_inventario = 1,
                            Cantidad = 30,
                            Id_producto = 1
                        },
                        new
                        {
                            Id_inventario = 2,
                            Cantidad = 25,
                            Id_producto = 2
                        },
                        new
                        {
                            Id_inventario = 3,
                            Cantidad = 20,
                            Id_producto = 3
                        },
                        new
                        {
                            Id_inventario = 4,
                            Cantidad = 50,
                            Id_producto = 4
                        },
                        new
                        {
                            Id_inventario = 5,
                            Cantidad = 60,
                            Id_producto = 5
                        },
                        new
                        {
                            Id_inventario = 6,
                            Cantidad = 80,
                            Id_producto = 6
                        },
                        new
                        {
                            Id_inventario = 7,
                            Cantidad = 90,
                            Id_producto = 7
                        });
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

                    b.HasKey("Id_pedido");

                    b.HasIndex("Id_proveedor");

                    b.ToTable("Pedidos");

                    b.HasData(
                        new
                        {
                            Id_pedido = 1,
                            Fecha = new DateTime(2025, 2, 10, 14, 51, 21, 628, DateTimeKind.Local).AddTicks(6038),
                            Id_proveedor = 1,
                            Id_usuario = 1
                        },
                        new
                        {
                            Id_pedido = 2,
                            Fecha = new DateTime(2025, 2, 10, 14, 51, 21, 628, DateTimeKind.Local).AddTicks(6053),
                            Id_proveedor = 1,
                            Id_usuario = 1
                        },
                        new
                        {
                            Id_pedido = 3,
                            Fecha = new DateTime(2025, 2, 10, 14, 51, 21, 628, DateTimeKind.Local).AddTicks(6055),
                            Id_proveedor = 2,
                            Id_usuario = 1
                        },
                        new
                        {
                            Id_pedido = 4,
                            Fecha = new DateTime(2025, 2, 10, 14, 51, 21, 628, DateTimeKind.Local).AddTicks(6056),
                            Id_proveedor = 2,
                            Id_usuario = 1
                        });
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Producto", b =>
                {
                    b.Property<int>("Id_producto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_producto"));

                    b.Property<int>("Id_categoria")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id_producto");

                    b.HasIndex("Id_categoria");

                    b.ToTable("Productos");

                    b.HasData(
                        new
                        {
                            Id_producto = 1,
                            Id_categoria = 3,
                            Nombre = "Culantro Coyote",
                            Stock = 40
                        },
                        new
                        {
                            Id_producto = 2,
                            Id_categoria = 3,
                            Nombre = "Tonillo",
                            Stock = 46
                        },
                        new
                        {
                            Id_producto = 3,
                            Id_categoria = 3,
                            Nombre = "Apio",
                            Stock = 50
                        },
                        new
                        {
                            Id_producto = 4,
                            Id_categoria = 1,
                            Nombre = "Papaya",
                            Stock = 50
                        },
                        new
                        {
                            Id_producto = 5,
                            Id_categoria = 1,
                            Nombre = "Manzana Verde",
                            Stock = 50
                        },
                        new
                        {
                            Id_producto = 6,
                            Id_categoria = 1,
                            Nombre = "Manzana Roja",
                            Stock = 100
                        },
                        new
                        {
                            Id_producto = 7,
                            Id_categoria = 2,
                            Nombre = "Brocoli",
                            Stock = 100
                        });
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Proveedor", b =>
                {
                    b.Property<int>("Id_proveedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_proveedor"));

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_proveedor");

                    b.ToTable("Proveedores");

                    b.HasData(
                        new
                        {
                            Id_proveedor = 1,
                            Apellidos = "Mata Rabbe",
                            Email = "gustabo@gmial.com",
                            Nombre = "Gustavo",
                            Telefono = "85637049"
                        },
                        new
                        {
                            Id_proveedor = 2,
                            Apellidos = "Tencio Solano",
                            Email = "elena@gmial.com",
                            Nombre = "Elena",
                            Telefono = "98540244"
                        });
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
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id_reporte");

                    b.HasIndex("Id_producto");

                    b.ToTable("Reportes");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.DetallesPedido", b =>
                {
                    b.HasOne("Frutos_del_Terraba.Models.Pedido", "Pedido")
                        .WithMany("DetallesPedidos")
                        .HasForeignKey("Id_pedido")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Frutos_del_Terraba.Models.Producto", "Producto")
                        .WithMany("DetallesPedidos")
                        .HasForeignKey("Id_producto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Distribucion", b =>
                {
                    b.HasOne("Frutos_del_Terraba.Models.Inventario", "Inventario")
                        .WithMany("Distribuciones")
                        .HasForeignKey("Id_inventario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventario");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Inventario", b =>
                {
                    b.HasOne("Frutos_del_Terraba.Models.Producto", "Producto")
                        .WithMany("Inventarios")
                        .HasForeignKey("Id_producto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Pedido", b =>
                {
                    b.HasOne("Frutos_del_Terraba.Models.Proveedor", "Proveedor")
                        .WithMany("Pedidos")
                        .HasForeignKey("Id_proveedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Producto", b =>
                {
                    b.HasOne("Frutos_del_Terraba.Models.Categoria", "Categoria")
                        .WithMany("Productos")
                        .HasForeignKey("Id_categoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Reporte", b =>
                {
                    b.HasOne("Frutos_del_Terraba.Models.Producto", "Producto")
                        .WithMany("Reportes")
                        .HasForeignKey("Id_producto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Categoria", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Inventario", b =>
                {
                    b.Navigation("Distribuciones");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Pedido", b =>
                {
                    b.Navigation("DetallesPedidos");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Producto", b =>
                {
                    b.Navigation("DetallesPedidos");

                    b.Navigation("Inventarios");

                    b.Navigation("Reportes");
                });

            modelBuilder.Entity("Frutos_del_Terraba.Models.Proveedor", b =>
                {
                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
