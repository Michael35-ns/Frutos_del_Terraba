using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Frutos_del_Terraba_Api.Migrations
{
    /// <inheritdoc />
    public partial class DB_Creacion : Migration
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
                name: "Distribuciones",
                columns: table => new
                {
                    Id_distribucion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Destino = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distribuciones", x => x.Id_distribucion);
                    table.ForeignKey(
                        name: "FK_Distribuciones_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
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
                name: "DetallesDistribuciones",
                columns: table => new
                {
                    Id_detalle_distribucion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Id_distribucion = table.Column<int>(type: "int", nullable: false),
                    Id_inventario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesDistribuciones", x => x.Id_detalle_distribucion);
                    table.ForeignKey(
                        name: "FK_DetallesDistribuciones_Distribuciones_Id_distribucion",
                        column: x => x.Id_distribucion,
                        principalTable: "Distribuciones",
                        principalColumn: "Id_distribucion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesDistribuciones_Inventarios_Id_inventario",
                        column: x => x.Id_inventario,
                        principalTable: "Inventarios",
                        principalColumn: "Id_inventario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01daa330-1e67-4302-b70e-d9ee9bc90b36", null, "Admin", "ADMIN" },
                    { "a1f17514-3467-4b7f-9822-4e0d6a6df446", null, "Distribuidor", "DISTRIBUIDOR" },
                    { "ab91b54e-7068-40eb-ba14-66bc140979e4", null, "Empleado", "EMPLEADO" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1daa661e-c084-4685-a1c4-1691d4e409ce", 0, "3d47f076-ea6e-4935-9601-0293755833d4", "rodolfo@gmail.com", true, false, null, "RODOLFO@GMAIL.COM", "RODOLFO@GMAIL.COM", "AQAAAAIAAYagAAAAEGleacv7xd6BO9kowqulU3w3V4iiecFScBunYlii5bZC9UAnfILUAorbJPia6rNDPA==", null, false, "1ded6fd3-c729-439f-bde4-92c33d2cbb47", false, "rodolfo@gmail.com" },
                    { "4be251a3-a379-45ab-a14d-ea1e4655f9ba", 0, "28fbe989-8286-4d9f-b6fd-30bca2ba5d5d", "cristopher@gmail.com", true, false, null, "CRISTOPHER@GMAIL.COM", "CRISTOPHER@GMAIL.COM", "AQAAAAIAAYagAAAAEHELEyS9szOe+8AOcDMOFnbG8auqOGwnY9ZVcqTsFgyIo2y5YaTyP83Q6WoXbtafPA==", null, false, "f3955dde-83f5-4e0c-9925-5366814bf1b0", false, "cristopher@gmail.com" },
                    { "bed3f5b8-d532-4dc0-bbf2-218952642b33", 0, "a0db3c0d-1629-4d77-aa40-a6f33d5bd242", "fabian@gmail.com", true, false, null, "FABIAN@GMAIL.COM", "FABIAN@GMAIL.COM", "AQAAAAIAAYagAAAAELxbBRh4RuJlPQ4y2Gi8qqCivPLnrqsjYeAYPRzQoOFnkCDzBxEn/hbG/Q+Og9QrGA==", null, false, "5d4a10f7-3ef3-40d5-ab4d-9fe8ad500b81", false, "fabian@gmail.com" }
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
                    { "a1f17514-3467-4b7f-9822-4e0d6a6df446", "1daa661e-c084-4685-a1c4-1691d4e409ce" },
                    { "ab91b54e-7068-40eb-ba14-66bc140979e4", "4be251a3-a379-45ab-a14d-ea1e4655f9ba" },
                    { "01daa330-1e67-4302-b70e-d9ee9bc90b36", "bed3f5b8-d532-4dc0-bbf2-218952642b33" }
                });

            migrationBuilder.InsertData(
                table: "Distribuciones",
                columns: new[] { "Id_distribucion", "Destino", "Observaciones", "Ubicacion", "UserId" },
                values: new object[,]
                {
                    { 1, "Supermercado A", "Es una cargamento que hay que transportar con cuidado", "Rúa 21", "bed3f5b8-d532-4dc0-bbf2-218952642b33" },
                    { 2, "Supermercado ", "", "El Boule Garage", "4be251a3-a379-45ab-a14d-ea1e4655f9ba" },
                    { 3, "Frutería C", "Un buen pedido", "Lomito's Grill - Steak House", "bed3f5b8-d532-4dc0-bbf2-218952642b33" }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id_pedido", "Fecha", "Id_proveedor", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 11, 18, 18, 20, 428, DateTimeKind.Local).AddTicks(6142), 1, "bed3f5b8-d532-4dc0-bbf2-218952642b33" },
                    { 2, new DateTime(2025, 3, 11, 18, 18, 20, 428, DateTimeKind.Local).AddTicks(6160), 1, "bed3f5b8-d532-4dc0-bbf2-218952642b33" },
                    { 3, new DateTime(2025, 3, 11, 18, 18, 20, 428, DateTimeKind.Local).AddTicks(6163), 2, "bed3f5b8-d532-4dc0-bbf2-218952642b33" },
                    { 4, new DateTime(2025, 3, 11, 18, 18, 20, 428, DateTimeKind.Local).AddTicks(6165), 2, "bed3f5b8-d532-4dc0-bbf2-218952642b33" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id_producto", "Id_categoria", "Nombre" },
                values: new object[,]
                {
                    { 1, 3, "Culantro Coyote" },
                    { 2, 3, "Tonillo" },
                    { 3, 3, "Apio" },
                    { 4, 1, "Papaya" },
                    { 5, 1, "Manzana Verde" },
                    { 6, 1, "Manzana Roja" },
                    { 7, 2, "Brocoli" }
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
                    { 1, 330, 1 },
                    { 2, 325, 2 },
                    { 3, 320, 3 },
                    { 4, 350, 4 },
                    { 5, 360, 5 },
                    { 6, 380, 6 },
                    { 7, 390, 7 }
                });

            migrationBuilder.InsertData(
                table: "DetallesDistribuciones",
                columns: new[] { "Id_detalle_distribucion", "Cantidad", "Id_distribucion", "Id_inventario" },
                values: new object[,]
                {
                    { 1, 10, 1, 1 },
                    { 2, 15, 1, 2 },
                    { 3, 5, 2, 3 },
                    { 4, 25, 2, 1 },
                    { 5, 30, 2, 5 },
                    { 6, 40, 3, 6 },
                    { 7, 50, 3, 7 }
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
                name: "IX_DetallesDistribuciones_Id_distribucion",
                table: "DetallesDistribuciones",
                column: "Id_distribucion");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesDistribuciones_Id_inventario",
                table: "DetallesDistribuciones",
                column: "Id_inventario");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidos_Id_pedido",
                table: "DetallesPedidos",
                column: "Id_pedido");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidos_Id_producto",
                table: "DetallesPedidos",
                column: "Id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_Distribuciones_UserId",
                table: "Distribuciones",
                column: "UserId");

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
                name: "DetallesDistribuciones");

            migrationBuilder.DropTable(
                name: "DetallesPedidos");

            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Distribuciones");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
