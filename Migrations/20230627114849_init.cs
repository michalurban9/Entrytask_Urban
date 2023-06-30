using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entrytask_Urban.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GetSalelinesTransactions",
                columns: table => new
                {
                    SaleLineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleLineTransactionKey = table.Column<int>(type: "int", nullable: false),
                    ItemNumber = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    VATRate = table.Column<double>(type: "float", nullable: false),
                    VAT = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetSalelinesTransactions", x => x.SaleLineId);
                });

            migrationBuilder.CreateTable(
                name: "GetSalesTransactions",
                columns: table => new
                {
                    TransactionKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketNumber = table.Column<int>(type: "int", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CashierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetSalesTransactions", x => x.TransactionKey);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetSalelinesTransactions");

            migrationBuilder.DropTable(
                name: "GetSalesTransactions");
        }
    }
}
