using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForecastExceptionPortal.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exceptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LocationName = table.Column<string>(type: "TEXT", nullable: false),
                    BusinessDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Sales = table.Column<decimal>(type: "TEXT", nullable: false),
                    ExpectedSales = table.Column<decimal>(type: "TEXT", nullable: false),
                    SalesVariancePct = table.Column<decimal>(type: "TEXT", nullable: false),
                    Customers = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpectedCustomers = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomersVariancePct = table.Column<decimal>(type: "TEXT", nullable: false),
                    ExceptionScore = table.Column<decimal>(type: "TEXT", nullable: false),
                    Severity = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    AssignedTo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exceptions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exceptions");
        }
    }
}
