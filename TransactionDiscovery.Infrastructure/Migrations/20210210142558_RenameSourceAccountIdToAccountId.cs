using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionDiscovery.Infrastructure.Migrations
{
    public partial class RenameSourceAccountIdToAccountId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_SourceAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SequenceNumber",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "SourceAccountId",
                table: "Transactions",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SourceAccountId",
                table: "Transactions",
                newName: "IX_Transactions_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Transactions",
                newName: "SourceAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                newName: "IX_Transactions_SourceAccountId");

            migrationBuilder.AddColumn<decimal>(
                name: "SequenceNumber",
                table: "Accounts",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_SourceAccountId",
                table: "Transactions",
                column: "SourceAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
