using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project13.Stats.Core.Migrations
{
    /// <inheritdoc />
    public partial class addedKvotKorrektTecken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "KvotKorrektTecken",
                table: "Matches",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KvotKorrektTecken",
                table: "Matches");
        }
    }
}
