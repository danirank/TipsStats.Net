using Project13.Stats.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project13.Stats.Core.Services
{
    public class CalculationService
    {


        public List<CalculationModel> Calculate(
        List<Summering> sum,
        List<Detaljer> det)
        {
            var result = new List<CalculationModel>();

            // Grupp per omgång
            var grupperade = det.GroupBy(m => m.SvSpelInfoId);

            foreach (var grupp in grupperade)
            {
                var summering = sum.FirstOrDefault(s => s.Id == grupp.Key);
                if (summering == null)
                    continue;

                var first = grupp.First();
                var calc = new CalculationModel
                {
                    Id = grupp.Key,
                    Tips = first.Produktnamn ?? "",
                    Vecka = first.Vecka,
                    Utdelning13 = summering.Utd13,
                    Antal13 = summering.Ant13,
                    Omsattning = summering.Turnover
                };

                // --- Variabler för beräkning ---
                decimal produktOddsMarknad = 1m;
                decimal produktOddsFolk = 1m;
                decimal produktFavorit = 1m;
                decimal produktFavoritFolk = 1m;

                int antalStor = 0, antalMellan = 0;
                int vStor = 0, vMellan = 0;
                int total = 0;
                int skrällar = 0;

                var disagreements = new List<decimal>();

                // --- Matchloop ---
                foreach (var match in grupp)
                {
                    total++;

                    decimal odd1 = match.Oddset1 ?? 0m;
                    decimal oddX = match.OddsetX ?? 0m;
                    decimal odd2 = match.Oddset2 ?? 0m;

                    decimal folk1 = match.SvenskaFolketOdds1 ?? 0m;
                    decimal folkX = match.SvenskaFolketOddsX ?? 0m;
                    decimal folk2 = match.SvenskaFolketOdds2 ?? 0m;

                    decimal chosenMarknad = match.Utfall switch
                    {
                        "1" => odd1,
                        "X" => oddX,
                        "2" => odd2,
                        _ => 1m
                    };

                    decimal chosenFolk = match.Utfall switch
                    {
                        "1" => folk1,
                        "X" => folkX,
                        "2" => folk2,
                        _ => 1m
                    };

                    // Favoritklassning
                    bool stor = IsBetween(odd1, 0, 1.5m) ||
                                IsBetween(oddX, 0, 1.5m) ||
                                IsBetween(odd2, 0, 1.5m);

                    bool mellan = IsBetween(odd1, 1.5m, 2.1m) ||
                                  IsBetween(oddX, 1.5m, 2.1m) ||
                                  IsBetween(odd2, 1.5m, 2.1m);

                    if (stor) antalStor++;
                    if (mellan) antalMellan++;

                    if (stor && chosenMarknad < 1.5m) vStor++;
                    if (mellan && chosenMarknad >= 1.5m && chosenMarknad < 2.1m) vMellan++;

                    // Produkter
                    if (chosenMarknad > 0) produktOddsMarknad *= chosenMarknad;
                    if (chosenFolk > 0) produktOddsFolk *= chosenFolk;

                    var minMarknad = MinPositive(odd1, oddX, odd2);
                    var minFolk = MinPositive(folk1, folkX, folk2);

                    if (minMarknad > 0) produktFavorit *= minMarknad;
                    if (minFolk > 0) produktFavoritFolk *= minFolk;

                    // --- Skrällar (oddset > 4.0) ---
                    if (chosenMarknad > 4.0m)
                        skrällar++;

                    // --- Disagreement: |folket% - oddsprocent| ---
                    decimal pOdds = match.Utfall switch
                    {
                        "1" => match.OddsetProcent1 ?? 0m,
                        "X" => match.OddsetProcentX ?? 0m,
                        "2" => match.OddsetProcent2 ?? 0m,
                        _ => 0m
                    };

                    decimal pFolk = match.Utfall switch
                    {
                        "1" => match.SvenskaFolket1 ?? 0m,
                        "X" => match.SvenskaFolketX ?? 0m,
                        "2" => match.SvenskaFolket2 ?? 0m,
                        _ => 0m
                    };

                    decimal disagreement = Math.Abs(pFolk - pOdds);
                    disagreements.Add(disagreement);

                    // --- MatchDto ---
                    calc.Matcher.Add(new MatchDto
                    {
                        Matchnummer = match.Matchnummer,
                        Hemmalag = match.Hemmalag,
                        Bortalag = match.Bortalag,
                        Utfall = match.Utfall,
                        Oddset1 = match.Oddset1,
                        OddsetX = match.OddsetX,
                        Oddset2 = match.Oddset2,
                        SvenskaFolket1 = match.SvenskaFolketOdds1,
                        SvenskaFolketX = match.SvenskaFolketOddsX,
                        SvenskaFolket2 = match.SvenskaFolketOdds2,
                        Disagreement = disagreement
                    });
                }

                // --- Efter loop ---
                int favSum = antalStor + antalMellan;
                int vFav = vStor + vMellan;

                calc.AntalStorfavoriter = antalStor;
                calc.AntalMellanFavoriter = antalMellan;
                calc.AntalStorfavoriterVinst = vStor;
                calc.AntalMellanFavoriterVinst = vMellan;
                calc.TotFavoriter = favSum;
                calc.TotVinstFav = vFav;
                calc.TotDiff = favSum - vFav;
                calc.AntalSkrällar = skrällar;

                calc.SkrallIndexProcent = total > 0 ? (decimal)skrällar / total * 100m : 0;
                calc.FavoritIndexProcent = total > 0 ? (decimal)favSum / total * 100m : 0;

                calc.MultipliceradeOddsMarknad = Math.Round(produktOddsMarknad, 2);
                calc.MultipliceradeOddsSvFolket = Math.Round(produktOddsFolk, 2);

                calc.OddsFavoritrad = Math.Round(produktFavorit, 2);
                calc.OddsFavoritradSvF = Math.Round(produktFavoritFolk, 2);

                calc.KvotFavoritRad = produktFavoritFolk > 0
                    ? Math.Round(produktFavorit / produktFavoritFolk, 2)
                    : 0;

                calc.OddsKvotTotal = produktOddsMarknad > 0
                    ? Math.Round(produktOddsFolk / produktOddsMarknad, 2)
                    : 0;

                // --- Ny: Geometriskt medel för odds-kvot ---
                calc.OddsKvotPerMatch = Math.Round(
                    (decimal)Math.Pow((double)calc.OddsKvotTotal, 1.0 / total), 3);

                // --- Disagreement ---
                calc.MeanDisagreement = disagreements.Any() ? disagreements.Average() : 0;
                calc.MaxDisagreement = disagreements.Any() ? disagreements.Max() : 0;
                calc.DisagreementTop3Sum = disagreements
                    .OrderByDescending(x => x)
                    .Take(3)
                    .Sum();

                // --- Summering-relaterat felindex ---
                int peopleRight = summering.PeopleWasRight ?? 0;
                int peopleWrong = summering.PeopleWasWrong ?? 0;
                int peopleTotal = peopleRight + peopleWrong;

                calc.FolketsFelProcent = peopleTotal > 0
                    ? Math.Round((decimal)peopleWrong / peopleTotal * 100, 1)
                    : 0;

                int oddestRight = summering.OddsetWasRight ?? 0;
                int oddestWrong = summering.OddsetWasWrong ?? 0;
                int oddestTotal = oddestRight + oddestWrong;

                calc.OddsetFelProcent = oddestTotal > 0
                    ? Math.Round((decimal)oddestWrong / oddestTotal * 100, 1)
                    : 0;

                calc.StorFavoritTraffProcent = antalStor > 0
                    ? Math.Round((decimal)vStor / antalStor * 100, 1)
                    : 0;

                calc.MellanFavoritTraffProcent = antalMellan > 0
                    ? Math.Round((decimal)vMellan / antalMellan * 100, 1)
                    : 0;

                result.Add(calc);
            }

            return result;
        }

        private bool IsBetween(decimal value, decimal min, decimal max)
            => value > min && value < max;

        private static decimal MinPositive(params decimal[] values)
        {
            return values.Where(v => v > 0).DefaultIfEmpty(0).Min();
        }
    }
}
