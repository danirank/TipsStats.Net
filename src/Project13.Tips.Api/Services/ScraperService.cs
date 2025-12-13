using Microsoft.Playwright;
using Project13.Tips.Api.Constants;
using Project13.Tips.Api.Mappings;
using Project13.Tips.Api.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Project13.Tips.Api.Services
{
    public class ScraperService
    {
        public async Task<CouponDto> GetKupong(TipType tipsTyp)
        {
            var matchInfoList = new List<MatchInfo>();
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

            await page.WaitForSelectorAsync("div.pg_draw_card_container");

            string? weekText = null;

            var weekEl = await page.QuerySelectorAsync("div.pg_draw_card__title__content");
            if (weekEl is not null)
            {
                weekText = (await weekEl.TextContentAsync())?.Trim();
            }

            var weekNumber = ExtractFirstInt(weekText ?? "");

            string? turnoverText = null;

            var turnoverEl = await page.QuerySelectorAsync("span.currency-counter");
            if (turnoverEl is not null)
            {
                turnoverText = (await turnoverEl.TextContentAsync());
            }

            long turnover = 0;

            if (!string.IsNullOrWhiteSpace(turnoverText))
            {
                // normalisera texten
                var cleaned = turnoverText
                    .Replace("\u00A0", "")   // nbsp
                    .Replace(" ", "")        // vanlig space
                    .Trim();

                long.TryParse(cleaned, out turnover);
            }


            var coupon = new CouponDto
            {
                Week = weekNumber,
                Turnover = turnover,
                
            }; 

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

                        matchInfoList.Add(matchInfo);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fel vid parsning av match: {ex.Message}");
                }
            }

            coupon.Matches = matchInfoList.Select(m => m.ToDto()).ToList();

            return coupon;
        }

        private static double ParseDouble(string input, bool isPercent = false)
        {
            input = input.Replace("%", "").Replace(",", ".").Trim();
            return double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out var val) ? val : 0;
        }

        // "Vecka 50" -> 50
        private static int ExtractFirstInt(string text)
        {
            var m = Regex.Match(text ?? "", @"\d+");
            return m.Success ? int.Parse(m.Value) : 0;
        }

        // "11 816 074" -> 11816074 (tar bort nbsp/spaces och allt som inte är siffra)
        private static long ExtractDigitsAsLong(string text)
        {
            var digits = Regex.Replace(text ?? "", @"[^\d]", "");
            return long.TryParse(digits, out var val) ? val : 0;
        }

    }
    
}
