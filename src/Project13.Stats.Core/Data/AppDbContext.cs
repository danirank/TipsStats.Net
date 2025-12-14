using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project13.Stats.Core.Models;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Emit;

namespace Project13.Stats.Core.Data
{
    public class AppDbContext : DbContext
    {
       

        // Exempel på tabeller (DbSets)
        public DbSet<Detaljer> Detaljer { get; set; }
        public DbSet<Summering> Summering { get; set; }

        public DbSet<CalculationModel> Calculations => Set<CalculationModel>();
        public DbSet<MatchEntity> Matches => Set<MatchEntity>();


        public AppDbContext(DbContextOptions<AppDbContext> optionsBuilder) : base(optionsBuilder) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }

        }



    }
}
