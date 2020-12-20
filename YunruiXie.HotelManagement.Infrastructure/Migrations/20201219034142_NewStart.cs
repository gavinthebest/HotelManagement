using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YunruiXie.HotelManagement.Infrastructure.Migrations
{
    public partial class NewStart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ROOMTYPES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RTDESC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Rent = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROOMTYPES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ROOMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RTCODE = table.Column<int>(type: "int", nullable: true),
                    STATUS = table.Column<bool>(type: "bit", nullable: true),
                    RoomtypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROOMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ROOMS_ROOMTYPES_RoomtypeId",
                        column: x => x.RoomtypeId,
                        principalTable: "ROOMTYPES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMERS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ROOMNO = table.Column<int>(type: "int", nullable: true),
                    CNAME = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PHONE = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CHECKIN = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalPERSONS = table.Column<int>(type: "int", nullable: true),
                    BookingDays = table.Column<int>(type: "int", nullable: true),
                    ADVANCE = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CUSTOMERS_ROOMS_ROOMNO",
                        column: x => x.ROOMNO,
                        principalTable: "ROOMS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SERVICES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ROOMNO = table.Column<int>(type: "int", nullable: true),
                    SDESC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ServiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERVICES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SERVICES_ROOMS_RoomId",
                        column: x => x.RoomId,
                        principalTable: "ROOMS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMERS_ROOMNO",
                table: "CUSTOMERS",
                column: "ROOMNO",
                unique: true,
                filter: "[ROOMNO] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ROOMS_RoomtypeId",
                table: "ROOMS",
                column: "RoomtypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SERVICES_RoomId",
                table: "SERVICES",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CUSTOMERS");

            migrationBuilder.DropTable(
                name: "SERVICES");

            migrationBuilder.DropTable(
                name: "ROOMS");

            migrationBuilder.DropTable(
                name: "ROOMTYPES");
        }
    }
}
