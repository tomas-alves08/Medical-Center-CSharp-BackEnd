using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Center.Migrations
{
    /// <inheritdoc />
    public partial class CreateBookingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Payment_PaymentId1",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Bookings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 10, 18, 14, 39, 58, 594, DateTimeKind.Local).AddTicks(4258), new DateTime(2023, 10, 18, 14, 39, 58, 594, DateTimeKind.Local).AddTicks(4297) });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 10, 18, 14, 39, 58, 594, DateTimeKind.Local).AddTicks(4426), new DateTime(2023, 10, 18, 14, 39, 58, 594, DateTimeKind.Local).AddTicks(4428) });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 10, 18, 14, 39, 58, 594, DateTimeKind.Local).AddTicks(4413), new DateTime(2023, 10, 18, 14, 39, 58, 594, DateTimeKind.Local).AddTicks(4415) });

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Bookings_PaymentId1",
                table: "Appointments",
                column: "PaymentId1",
                principalTable: "Bookings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Bookings_PaymentId1",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "Payment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 10, 16, 15, 21, 16, 986, DateTimeKind.Local).AddTicks(3685), new DateTime(2023, 10, 16, 15, 21, 16, 986, DateTimeKind.Local).AddTicks(3732) });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 10, 16, 15, 21, 16, 986, DateTimeKind.Local).AddTicks(3859), new DateTime(2023, 10, 16, 15, 21, 16, 986, DateTimeKind.Local).AddTicks(3860) });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 10, 16, 15, 21, 16, 986, DateTimeKind.Local).AddTicks(3845), new DateTime(2023, 10, 16, 15, 21, 16, 986, DateTimeKind.Local).AddTicks(3847) });

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Payment_PaymentId1",
                table: "Appointments",
                column: "PaymentId1",
                principalTable: "Payment",
                principalColumn: "Id");
        }
    }
}
