using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Center.Migrations
{
    /// <inheritdoc />
    public partial class DbContextWithFluentAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Patients",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Doctors",
                newName: "Id");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDateTime", "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 10, 17, 14, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 11, 15, 25, 29, 906, DateTimeKind.Local).AddTicks(7434), new DateTime(2023, 9, 11, 15, 25, 29, 906, DateTimeKind.Local).AddTicks(7493) });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 9, 11, 15, 25, 29, 906, DateTimeKind.Local).AddTicks(7717), new DateTime(2023, 9, 11, 15, 25, 29, 906, DateTimeKind.Local).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 9, 11, 15, 25, 29, 906, DateTimeKind.Local).AddTicks(7694), new DateTime(2023, 9, 11, 15, 25, 29, 906, DateTimeKind.Local).AddTicks(7698) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Patients",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Doctors",
                newName: "DoctorId");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDateTime", "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 9, 5, 16, 17, 34, 449, DateTimeKind.Local).AddTicks(6383), new DateTime(2023, 9, 5, 16, 17, 34, 449, DateTimeKind.Local).AddTicks(6429), new DateTime(2023, 9, 5, 16, 17, 34, 449, DateTimeKind.Local).AddTicks(6430) });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 9, 5, 16, 17, 34, 449, DateTimeKind.Local).AddTicks(6588), new DateTime(2023, 9, 5, 16, 17, 34, 449, DateTimeKind.Local).AddTicks(6589) });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 9, 5, 16, 17, 34, 449, DateTimeKind.Local).AddTicks(6549), new DateTime(2023, 9, 5, 16, 17, 34, 449, DateTimeKind.Local).AddTicks(6550) });
        }
    }
}
