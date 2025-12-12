using Microsoft.Playwright;
using SvSWebApiTips.Constants;
using SvSWebApiTips.Models;
using System.Globalization;

namespace SvSWebApiTips.Services
{
    public class ScraperService
    {
        public async Task<List<MatchInfo>> GetKupong(TipType tipsTyp)
        {
            var resultat = new List<MatchInfo>();
            string url = tipsTyp switch
            {
                TipType.Stryktipset => $"https://www.spela.svenskaspel.se/{TipsNamn.Stryktipset}",
                TipType.Europatipset => $"https://www.spela.svenskaspel.se/{TipsNamn.Europatipset}",
                TipType.Topptipset => $"https://www.spela.svenskaspel.se/{TipsNamn.Topptipset}",
                _ => throw new ArgumentOutOfRangeException(nameof(tipsTyp))
            };

            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            await page.GotoAsync(url);

            var kupong = await page.WaitForSelectorAsync("li.coupon-row-container");
            var matchElements = await page.QuerySelectorAllAsync("li.coupon-row-container");

            foreach (var match in matchElements)
            {
                try
                {
                    var matchNrText = await match.QuerySelectorAsync(".coupon-row-number");
                    var home = await match.QuerySelectorAsync(".home-participant");
                    var away = await match.QuerySelectorAsync(".away-participant");

                    var procentDivs = await match.QuerySelectorAllAsync("[data-testid='coupon-row-tips-info-svenska-folket'] .stat-trend");
                    var oddsDivs = await match.QuerySelectorAllAsync("[data-testid='coupon-row-tips-info-odds'] .stat-trend");

                    if (procentDivs.Count == 3 && oddsDivs.Count == 3)
                    {
                        var matchInfo = new MatchInfo
                        {
                            Matchnummer = int.Parse(await matchNrText.InnerTextAsync()),
                            Hemmalag = await home.InnerTextAsync(),
                            Bortalag = await away.InnerTextAsync(),
                            Odds1 = ParseDouble(await oddsDivs[0].InnerTextAsync()),
                            OddsX = ParseDouble(await oddsDivs[1].InnerTextAsync()),
                            Odds2 = ParseDouble(await oddsDivs[2].InnerTextAsync()),
                            SvenskaFolketProcent1 = ParseDouble(await procentDivs[0].InnerTextAsync(), true),
                            SvenskaFolketProcentX = ParseDouble(await procentDivs[1].InnerTextAsync(), true),
                            SvenskaFolketProcent2 = ParseDouble(await procentDivs[2].InnerTextAsync(), true)
                        };

                        resultat.Add(matchInfo);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fel vid parsning av match: {ex.Message}");
                }
            }

            return resultat;
        }

        private static double ParseDouble(string input, bool isPercent = false)
        {
            input = input.Replace("%", "").Replace(",", ".").Trim();
            return double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out var val) ? val : 0;
        }

    }
    
}
