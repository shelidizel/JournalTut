using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JournalAPI.Migrations
{
    /// <inheritdoc />
    public partial class freshstart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descrition = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    ExchangeCurrencyId = table.Column<int>(type: "int", nullable: true),
                    ExchangeRate = table.Column<decimal>(type: "smallmoney", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currencies_Currencies_ExchangeCurrencyId",
                        column: x => x.ExchangeCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    SupplierEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Journals",
                columns: table => new
                {
                    JournalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JournalNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JournalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    BaseCurrencyId = table.Column<int>(type: "int", nullable: false),
                    PoCurrencyId = table.Column<int>(type: "int", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "smallmoney", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "smallmoney", nullable: false),
                    QuotationNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    QuotationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentTerms = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journals", x => x.JournalID);
                    table.ForeignKey(
                        name: "FK_Journals_Currencies_BaseCurrencyId",
                        column: x => x.BaseCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Journals_Currencies_PoCurrencyId",
                        column: x => x.PoCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Journals_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Cost = table.Column<decimal>(type: "smallmoney", nullable: false),
                    Price = table.Column<decimal>(type: "smallmoney", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JournalPL",
                columns: table => new
                {
                    JournalPID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Isstart = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    JournalID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalPL", x => x.JournalPID);
                    table.ForeignKey(
                        name: "FK_JournalPL_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JournalPL_Journals_JournalID",
                        column: x => x.JournalID,
                        principalTable: "Journals",
                        principalColumn: "JournalID");
                    table.ForeignKey(
                        name: "FK_JournalPL_Units_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JournalBS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "nvarchar(6)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fob = table.Column<decimal>(type: "smallmoney", nullable: false),
                    PrcInBaseCurr = table.Column<decimal>(type: "smallmoney", nullable: false),
                    JournalID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalBS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalBS_Journals_JournalID",
                        column: x => x.JournalID,
                        principalTable: "Journals",
                        principalColumn: "JournalID");
                    table.ForeignKey(
                        name: "FK_JournalBS_Products_ProductCode",
                        column: x => x.ProductCode,
                        principalTable: "Products",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_ExchangeCurrencyId",
                table: "Currencies",
                column: "ExchangeCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalBS_JournalID",
                table: "JournalBS",
                column: "JournalID");

            migrationBuilder.CreateIndex(
                name: "IX_JournalBS_ProductCode",
                table: "JournalBS",
                column: "ProductCode");

            migrationBuilder.CreateIndex(
                name: "IX_JournalPL_AccountId",
                table: "JournalPL",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalPL_JournalID",
                table: "JournalPL",
                column: "JournalID");

            migrationBuilder.CreateIndex(
                name: "IX_JournalPL_UnitID",
                table: "JournalPL",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_BaseCurrencyId",
                table: "Journals",
                column: "BaseCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_PoCurrencyId",
                table: "Journals",
                column: "PoCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_SupplierID",
                table: "Journals",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalBS");

            migrationBuilder.DropTable(
                name: "JournalPL");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Journals");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
