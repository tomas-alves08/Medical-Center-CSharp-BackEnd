using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Center.Migrations
{
    /// <inheritdoc />
    public partial class CreatePaymentAndBookingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    IsPayed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PatientId",
                table: "Bookings",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 9, 20, 14, 25, 8, 242, DateTimeKind.Local).AddTicks(2721), new DateTime(2023, 9, 20, 14, 25, 8, 242, DateTimeKind.Local).AddTicks(2762) });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 9, 20, 14, 25, 8, 242, DateTimeKind.Local).AddTicks(2885), new DateTime(2023, 9, 20, 14, 25, 8, 242, DateTimeKind.Local).AddTicks(2886) });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 9, 20, 14, 25, 8, 242, DateTimeKind.Local).AddTicks(2872), new DateTime(2023, 9, 20, 14, 25, 8, 242, DateTimeKind.Local).AddTicks(2874) });
        }
    }
}
