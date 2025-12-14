using Project13.Stats.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project13.Stats.Core.Services
{
    public class CalculationService
    {
        public List<CalculationModel> Calculate(List<Summering> sum, List<Detaljer> det)
        {
            var result = new List<CalculationModel>();

            var grupperade = det.GroupBy(m => m.SvSpelInfoId);

            foreach (var grupp in grupperade)
            {
                var summering = sum.FirstOrDefault(s => s.SvSpelInfoId == grupp.Key);
                if (summering == null) continue;

                var first = grupp.First();

                var calc = new CalculationModel
                {
                    SvSpelInfoId = grupp.Key,
                    Tips = first.Produktnamn ?? "",
                    Vecka = first.Vecka,
                    Utdelning13 = summering.Utd13,
                    Antal13 = summering.Ant13,
                    Omsattning = summering.Turnover,

                    // Säkra att listan finns:
                    Matcher = new List<MatchEntity>()
                };

                decimal produktOddsMarknad = 1m;
                decimal produktOddsFolk = 1m;
                decimal produktFavorit = 1m;
                decimal produktFavoritFolk = 1m;

                int antalStor = 0, antalMellan = 0;
                int vStor = 0, vMellan = 0;

                int bigOddsWinCount = 0;

                // Vi vill INTE dela på matcher som aldrig räknades in.
                int validMatches = 0;

                var disagreements = new List<decimal>();

                decimal strongestFavOdds = 0m;          // 0 => "ej satt"
                bool strongestFavWon = false;
                decimal strongestFavDisagreement = 0m;
                int strongestFavMatchNr = 0;

                foreach (var match in grupp)
                {
                    // --- Plocka odds (marknad + folk-odds) ---
                    decimal odd1 = match.Oddset1 ?? 0m;
                    decimal oddX = match.OddsetX ?? 0m;
                    decimal odd2 = match.Oddset2 ?? 0m;

                    decimal folkOdd1 = match.SvenskaFolketOdds1 ?? 0m;
                    decimal folkOddX = match.SvenskaFolketOddsX ?? 0m;
                    decimal folkOdd2 = match.SvenskaFolketOdds2 ?? 0m;

                    // Marknadens favorit (minsta positiva oddset)
                    var (favSign, favOdd) = GetFavoriteSignAndOdd(odd1, oddX, odd2);
                    if (favSign == null || favOdd <= 0m)
                    {
                        // Kan inte klassificera favorit utan marknadsodds
                        // men disagreement kan fortfarande räknas om procent finns:
                        AddDisagreementIfPossible(match, disagreements);
                        continue;
                    }

                    // Utfallets marknadsodds/folks odds
                    var utfall = match.Utfall;
                    if (utfall != "1" && utfall != "X" && utfall != "2")
                    {
                        AddDisagreementIfPossible(match, disagreements);
                        continue;
                    }

                    decimal chosenMarknad = utfall switch
                    {
                        "1" => odd1,
                        "X" => oddX,
                        "2" => odd2,
                        _ => 0m
                    };

                    decimal chosenFolk = utfall switch
                    {
                        "1" => folkOdd1,
                        "X" => folkOddX,
                        "2" => folkOdd2,
                        _ => 0m
                    };

                    // Vi räknar matchen som "valid" först när den kan bidra till odds/kvot
                    bool canUseForOddsProducts = chosenMarknad > 0m && chosenFolk > 0m;

                    decimal korrektTeckenKvot = 0m;
                    if (canUseForOddsProducts)
                    {
                        validMatches++;

                        produktOddsMarknad *= chosenMarknad;
                        produktOddsFolk *= chosenFolk;
                        korrektTeckenKvot = chosenFolk / chosenMarknad;

                        if (chosenMarknad > 4.0m)
                            bigOddsWinCount++;
                    }

                    // Favorit enligt folk (för favoritrad hos folket)
                    var (_, favOddFolk) = GetFavoriteSignAndOdd(folkOdd1, folkOddX, folkOdd2);

                    // Multiplicera favorit-rad (bara om odds finns)
                    if (favOdd > 0m) produktFavorit *= favOdd;
                    if (favOddFolk > 0m) produktFavoritFolk *= favOddFolk;

                    // Klassning baserat på FAVORITODDSET
                    bool stor = IsBetween(favOdd, 0m, 1.5m);
                    bool mellan = IsBetween(favOdd, 1.5m, 2.1m);

                    if (stor) antalStor++;
                    if (mellan) antalMellan++;

                    bool favoriteWon = match.Utfall == favSign;
                    if (stor && favoriteWon) vStor++;
                    if (mellan && favoriteWon) vMellan++;

                    // Strongest favorite (lägsta favoritoddset i omgången)
                    if (strongestFavOdds == 0m || favOdd < strongestFavOdds)
                    {
                        strongestFavOdds = favOdd;
                        strongestFavMatchNr = match.Matchnummer;
                        strongestFavWon = favoriteWon;

                        strongestFavDisagreement = favSign switch
                        {
                            "1" => Math.Abs((match.SvenskaFolket1 ?? 0m) - (match.OddsetProcent1 ?? 0m)),
                            "X" => Math.Abs((match.SvenskaFolketX ?? 0m) - (match.OddsetProcentX ?? 0m)),
                            "2" => Math.Abs((match.SvenskaFolket2 ?? 0m) - (match.OddsetProcent2 ?? 0m)),
                            _ => 0m
                        };
                    }

                    // Disagreement per match (max av |folk% - odds%|)
                    var disagreementMax = AddDisagreementIfPossible(match, disagreements);

                    // MatchEntity
                    calc.Matcher.Add(new MatchEntity
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
                        KvotKorrektTecken = korrektTeckenKvot,
                        Disagreement = disagreementMax
                    });
                }

                int favSum = antalStor + antalMellan;
                int vFav = vStor + vMellan;

                calc.AntalStorfavoriter = antalStor;
                calc.AntalMellanFavoriter = antalMellan;
                calc.AntalStorfavoriterVinst = vStor;
                calc.AntalMellanFavoriterVinst = vMellan;
                calc.TotFavoriter = favSum;
                calc.TotVinstFav = vFav;

                calc.StrongestFavoriteOdds = strongestFavOdds == 0m ? (decimal?)null : strongestFavOdds;
                calc.StrongestFavoriteWon = strongestFavWon;
                calc.StrongestFavoriteDisagreement = strongestFavDisagreement;
                calc.StrongestFavoriteMatchNumber = strongestFavMatchNr;

                calc.TotDiff = favSum - vFav;
                calc.AntalSkrällar = bigOddsWinCount;

                // Index ska baseras på antal matcher vi faktiskt räknade in för odds/kvot
                calc.SkrallIndexProcent = validMatches > 0 ? (decimal)bigOddsWinCount / validMatches * 100m : 0m;
                calc.FavoritIndexProcent = calc.Matcher.Count > 0 ? (decimal)favSum / calc.Matcher.Count * 100m : 0m;

                calc.MultipliceradeOddsMarknad = validMatches > 0 ? Math.Round(produktOddsMarknad, 2) : 0m;
                calc.MultipliceradeOddsSvFolket = validMatches > 0 ? Math.Round(produktOddsFolk, 2) : 0m;

                calc.OddsFavoritrad = Math.Round(produktFavorit, 2);
                calc.OddsFavoritradSvF = Math.Round(produktFavoritFolk, 2);

                calc.KvotFavoritRad = produktFavoritFolk > 0m
                    ? Math.Round(produktFavorit / produktFavoritFolk, 2)
                    : 0m;

                calc.OddsKvotTotal = (validMatches > 0 && produktOddsMarknad > 0m)
                    ? Math.Round(produktOddsFolk / produktOddsMarknad, 2)
                    : 0m;

                calc.OddsKvotPerMatch = validMatches > 0
                    ? Math.Round((decimal)Math.Pow((double)calc.OddsKvotTotal, 1.0 / validMatches), 3)
                    : 0m;

                calc.MeanDisagreement = disagreements.Any() ? disagreements.Average() : 0m;
                calc.MaxDisagreement = disagreements.Any() ? disagreements.Max() : 0m;
                calc.DisagreementTop3Sum = disagreements.OrderByDescending(x => x).Take(3).Sum();

                int peopleRight = summering.PeopleWasRight ?? 0;
                int peopleWrong = summering.PeopleWasWrong ?? 0;
                int peopleTotal = peopleRight + peopleWrong;

                calc.FolketsFelProcent = peopleTotal > 0
                    ? Math.Round((decimal)peopleWrong / peopleTotal * 100m, 1)
                    : 0m;

                int oddestRight = summering.OddsetWasRight ?? 0;
                int oddestWrong = summering.OddsetWasWrong ?? 0;
                int oddestTotal = oddestRight + oddestWrong;

                calc.OddsetFelProcent = oddestTotal > 0
                    ? Math.Round((decimal)oddestWrong / oddestTotal * 100m, 1)
                    : 0m;

                calc.StorFavoritTraffProcent = antalStor > 0
                    ? Math.Round((decimal)vStor / antalStor * 100m, 1)
                    : 0m;

                calc.MellanFavoritTraffProcent = antalMellan > 0
                    ? Math.Round((decimal)vMellan / antalMellan * 100m, 1)
                    : 0m;

                result.Add(calc);
            }

            return result;
        }

        private static decimal AddDisagreementIfPossible(Detaljer match, List<decimal> disagreements)
        {
            decimal oddsPct1 = match.OddsetProcent1 ?? 0m;
            decimal oddsPctX = match.OddsetProcentX ?? 0m;
            decimal oddsPct2 = match.OddsetProcent2 ?? 0m;

            decimal folkPct1 = match.SvenskaFolket1 ?? 0m;
            decimal folkPctX = match.SvenskaFolketX ?? 0m;
            decimal folkPct2 = match.SvenskaFolket2 ?? 0m;

            decimal d1 = Math.Abs(folkPct1 - oddsPct1);
            decimal dX = Math.Abs(folkPctX - oddsPctX);
            decimal d2 = Math.Abs(folkPct2 - oddsPct2);

            decimal disagreementMax = MaxOf(d1, dX, d2);
            disagreements.Add(disagreementMax);

            return disagreementMax;
        }

        private static bool IsBetween(decimal value, decimal minInclusive, decimal maxExclusive)
            => value >= minInclusive && value < maxExclusive;

        private static decimal MaxOf(decimal a, decimal b, decimal c)
            => Math.Max(a, Math.Max(b, c));

        // Returnerar (sign, odd) för minsta positiva oddset. Om inga odds är >0 => (null, 0)
        private static (string? Sign, decimal Odd) GetFavoriteSignAndOdd(decimal odd1, decimal oddX, decimal odd2)
        {
            decimal min = 0m;
            string? sign = null;

            if (odd1 > 0m) { min = odd1; sign = "1"; }
            if (oddX > 0m && (min == 0m || oddX < min)) { min = oddX; sign = "X"; }
            if (odd2 > 0m && (min == 0m || odd2 < min)) { min = odd2; sign = "2"; }

            return (sign, min);
        }
    }
}
