using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionDiscovery.Infrastructure.Migrations
{
	public partial class AdjustDomainTransactionAndOperation : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				 name: "Fee",
				 table: "Transactions");

			migrationBuilder.DropColumn(
				 name: "SequenceNumber",
				 table: "Transactions");

			migrationBuilder.AddColumn<string>(
				 name: "Hash",
				 table: "Transactions",
				 type: "nvarchar(max)",
				 nullable: true);


			migrationBuilder.AddColumn<string>(
				name: "Id_Temp",
				table: "Operations",
				type: "bigint",
				nullable: false);

			migrationBuilder.DropUniqueConstraint("PK_Operations", "Operations");
			migrationBuilder.DropColumn(
				name: "Id",
				table: "Operations");

			migrationBuilder.RenameColumn("Id_Temp", "Operations", "Id");
			migrationBuilder.AddPrimaryKey("PK_Operations", "Operations", "Id");

			migrationBuilder.AddColumn<decimal>(
				 name: "Amount",
				 table: "Operations",
				 type: "decimal(18,2)",
				 nullable: false,
				 defaultValue: 0m);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				 name: "Hash",
				 table: "Transactions");

			migrationBuilder.DropColumn(
				 name: "Amount",
				 table: "Operations");

			migrationBuilder.AddColumn<decimal>(
				 name: "Fee",
				 table: "Transactions",
				 type: "decimal(18,2)",
				 nullable: false,
				 defaultValue: 0m);

			migrationBuilder.AddColumn<long>(
				 name: "SequenceNumber",
				 table: "Transactions",
				 type: "bigint",
				 nullable: false,
				 defaultValue: 0L);

			migrationBuilder.AlterColumn<Guid>(
				 name: "Id",
				 table: "Operations",
				 type: "uniqueidentifier",
				 nullable: false,
				 oldClrType: typeof(long),
				 oldType: "bigint")
				 .OldAnnotation("SqlServer:Identity", "1, 1");
		}
	}
}
