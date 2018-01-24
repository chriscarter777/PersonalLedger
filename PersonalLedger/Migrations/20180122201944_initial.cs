using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PersonalLedger.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Debit = table.Column<bool>(nullable: false),
                    DefaultAcct = table.Column<int>(nullable: true),
                    DefaultAmt = table.Column<decimal>(type: "money", nullable: true),
                    DefaultCat = table.Column<int>(nullable: true),
                    Institution = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    Interest = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    Limit = table.Column<decimal>(type: "money", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    Owned = table.Column<bool>(nullable: false),
                    User = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    Tax = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    User = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Category = table.Column<int>(nullable: false),
                    CrAcct = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    DrAcct = table.Column<int>(nullable: false),
                    Tax = table.Column<bool>(nullable: false),
                    User = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
