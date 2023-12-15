using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Center_Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityUserWithoutBookingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 12, 12, 17, 19, 20, 530, DateTimeKind.Local).AddTicks(2816), new DateTime(2023, 12, 12, 17, 19, 20, 530, DateTimeKind.Local).AddTicks(2856) });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 12, 12, 17, 19, 20, 530, DateTimeKind.Local).AddTicks(2978), new DateTime(2023, 12, 12, 17, 19, 20, 530, DateTimeKind.Local).AddTicks(2979) });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 12, 12, 17, 19, 20, 530, DateTimeKind.Local).AddTicks(2965), new DateTime(2023, 12, 12, 17, 19, 20, 530, DateTimeKind.Local).AddTicks(2966) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 12, 12, 16, 48, 50, 785, DateTimeKind.Local).AddTicks(1480), new DateTime(2023, 12, 12, 16, 48, 50, 785, DateTimeKind.Local).AddTicks(1523) });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 12, 12, 16, 48, 50, 785, DateTimeKind.Local).AddTicks(1639), new DateTime(2023, 12, 12, 16, 48, 50, 785, DateTimeKind.Local).AddTicks(1640) });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 12, 12, 16, 48, 50, 785, DateTimeKind.Local).AddTicks(1627), new DateTime(2023, 12, 12, 16, 48, 50, 785, DateTimeKind.Local).AddTicks(1628) });
        }
    }
}
