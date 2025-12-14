using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project13.Stats.Core.Models
{
    public sealed record CalculationDto
(
    int Id,
    int SvSpelInfoId,
    int? Vecka,
    string Tips,

    decimal? Utdelning13,
    int? Antal13,
    decimal? Omsattning,

    decimal MultipliceradeOddsMarknad,
    decimal MultipliceradeOddsSvFolket,

    decimal OddsKvotTotal,
    decimal OddsKvotPerMatch,

    decimal OddsFavoritrad,
    decimal OddsFavoritradSvF,
    decimal KvotFavoritRad,

    int AntalStorfavoriter,
    int AntalMellanFavoriter,
    int AntalStorfavoriterVinst,
    int AntalMellanFavoriterVinst,

    int TotFavoriter,
    int TotVinstFav,
    int TotDiff,

     bool StrongestFavoriteWon,
     decimal? StrongestFavoriteOdds,
     decimal StrongestFavoriteDisagreement,
     int StrongestFavoriteMatchNumber ,

    int AntalSkrällar,
    decimal SkrallIndexProcent,
    decimal FavoritIndexProcent,

    decimal StorFavoritTraffProcent,
    decimal MellanFavoritTraffProcent,

    decimal FolketsFelProcent,
    decimal OddsetFelProcent,

    decimal MeanDisagreement,
    decimal MaxDisagreement,
    decimal DisagreementTop3Sum,

    IReadOnlyList<MatchDto> Matcher
);


}
