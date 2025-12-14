using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project13.Stats.DataSeeder.Services
{
    public static class LoadCsvService<T> where T : class
    {
        public static List<T> Loader(string filepath)
        {
            try
            {
                using var reader = new StreamReader(filepath);
                var config = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = ";"
                    
                };
                using var csv = new CsvHelper.CsvReader(reader, config);

                var records = csv.GetRecords<T>().ToList();

                return records;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading CSV file: {ex.Message}");
                return new List<T>();

            }
        }
    }
}
