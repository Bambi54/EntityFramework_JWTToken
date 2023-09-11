using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CW8.Migrations
{
    /// <inheritdoc />
    public partial class Accounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Username);
                });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 23, 16, 6, 11, 220, DateTimeKind.Utc).AddTicks(36), new DateTime(2023, 5, 23, 16, 6, 11, 220, DateTimeKind.Utc).AddTicks(37) });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 23, 16, 6, 11, 220, DateTimeKind.Utc).AddTicks(39), new DateTime(2023, 5, 23, 16, 6, 11, 220, DateTimeKind.Utc).AddTicks(40) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 17, 18, 59, 28, 424, DateTimeKind.Utc).AddTicks(2235), new DateTime(2023, 5, 17, 18, 59, 28, 424, DateTimeKind.Utc).AddTicks(2236) });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 17, 18, 59, 28, 424, DateTimeKind.Utc).AddTicks(2239), new DateTime(2023, 5, 17, 18, 59, 28, 424, DateTimeKind.Utc).AddTicks(2240) });
        }
    }
}
