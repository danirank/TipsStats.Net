using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project13.Stats.Core.Migrations
{
    /// <inheritdoc />
    public partial class newColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "StrongestFavoriteDisagreement",
                table: "Calculations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StrongestFavoriteMatchNumber",
                table: "Calculations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "StrongestFavoriteOdds",
                table: "Calculations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "StrongestFavoriteWon",
                table: "Calculations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StrongestFavoriteDisagreement",
                table: "Calculations");

            migrationBuilder.DropColumn(
                name: "StrongestFavoriteMatchNumber",
                table: "Calculations");

            migrationBuilder.DropColumn(
                name: "StrongestFavoriteOdds",
                table: "Calculations");

            migrationBuilder.DropColumn(
                name: "StrongestFavoriteWon",
                table: "Calculations");
        }
    }
}
