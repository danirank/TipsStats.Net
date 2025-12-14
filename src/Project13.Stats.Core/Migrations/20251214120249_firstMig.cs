using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project13.Stats.Core.Migrations
{
    /// <inheritdoc />
    public partial class firstMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calculations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SvSpelInfoId = table.Column<int>(type: "int", nullable: false),
                    Tips = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vecka = table.Column<int>(type: "int", nullable: true),
                    Utdelning13 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Antal13 = table.Column<int>(type: "int", nullable: true),
                    Omsattning = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MultipliceradeOddsMarknad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MultipliceradeOddsSvFolket = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OddsKvotTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OddsKvotPerMatch = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OddsFavoritrad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OddsFavoritradSvF = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KvotFavoritRad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AntalStorfavoriter = table.Column<int>(type: "int", nullable: false),
                    AntalMellanFavoriter = table.Column<int>(type: "int", nullable: false),
                    AntalStorfavoriterVinst = table.Column<int>(type: "int", nullable: false),
                    AntalMellanFavoriterVinst = table.Column<int>(type: "int", nullable: false),
                    TotFavoriter = table.Column<int>(type: "int", nullable: false),
                    TotVinstFav = table.Column<int>(type: "int", nullable: false),
                    TotDiff = table.Column<int>(type: "int", nullable: false),
                    SkrallIndexProcent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AntalSkrällar = table.Column<int>(type: "int", nullable: false),
                    FavoritIndexProcent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StorFavoritTraffProcent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MellanFavoritTraffProcent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FolketsFelProcent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OddsetFelProcent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MeanDisagreement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxDisagreement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DisagreementTop3Sum = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calculations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Summering",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SvSpelInfoId = table.Column<int>(type: "int", nullable: false),
                    Vecka = table.Column<int>(type: "int", nullable: false),
                    Produktnamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectRow = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Turnover = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Utd13 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Utd12 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Utd11 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Utd10 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Ant13 = table.Column<int>(type: "int", nullable: true),
                    Ant12 = table.Column<int>(type: "int", nullable: true),
                    Ant11 = table.Column<int>(type: "int", nullable: true),
                    Ant10 = table.Column<int>(type: "int", nullable: true),
                    Count1 = table.Column<int>(type: "int", nullable: true),
                    CountX = table.Column<int>(type: "int", nullable: true),
                    Count2 = table.Column<int>(type: "int", nullable: true),
                    RandomResults = table.Column<int>(type: "int", nullable: true),
                    PeopleWasRight = table.Column<int>(type: "int", nullable: true),
                    OddsetWasRight = table.Column<int>(type: "int", nullable: true),
                    PeopleWasWrong = table.Column<int>(type: "int", nullable: true),
                    OddsetWasWrong = table.Column<int>(type: "int", nullable: true),
                    PeopleMaxSum = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OddsetMaxSum = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PeopleMinSum = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OddsetMinSum = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PeopleSum = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OddsetSum = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PeopleDiffSum = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OddsetDiffSum = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Odds1Ok = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Odds2Ok = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Odds3Ok = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    People1Ok = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    People2Ok = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    People3Ok = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Summering", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalculationModelId = table.Column<int>(type: "int", nullable: false),
                    Matchnummer = table.Column<int>(type: "int", nullable: false),
                    Hemmalag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bortalag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Utfall = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Oddset1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OddsetX = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Oddset2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SvenskaFolket1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SvenskaFolketX = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SvenskaFolket2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Disagreement = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Calculations_CalculationModelId",
                        column: x => x.CalculationModelId,
                        principalTable: "Calculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Detaljer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Produktnamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vecka = table.Column<int>(type: "int", nullable: false),
                    Omg = table.Column<int>(type: "int", nullable: false),
                    Matchnummer = table.Column<int>(type: "int", nullable: false),
                    Hemmalag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bortalag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hemmaresultat = table.Column<int>(type: "int", nullable: true),
                    Bortaresultat = table.Column<int>(type: "int", nullable: true),
                    Matchstart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Utfall = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Matchstatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experttips = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OddsetHigh = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OddsetLow = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OddsetDiff = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PeopleHigh = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PeopleLow = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PeopleDiff = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PeopleRank = table.Column<int>(type: "int", nullable: true),
                    OddsetRank = table.Column<int>(type: "int", nullable: true),
                    Oddset1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OddsetX = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Oddset2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OddsetProcent1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OddsetProcentX = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OddsetProcent2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SvenskaFolketOdds1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SvenskaFolketOddsX = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SvenskaFolketOdds2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SvenskaFolket1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SvenskaFolketX = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SvenskaFolket2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TioTidningar1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TioTidningarX = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TioTidningar2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PeopleWasRight = table.Column<int>(type: "int", nullable: true),
                    OddsetWasRight = table.Column<int>(type: "int", nullable: true),
                    PeopleWasWrong = table.Column<int>(type: "int", nullable: true),
                    OddsetWasWrong = table.Column<int>(type: "int", nullable: true),
                    ExpertWasRight = table.Column<int>(type: "int", nullable: true),
                    SvSpelInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detaljer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detaljer_Summering_SvSpelInfoId",
                        column: x => x.SvSpelInfoId,
                        principalTable: "Summering",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detaljer_SvSpelInfoId",
                table: "Detaljer",
                column: "SvSpelInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_CalculationModelId",
                table: "Matches",
                column: "CalculationModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detaljer");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Summering");

            migrationBuilder.DropTable(
                name: "Calculations");
        }
    }
}
