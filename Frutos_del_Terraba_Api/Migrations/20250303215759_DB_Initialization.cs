using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Frutos_del_Terraba_Api.Migrations
{
    /// <inheritdoc />
    public partial class DB_Initialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_proveedor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id_pedido);
                    table.ForeignKey(
                        name: "FK_Pedidos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "aecad1ec-a292-47f0-b58c-7b3b7fcce86a", null, "Distribuidor", "DISTRIBUIDOR" },
                    { "ca645775-80c6-44a5-b5ab-079a07cd521e", null, "Empleado", "EMPLEADO" },
                    { "d86e1426-a604-499c-b4d2-ad5023a6b42d", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5900c253-7075-4165-95de-79bc9952f055", 0, "04f67baa-8098-437f-a53f-81cce1bbd059", "rodolfo@gmail.com", true, false, null, "RODOLFO@GMAIL.COM", "RODOLFO@GMAIL.COM", "AQAAAAIAAYagAAAAELaKytraFxqY/yHjLMX3WVsHZCoThOMpTQIabFaEtfU8rZHjypif52LF6DU6MmX4ww==", null, false, "a39be971-03ad-4830-afdd-76c90d112fc7", false, "rodolfo@gmail.com" },
                    { "7cba396e-ef44-45d4-8ca8-cbe90733d21e", 0, "35eb9634-0265-49df-9db7-f19472c45751", "fabian@gmail.com", true, false, null, "FABIAN@GMAIL.COM", "FABIAN@GMAIL.COM", "AQAAAAIAAYagAAAAEDrXJ5R8HyW5N3PNAf74XmO+pO0rpXGxiVkNRcVw0AOzM03KEo9fPcnE6sXfx4Cn8Q==", null, false, "160f00ea-0d4a-4f55-98e3-435627c81f46", false, "fabian@gmail.com" },
                    { "8ea58a73-f3c8-4b9a-bb9f-980f00e9bb43", 0, "be4323f7-2220-43ce-8104-e01fd3c1986e", "cristopher@gmail.com", true, false, null, "CRISTOPHER@GMAIL.COM", "CRISTOPHER@GMAIL.COM", "AQAAAAIAAYagAAAAEOavrtmtV+QrmYgc0nMnUzGIyxNMVFf11rrRRGPv4hV6hxskWDqaeXfHrHaRfHTaEA==", null, false, "70129b1e-5385-4bef-a31c-fe1492b99563", false, "cristopher@gmail.com" }
                });

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
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "aecad1ec-a292-47f0-b58c-7b3b7fcce86a", "5900c253-7075-4165-95de-79bc9952f055" },
                    { "d86e1426-a604-499c-b4d2-ad5023a6b42d", "7cba396e-ef44-45d4-8ca8-cbe90733d21e" },
                    { "ca645775-80c6-44a5-b5ab-079a07cd521e", "8ea58a73-f3c8-4b9a-bb9f-980f00e9bb43" }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id_pedido", "Fecha", "Id_proveedor", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 3, 15, 57, 58, 659, DateTimeKind.Local).AddTicks(6742), 1, "7cba396e-ef44-45d4-8ca8-cbe90733d21e" },
                    { 2, new DateTime(2025, 3, 3, 15, 57, 58, 659, DateTimeKind.Local).AddTicks(6767), 1, "7cba396e-ef44-45d4-8ca8-cbe90733d21e" },
                    { 3, new DateTime(2025, 3, 3, 15, 57, 58, 659, DateTimeKind.Local).AddTicks(6769), 2, "7cba396e-ef44-45d4-8ca8-cbe90733d21e" },
                    { 4, new DateTime(2025, 3, 3, 15, 57, 58, 659, DateTimeKind.Local).AddTicks(6771), 2, "7cba396e-ef44-45d4-8ca8-cbe90733d21e" }
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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_Pedidos_UserId",
                table: "Pedidos",
                column: "UserId");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DetallesPedidos");

            migrationBuilder.DropTable(
                name: "Distribuciones");

            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
