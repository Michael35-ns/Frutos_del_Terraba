using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Frutos_del_Terraba.Migrations
{
    /// <inheritdoc />
    public partial class InicialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id_categoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id_categoria);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id_proveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id_proveedor);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Id_categoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id_producto);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_Id_categoria",
                        column: x => x.Id_categoria,
                        principalTable: "Categorias",
                        principalColumn: "Id_categoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id_pedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_usuario = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_proveedor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id_pedido);
                    table.ForeignKey(
                        name: "FK_Pedidos_Proveedores_Id_proveedor",
                        column: x => x.Id_proveedor,
                        principalTable: "Proveedores",
                        principalColumn: "Id_proveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    Id_inventario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Id_producto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios", x => x.Id_inventario);
                    table.ForeignKey(
                        name: "FK_Inventarios_Productos_Id_producto",
                        column: x => x.Id_producto,
                        principalTable: "Productos",
                        principalColumn: "Id_producto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reportes",
                columns: table => new
                {
                    Id_reporte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad_reportada = table.Column<int>(type: "int", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_producto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportes", x => x.Id_reporte);
                    table.ForeignKey(
                        name: "FK_Reportes_Productos_Id_producto",
                        column: x => x.Id_producto,
                        principalTable: "Productos",
                        principalColumn: "Id_producto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallesPedidos",
                columns: table => new
                {
                    Id_detalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Id_pedido = table.Column<int>(type: "int", nullable: false),
                    Id_producto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesPedidos", x => x.Id_detalle);
                    table.ForeignKey(
                        name: "FK_DetallesPedidos_Pedidos_Id_pedido",
                        column: x => x.Id_pedido,
                        principalTable: "Pedidos",
                        principalColumn: "Id_pedido",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesPedidos_Productos_Id_producto",
                        column: x => x.Id_producto,
                        principalTable: "Productos",
                        principalColumn: "Id_producto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Distribuciones",
                columns: table => new
                {
                    Id_distribucion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Destino = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Id_inventario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distribuciones", x => x.Id_distribucion);
                    table.ForeignKey(
                        name: "FK_Distribuciones_Inventarios_Id_inventario",
                        column: x => x.Id_inventario,
                        principalTable: "Inventarios",
                        principalColumn: "Id_inventario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidos_Id_pedido",
                table: "DetallesPedidos",
                column: "Id_pedido");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidos_Id_producto",
                table: "DetallesPedidos",
                column: "Id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_Distribuciones_Id_inventario",
                table: "Distribuciones",
                column: "Id_inventario");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Id_producto",
                table: "Inventarios",
                column: "Id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Id_proveedor",
                table: "Pedidos",
                column: "Id_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Id_categoria",
                table: "Productos",
                column: "Id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_Id_producto",
                table: "Reportes",
                column: "Id_producto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesPedidos");

            migrationBuilder.DropTable(
                name: "Distribuciones");

            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
