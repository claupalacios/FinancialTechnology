using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialTechnology.Migrations
{
    /// <inheritdoc />
    public partial class AddedAccountInfoColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountNumber",
                table: "Account",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "Account",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "Account");
        }
    }
}
