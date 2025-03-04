using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Auth_API.Migrations
{
    /// <inheritdoc />
    public partial class Seeders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "184c37c1-2ca6-491a-a6ae-c3b8de381bbb", null, "Admin", "ADMIN" },
                    { "48bba5f4-7d2d-4546-98c0-6184379b1a02", null, "Empleado", "EMPLEADO" },
                    { "b4e48b19-4d9e-4eec-b1c5-d740df4ec0b0", null, "Distribuidor", "DISTRIBUIDOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2920f578-d47d-4db0-a19c-e79a2b66964a", 0, "f08a6889-fb85-4cd8-8220-94c3e585ce84", "cristopher@gmail.com", true, false, null, "CRISTOPHER@GMAIL.COM", "CRISTOPHER@GMAIL.COM", "AQAAAAIAAYagAAAAEIOhcueS1DAAuq5EUx4rSLpzJP9tbESr90mqqw07NvmWs3FfeIRwcLyHAkk23dIFcA==", null, false, "8ef5d70d-e4bb-4a76-93f7-c5d3990a9a7a", false, "cristopher@gmail.com" },
                    { "b55fc61e-5d69-41a1-8b4c-42914e40d3ec", 0, "bd871020-a7c8-4cd4-92ba-073566d43193", "rodolfo@gmail.com", true, false, null, "RODOLFO@GMAIL.COM", "RODOLFO@GMAIL.COM", "AQAAAAIAAYagAAAAEIBgM8tKE36uNhjarA3uBXjryz7SAsYYaO2BuU8mhZrDQA2QGuMaTMgYDjP6H/ee3Q==", null, false, "b1839345-af0d-4c43-88dc-36b322cf93f5", false, "rodolfo@gmail.com" },
                    { "f87f41b8-38f5-4505-a357-5ba5059345c9", 0, "190e43f5-2a47-464e-9bef-eecb31312d6e", "fabian@gmail.com", true, false, null, "FABIAN@GMAIL.COM", "FABIAN@GMAIL.COM", "AQAAAAIAAYagAAAAEFFQ/FrLvLaTBeStBfwl9jzg+gNlwEM+mBtBervfF0G90XRURmnTJnieJY6Kp+zfTw==", null, false, "0a795ef3-3e38-427f-92e2-2bdeafdca9ea", false, "fabian@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "48bba5f4-7d2d-4546-98c0-6184379b1a02", "2920f578-d47d-4db0-a19c-e79a2b66964a" },
                    { "b4e48b19-4d9e-4eec-b1c5-d740df4ec0b0", "b55fc61e-5d69-41a1-8b4c-42914e40d3ec" },
                    { "184c37c1-2ca6-491a-a6ae-c3b8de381bbb", "f87f41b8-38f5-4505-a357-5ba5059345c9" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "48bba5f4-7d2d-4546-98c0-6184379b1a02", "2920f578-d47d-4db0-a19c-e79a2b66964a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b4e48b19-4d9e-4eec-b1c5-d740df4ec0b0", "b55fc61e-5d69-41a1-8b4c-42914e40d3ec" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "184c37c1-2ca6-491a-a6ae-c3b8de381bbb", "f87f41b8-38f5-4505-a357-5ba5059345c9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "184c37c1-2ca6-491a-a6ae-c3b8de381bbb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48bba5f4-7d2d-4546-98c0-6184379b1a02");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4e48b19-4d9e-4eec-b1c5-d740df4ec0b0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2920f578-d47d-4db0-a19c-e79a2b66964a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b55fc61e-5d69-41a1-8b4c-42914e40d3ec");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f87f41b8-38f5-4505-a357-5ba5059345c9");
        }
    }
}
