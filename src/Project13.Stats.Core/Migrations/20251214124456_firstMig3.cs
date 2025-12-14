using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project13.Stats.Core.Migrations
{
    /// <inheritdoc />
    public partial class firstMig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detaljer_Summering_SvSpelInfoId",
                table: "Detaljer");

            migrationBuilder.DropIndex(
                name: "IX_Detaljer_SvSpelInfoId",
                table: "Detaljer");

            migrationBuilder.AddColumn<int>(
                name: "SummeringId",
                table: "Detaljer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Detaljer_SummeringId",
                table: "Detaljer",
                column: "SummeringId");

            migrationBuilder.AddForeignKey(
                name: "FK_Detaljer_Summering_SummeringId",
                table: "Detaljer",
                column: "SummeringId",
                principalTable: "Summering",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detaljer_Summering_SummeringId",
                table: "Detaljer");

            migrationBuilder.DropIndex(
                name: "IX_Detaljer_SummeringId",
                table: "Detaljer");

            migrationBuilder.DropColumn(
                name: "SummeringId",
                table: "Detaljer");

            migrationBuilder.CreateIndex(
                name: "IX_Detaljer_SvSpelInfoId",
                table: "Detaljer",
                column: "SvSpelInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Detaljer_Summering_SvSpelInfoId",
                table: "Detaljer",
                column: "SvSpelInfoId",
                principalTable: "Summering",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
