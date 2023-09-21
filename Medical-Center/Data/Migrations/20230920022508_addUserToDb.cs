using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Center.Migrations
{
    /// <inheritdoc />
    public partial class addUserToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUsers", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalUsers");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2023, 9, 11, 15, 25, 29, 906, DateTimeKind.Local).AddTicks(7434), new DateTime(2023, 9, 11, 15, 25, 29, 906, DateTimeKind.Local).AddTicks(7493) });

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
    }
}
