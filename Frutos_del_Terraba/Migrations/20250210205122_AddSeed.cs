using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Frutos_del_Terraba.Migrations
{
    /// <inheritdoc />
    public partial class AddSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Productos_Id_producto",
                table: "Inventarios");

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id_categoria", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Todas las frutas", "Frutas" },
                    { 2, "Todas las verduras", "Verduras" },
                    { 3, "Todas las verduras", "Legumbres" }
                });

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "Id_proveedor", "Apellidos", "Email", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "Mata Rabbe", "gustabo@gmial.com", "Gustavo", "85637049" },
                    { 2, "Tencio Solano", "elena@gmial.com", "Elena", "98540244" }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id_pedido", "Fecha", "Id_proveedor", "Id_usuario" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 10, 14, 51, 21, 628, DateTimeKind.Local).AddTicks(6038), 1, 1 },
                    { 2, new DateTime(2025, 2, 10, 14, 51, 21, 628, DateTimeKind.Local).AddTicks(6053), 1, 1 },
                    { 3, new DateTime(2025, 2, 10, 14, 51, 21, 628, DateTimeKind.Local).AddTicks(6055), 2, 1 },
                    { 4, new DateTime(2025, 2, 10, 14, 51, 21, 628, DateTimeKind.Local).AddTicks(6056), 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id_producto", "Id_categoria", "Nombre", "Stock" },
                values: new object[,]
                {
                    { 1, 3, "Culantro Coyote", 40 },
                    { 2, 3, "Tonillo", 46 },
                    { 3, 3, "Apio", 50 },
                    { 4, 1, "Papaya", 50 },
                    { 5, 1, "Manzana Verde", 50 },
                    { 6, 1, "Manzana Roja", 100 },
                    { 7, 2, "Brocoli", 100 }
                });

            migrationBuilder.InsertData(
                table: "DetallesPedidos",
                columns: new[] { "Id_detalle", "Cantidad", "Id_pedido", "Id_producto", "Observaciones" },
                values: new object[,]
                {
                    { 1, 40, 1, 1, "" },
                    { 2, 46, 1, 2, "" },
                    { 3, 50, 1, 3, "" },
                    { 4, 50, 2, 4, "" },
                    { 5, 50, 3, 5, "" },
                    { 6, 100, 3, 6, "" },
                    { 7, 100, 4, 7, "" }
                });

            migrationBuilder.InsertData(
                table: "Inventarios",
                columns: new[] { "Id_inventario", "Cantidad", "Id_producto" },
                values: new object[,]
                {
                    { 1, 30, 1 },
                    { 2, 25, 2 },
                    { 3, 20, 3 },
                    { 4, 50, 4 },
                    { 5, 60, 5 },
                    { 6, 80, 6 },
                    { 7, 90, 7 }
                });

            migrationBuilder.InsertData(
                table: "Distribuciones",
                columns: new[] { "Id_distribucion", "Cantidad", "Destino", "Id_inventario" },
                values: new object[,]
                {
                    { 1, 10, "Supermercado A", 1 },
                    { 2, 15, "Supermercado B", 2 },
                    { 3, 5, "Frutería C", 3 },
                    { 4, 25, "Verdulería D", 4 },
                    { 5, 30, "Tienda E", 5 },
                    { 6, 40, "Mercado F", 6 },
                    { 7, 50, "Comedor G", 7 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Productos_Id_producto",
                table: "Inventarios",
                column: "Id_producto",
                principalTable: "Productos",
                principalColumn: "Id_producto",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Productos_Id_producto",
                table: "Inventarios");

            migrationBuilder.DeleteData(
                table: "DetallesPedidos",
                keyColumn: "Id_detalle",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DetallesPedidos",
                keyColumn: "Id_detalle",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DetallesPedidos",
                keyColumn: "Id_detalle",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DetallesPedidos",
                keyColumn: "Id_detalle",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DetallesPedidos",
                keyColumn: "Id_detalle",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DetallesPedidos",
                keyColumn: "Id_detalle",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DetallesPedidos",
                keyColumn: "Id_detalle",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Distribuciones",
                keyColumn: "Id_distribucion",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Distribuciones",
                keyColumn: "Id_distribucion",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Distribuciones",
                keyColumn: "Id_distribucion",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Distribuciones",
                keyColumn: "Id_distribucion",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Distribuciones",
                keyColumn: "Id_distribucion",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Distribuciones",
                keyColumn: "Id_distribucion",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Distribuciones",
                keyColumn: "Id_distribucion",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Inventarios",
                keyColumn: "Id_inventario",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Inventarios",
                keyColumn: "Id_inventario",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Inventarios",
                keyColumn: "Id_inventario",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Inventarios",
                keyColumn: "Id_inventario",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Inventarios",
                keyColumn: "Id_inventario",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Inventarios",
                keyColumn: "Id_inventario",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Inventarios",
                keyColumn: "Id_inventario",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id_pedido",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id_pedido",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id_pedido",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id_pedido",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Proveedores",
                keyColumn: "Id_proveedor",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Proveedores",
                keyColumn: "Id_proveedor",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id_categoria",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id_categoria",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id_categoria",
                keyValue: 3);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Productos_Id_producto",
                table: "Inventarios",
                column: "Id_producto",
                principalTable: "Productos",
                principalColumn: "Id_producto",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
