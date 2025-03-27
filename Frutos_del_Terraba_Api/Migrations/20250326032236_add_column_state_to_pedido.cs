using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Frutos_del_Terraba_Api.Migrations
{
    /// <inheritdoc />
    public partial class add_column_state_to_pedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a1f17514-3467-4b7f-9822-4e0d6a6df446", "1daa661e-c084-4685-a1c4-1691d4e409ce" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ab91b54e-7068-40eb-ba14-66bc140979e4", "4be251a3-a379-45ab-a14d-ea1e4655f9ba" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "01daa330-1e67-4302-b70e-d9ee9bc90b36", "bed3f5b8-d532-4dc0-bbf2-218952642b33" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01daa330-1e67-4302-b70e-d9ee9bc90b36");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1f17514-3467-4b7f-9822-4e0d6a6df446");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab91b54e-7068-40eb-ba14-66bc140979e4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1daa661e-c084-4685-a1c4-1691d4e409ce");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4be251a3-a379-45ab-a14d-ea1e4655f9ba");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bed3f5b8-d532-4dc0-bbf2-218952642b33");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Pedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3f3c70b6-d761-448a-8f97-e9a5656c4cfb", null, "Distribuidor", "DISTRIBUIDOR" },
                    { "5da2f21c-ddd4-4ef5-8522-87c89ab24a5b", null, "Empleado", "EMPLEADO" },
                    { "d9ef6f11-555d-4e1d-a904-cc79bfc6af70", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "89f1dd0a-89b9-43eb-9942-0eb1ad9aa080", 0, "bfd180d9-2398-452e-bf5d-8451bd64fcc7", "fabian@gmail.com", true, false, null, "FABIAN@GMAIL.COM", "FABIAN@GMAIL.COM", "AQAAAAIAAYagAAAAEJXxWtz2lHWt0uEiOWqIFzEwFqbVMCk2Yu9A06gt3nXJazWA1Ys7iiU3PbcmhRIdAg==", null, false, "411d6224-9bbb-4bc6-984e-997622acca07", false, "fabian@gmail.com" },
                    { "b944e074-7724-4999-9e2d-11835200c4ef", 0, "9de8deee-8941-49b7-aa4c-a0eea6455c58", "cristopher@gmail.com", true, false, null, "CRISTOPHER@GMAIL.COM", "CRISTOPHER@GMAIL.COM", "AQAAAAIAAYagAAAAEPhFqlUEKHW8oEz+VGLRdRVKGQo9UBjAK4ZEKUK4BB+kQw8GfWHtBXWC0mmAqIVOrw==", null, false, "a7b6c4e3-57ce-4128-9166-d69d99dffae0", false, "cristopher@gmail.com" },
                    { "c6bd8445-378e-4eb6-a62d-1740c14957cc", 0, "1e8a3148-de68-41d1-8323-6a7632ba777f", "rodolfo@gmail.com", true, false, null, "RODOLFO@GMAIL.COM", "RODOLFO@GMAIL.COM", "AQAAAAIAAYagAAAAENSBZnl6drrA/ifPGAhj4KuiWGzRyTTFmS+/h7r0jFXozjz7JG365+zqLjC+Wc+OFw==", null, false, "ef68e0ba-0d27-4500-8249-6655b95fe43c", false, "rodolfo@gmail.com" }
                });

            migrationBuilder.UpdateData(
                table: "Distribuciones",
                keyColumn: "Id_distribucion",
                keyValue: 1,
                column: "UserId",
                value: "89f1dd0a-89b9-43eb-9942-0eb1ad9aa080");

            migrationBuilder.UpdateData(
                table: "Distribuciones",
                keyColumn: "Id_distribucion",
                keyValue: 2,
                column: "UserId",
                value: "b944e074-7724-4999-9e2d-11835200c4ef");

            migrationBuilder.UpdateData(
                table: "Distribuciones",
                keyColumn: "Id_distribucion",
                keyValue: 3,
                column: "UserId",
                value: "89f1dd0a-89b9-43eb-9942-0eb1ad9aa080");

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id_pedido",
                keyValue: 1,
                columns: new[] { "Estado", "Fecha", "UserId" },
                values: new object[] { false, new DateTime(2025, 3, 25, 21, 22, 35, 895, DateTimeKind.Local).AddTicks(8965), "89f1dd0a-89b9-43eb-9942-0eb1ad9aa080" });

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id_pedido",
                keyValue: 2,
                columns: new[] { "Estado", "Fecha", "UserId" },
                values: new object[] { false, new DateTime(2025, 3, 25, 21, 22, 35, 895, DateTimeKind.Local).AddTicks(8987), "89f1dd0a-89b9-43eb-9942-0eb1ad9aa080" });

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id_pedido",
                keyValue: 3,
                columns: new[] { "Estado", "Fecha", "UserId" },
                values: new object[] { false, new DateTime(2025, 3, 25, 21, 22, 35, 895, DateTimeKind.Local).AddTicks(8988), "89f1dd0a-89b9-43eb-9942-0eb1ad9aa080" });

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id_pedido",
                keyValue: 4,
                columns: new[] { "Estado", "Fecha", "UserId" },
                values: new object[] { false, new DateTime(2025, 3, 25, 21, 22, 35, 895, DateTimeKind.Local).AddTicks(8989), "89f1dd0a-89b9-43eb-9942-0eb1ad9aa080" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "d9ef6f11-555d-4e1d-a904-cc79bfc6af70", "89f1dd0a-89b9-43eb-9942-0eb1ad9aa080" },
                    { "5da2f21c-ddd4-4ef5-8522-87c89ab24a5b", "b944e074-7724-4999-9e2d-11835200c4ef" },
                    { "3f3c70b6-d761-448a-8f97-e9a5656c4cfb", "c6bd8445-378e-4eb6-a62d-1740c14957cc" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d9ef6f11-555d-4e1d-a904-cc79bfc6af70", "89f1dd0a-89b9-43eb-9942-0eb1ad9aa080" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5da2f21c-ddd4-4ef5-8522-87c89ab24a5b", "b944e074-7724-4999-9e2d-11835200c4ef" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3f3c70b6-d761-448a-8f97-e9a5656c4cfb", "c6bd8445-378e-4eb6-a62d-1740c14957cc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f3c70b6-d761-448a-8f97-e9a5656c4cfb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5da2f21c-ddd4-4ef5-8522-87c89ab24a5b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9ef6f11-555d-4e1d-a904-cc79bfc6af70");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89f1dd0a-89b9-43eb-9942-0eb1ad9aa080");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b944e074-7724-4999-9e2d-11835200c4ef");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6bd8445-378e-4eb6-a62d-1740c14957cc");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Pedidos");

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

            migrationBuilder.UpdateData(
                table: "Distribuciones",
                keyColumn: "Id_distribucion",
                keyValue: 1,
                column: "UserId",
                value: "bed3f5b8-d532-4dc0-bbf2-218952642b33");

            migrationBuilder.UpdateData(
                table: "Distribuciones",
                keyColumn: "Id_distribucion",
                keyValue: 2,
                column: "UserId",
                value: "4be251a3-a379-45ab-a14d-ea1e4655f9ba");

            migrationBuilder.UpdateData(
                table: "Distribuciones",
                keyColumn: "Id_distribucion",
                keyValue: 3,
                column: "UserId",
                value: "bed3f5b8-d532-4dc0-bbf2-218952642b33");

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id_pedido",
                keyValue: 1,
                columns: new[] { "Fecha", "UserId" },
                values: new object[] { new DateTime(2025, 3, 11, 18, 18, 20, 428, DateTimeKind.Local).AddTicks(6142), "bed3f5b8-d532-4dc0-bbf2-218952642b33" });

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id_pedido",
                keyValue: 2,
                columns: new[] { "Fecha", "UserId" },
                values: new object[] { new DateTime(2025, 3, 11, 18, 18, 20, 428, DateTimeKind.Local).AddTicks(6160), "bed3f5b8-d532-4dc0-bbf2-218952642b33" });

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id_pedido",
                keyValue: 3,
                columns: new[] { "Fecha", "UserId" },
                values: new object[] { new DateTime(2025, 3, 11, 18, 18, 20, 428, DateTimeKind.Local).AddTicks(6163), "bed3f5b8-d532-4dc0-bbf2-218952642b33" });

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id_pedido",
                keyValue: 4,
                columns: new[] { "Fecha", "UserId" },
                values: new object[] { new DateTime(2025, 3, 11, 18, 18, 20, 428, DateTimeKind.Local).AddTicks(6165), "bed3f5b8-d532-4dc0-bbf2-218952642b33" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "a1f17514-3467-4b7f-9822-4e0d6a6df446", "1daa661e-c084-4685-a1c4-1691d4e409ce" },
                    { "ab91b54e-7068-40eb-ba14-66bc140979e4", "4be251a3-a379-45ab-a14d-ea1e4655f9ba" },
                    { "01daa330-1e67-4302-b70e-d9ee9bc90b36", "bed3f5b8-d532-4dc0-bbf2-218952642b33" }
                });
        }
    }
}
