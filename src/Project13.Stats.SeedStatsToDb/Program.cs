using Project13.Stats.Core.Data;
using Project13.Stats.Core.Models;
using Project13.Stats.DataSeeder.Services;
using Microsoft.EntityFrameworkCore;

namespace Project13.Stats.SeedStatsToDb
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var conntctionstring = "Data Source=LAPTOP-5VBOLG0M\\SQLEXPRESS;Database=Project13Stats;Trusted_Connection=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(conntctionstring)
                .Options;
            var db = new AppDbContext(options);

            List<Summering> summering = new();
            List<Detaljer> detaljer = new();
            


            while (true)
            {


                Console.Clear();
                Console.WriteLine("-----Seeda statistik för Stryktipset, Europatipset och Topptipset------");
                Console.WriteLine("1. Ladda filer");
                Console.WriteLine("2. Skicka till DB");
                Console.WriteLine("3. Läs statistik");
                Console.WriteLine("0. Exit");
                Console.Write("Select option: ");


                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        //Ladda in filer
                        (summering, detaljer) = LoadFiles();
                        break;

                    case "2":

                        //Skicka data till DB
                        await WriteToDatabase(db, summering, detaljer);
                        break;

                    case "3":
                        //Läs statistik från DB
                        await ReadStatisticsFromDatabase(db);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again.");

                        break;
                }
                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey();
            }


        }

        //Ladda filer
        public static (List<Summering>, List<Detaljer>) LoadFiles()
        {
            Console.WriteLine();
            // Implementation for loading files
            Console.WriteLine("=== Tryck på länken för att ladda ner senaste statistiken ===");
            Console.WriteLine("Stryk/Eu Summering: https://static.tipsxtra.se/downloads/TipsXtra_Stryk_Euro_Statistik_Summering.csv");
            Console.WriteLine("Stryk/Eu Detaljer: https://static.tipsxtra.se/downloads/TipsXtra_Stryk_Euro_Statistik_Detaljer.csv");
            Console.WriteLine();
            Console.Write("Ange sökväg till Surreings fil: ");
            string filePathSummering = Console.ReadLine();

            while (!File.Exists(filePathSummering))
            {
                Console.WriteLine("Filen hittades inte. Vänligen ange en giltig sökväg.");
                Console.Write("Ange sökväg till nedladdad fil: ");
                filePathSummering = Console.ReadLine();
            }

            Console.WriteLine($"Filen '{filePathSummering}' har laddats in");

            var summering = LoadCsvService<Summering>.Loader(filePathSummering);


            Console.WriteLine("Hämtade rader i summering: " + summering.Count);

            Console.WriteLine();
            Console.Write("Ange sökväg till Detalj fil: ");
            string filePath = Console.ReadLine();

            while (!File.Exists(filePath))
            {
                Console.WriteLine("Filen hittades inte. Vänligen ange en giltig sökväg.");
                Console.Write("Ange sökväg till nedladdad fil: ");
                filePath = Console.ReadLine();
            }

            Console.WriteLine($"Filen '{filePath}' har laddats in");

            var detaljer = LoadCsvService<Detaljer>.Loader(filePath);

            Console.WriteLine("Hämtade rader i detaljer: " + detaljer.Count);

            return (summering, detaljer);
        }



        //Skriv till DB
    public static async Task WriteToDatabase(
    AppDbContext dbContext,
    List<Summering> summering,
    List<Detaljer> detaljer)
        {
            try
            {
                Console.WriteLine("Startar skrivning till databas...");

                // Hämta redan befintliga nycklar EN gång
                var befintligaSummeringIds = await dbContext.Summering
                    .Select(s => s.Id)
                    .ToListAsync();



                var summeringSet = befintligaSummeringIds.ToHashSet();


                int addedSummering = 0;
                int addedDetaljer = 0;


                foreach (var sum in summering)
                {
                    // initiera navigation om den skulle vara null
                    sum.Matches ??= new List<Detaljer>();

                    if (!summeringSet.Contains(sum.Id))
                    {
                        var svsId = sum.Id;

                        var matchedDetaljer = detaljer
                            .Where(d => d.SvSpelInfoId == svsId)
                            .ToList();

                        foreach (var match in matchedDetaljer)
                        {
                            sum.Matches.Add(match);
                            addedDetaljer++;
                        }

                        dbContext.Summering.Add(sum);
                        addedSummering++;
                    }


                }

                Console.WriteLine($"Summering att lägga in: {addedSummering}");
                Console.WriteLine($"Detaljer att koppla:   {addedDetaljer}");


                await dbContext.SaveChangesAsync();
                Console.WriteLine("Skrivning klar utan fel.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid skrivning till databasen!");
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("InnerException:");
                    Console.WriteLine(ex.InnerException.Message);
                }
                throw; // låt felet bubbla upp så du ser det i PMC också
            }
        }


        public static async Task ReadStatisticsFromDatabase(AppDbContext dbContext)
        {
            // Implementation for reading statistics from the database
            var totalSummering = await dbContext.Summering.CountAsync();
            Console.WriteLine($"Totalt antal Summering poster i databasen: {totalSummering}");

            var totalDetaljer = await dbContext.Detaljer.CountAsync();
            Console.WriteLine($"Totalt antal Detaljer poster i databasen: {totalDetaljer}");

        }
    }
}
