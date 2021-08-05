using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Erefinance.Migrations
{
    public partial class initialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "district",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictCode = table.Column<int>(type: "int", nullable: false),
                    DistrictName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_district", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fund",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FundCode = table.Column<int>(type: "int", nullable: false),
                    FundName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fund", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sector",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sector", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "statusEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FundName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SMECategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypesofSector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisbursedFemaleEnterprise = table.Column<long>(type: "bigint", nullable: false),
                    DisbursedFemaleAmount = table.Column<long>(type: "bigint", nullable: false),
                    DisbursedMaleEnterprise = table.Column<long>(type: "bigint", nullable: false),
                    DisbursedMaleAmount = table.Column<long>(type: "bigint", nullable: false),
                    ApprovedFemaleEnterprise = table.Column<long>(type: "bigint", nullable: false),
                    ApprovedFemaleAmount = table.Column<long>(type: "bigint", nullable: false),
                    ApprovedMaleEnterprise = table.Column<long>(type: "bigint", nullable: false),
                    ApprovedMaleAmount = table.Column<long>(type: "bigint", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchCode = table.Column<int>(type: "int", nullable: false),
                    EntryBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statusEntry", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "district");

            migrationBuilder.DropTable(
                name: "fund");

            migrationBuilder.DropTable(
                name: "sector");

            migrationBuilder.DropTable(
                name: "statusEntry");
        }
    }
}
