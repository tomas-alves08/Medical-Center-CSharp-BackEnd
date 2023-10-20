using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Center.Migrations
{
    /// <inheritdoc />
    public partial class CreatePaymentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentId1",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPayed",
                table: "Payment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "PaymentId", "PaymentId1", "UpdateTime" },
                values: new object[] { new DateTime(2023, 10, 16, 15, 21, 16, 986, DateTimeKind.Local).AddTicks(3685), 0, null, new DateTime(2023, 10, 16, 15, 21, 16, 986, DateTimeKind.Local).AddTicks(3732) });

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

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PaymentId1",
                table: "Appointments",
                column: "PaymentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Payment_PaymentId1",
                table: "Appointments",
                column: "PaymentId1",
                principalTable: "Payment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Payment_PaymentId1",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PaymentId1",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PaymentId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsPayed",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PatientId",
                table: "Bookings",
                column: "PatientId");
        }
    }
}
