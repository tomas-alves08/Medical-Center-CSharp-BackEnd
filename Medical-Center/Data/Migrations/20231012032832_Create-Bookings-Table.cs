using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Center.Migrations
{
    /// <inheritdoc />
    public partial class CreateBookingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 10, 12, 16, 28, 32, 681, DateTimeKind.Local).AddTicks(1633), new DateTime(2023, 10, 12, 16, 28, 32, 681, DateTimeKind.Local).AddTicks(1675) });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 10, 12, 16, 28, 32, 681, DateTimeKind.Local).AddTicks(1825), new DateTime(2023, 10, 12, 16, 28, 32, 681, DateTimeKind.Local).AddTicks(1826) });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 10, 12, 16, 28, 32, 681, DateTimeKind.Local).AddTicks(1810), new DateTime(2023, 10, 12, 16, 28, 32, 681, DateTimeKind.Local).AddTicks(1812) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 10, 12, 16, 14, 41, 736, DateTimeKind.Local).AddTicks(2739), new DateTime(2023, 10, 12, 16, 14, 41, 736, DateTimeKind.Local).AddTicks(2783) });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 10, 12, 16, 14, 41, 736, DateTimeKind.Local).AddTicks(2912), new DateTime(2023, 10, 12, 16, 14, 41, 736, DateTimeKind.Local).AddTicks(2913) });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 10, 12, 16, 14, 41, 736, DateTimeKind.Local).AddTicks(2898), new DateTime(2023, 10, 12, 16, 14, 41, 736, DateTimeKind.Local).AddTicks(2900) });
        }
    }
}
