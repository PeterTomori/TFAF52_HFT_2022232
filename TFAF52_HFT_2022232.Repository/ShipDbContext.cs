using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFAF52_HFT_2022232.Models;

namespace TFAF52_HFT_2022232.Repository
{
    public class ShipDbContext : DbContext
    {
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Company> Companies { get; set; }

        public ShipDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("star wars")
                              .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Planet>(planet => planet
                        .HasOne(planet => planet.Company)
                        .WithMany(company => company.Planets)
                        .HasForeignKey(planet => planet.CompanyId)
                        .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Ship>(ship => ship
                        .HasOne(ship => ship.Company)
                        .WithMany(company => company.Ships)
                        .HasForeignKey(ship => ship.CompanyId)
                        .OnDelete(DeleteBehavior.Cascade));



            modelBuilder.Entity<Planet>().HasData(new Planet[]
            {
                new Planet(){PlanetId = 1, PlanetName = "Mon Cala" , CompanyId = 1 },
                new Planet(){PlanetId = 2, PlanetName = "Kuat" , CompanyId = 2},
                new Planet(){PlanetId = 3, PlanetName = "Corellia" , CompanyId = 3},
                new Planet(){PlanetId = 4, PlanetName = "Pammant" , CompanyId = 4},
                new Planet(){PlanetId = 5, PlanetName = "Esseles" , CompanyId = 5},
                new Planet(){PlanetId = 6, PlanetName = "Absanz" , CompanyId = 6},
                new Planet(){PlanetId = 7, PlanetName = "Ryloth" , CompanyId = 7},
                new Planet(){PlanetId = 8, PlanetName = "Castell" , CompanyId = 6},
                new Planet(){PlanetId = 9, PlanetName = "Lothal" , CompanyId = 2},
                new Planet(){PlanetId = 10, PlanetName = "Naboo" , CompanyId = 8},
                new Planet(){PlanetId = 11, PlanetName = "Rothana" , CompanyId = 2},
                new Planet(){PlanetId = 12, PlanetName = "Fresia" , CompanyId = 10},
                new Planet(){PlanetId = 13, PlanetName = "Nimban" , CompanyId = 11},
                new Planet(){PlanetId = 14, PlanetName = "Colla" , CompanyId = 12},
                new Planet(){PlanetId = 15, PlanetName = "Koensayr" , CompanyId = 9},
            });

            modelBuilder.Entity<Company>().HasData(new Company[]
            {
                new Company(){CompanyId = 1, CompanyName = "Mon Calamari Shipyards", Faction = "Rebel Alliance" },
                new Company(){CompanyId = 2, CompanyName = "Kuat Drive Yards", Faction = "Galactic Empire" },
                new Company(){CompanyId = 3, CompanyName = "Corellian Engineering Corporation", Faction = "Rebel Alliance" },
                new Company(){CompanyId = 4, CompanyName = "Free Dac Volunteers Engineering Corps", Faction = "CIS" },
                new Company(){CompanyId = 5, CompanyName = "Damorian Manufacturing Corporation", Faction = "Galactic Empire" },
                new Company(){CompanyId = 6, CompanyName = "Sienar Fleet Systems", Faction = "Galactic Empire" },
                new Company(){CompanyId = 7, CompanyName = "Zann Consortium", Faction = "Zann Consortium" },
                new Company(){CompanyId = 8, CompanyName = "Theed Palace Space Vessel Engineering Corps", Faction = "Galactic Republic" },
                new Company(){CompanyId = 9, CompanyName = "Koensayr Manufacturing", Faction = "Rebel Alliance" },
                new Company(){CompanyId = 10, CompanyName = "Incom Corporation", Faction = "Rebel Alliance" },
                new Company(){CompanyId = 11, CompanyName = "Hoersch-Kessel Drive, Inc.", Faction = "CIS"},
                new Company(){CompanyId = 12, CompanyName = "Phlac-Arphocc Automata Industries", Faction = "CIS" }
            });

            modelBuilder.Entity<Ship>().HasData(new Ship[]
            {
                new Ship(){ShipId = 1, ShipName = "Mon Calamari Cruiser", ShipType = "Capital Ship", CompanyId = 1},
                new Ship(){ShipId = 2, ShipName = "EF76 Nebulon-B escort frigate", ShipType = "Frigate", CompanyId = 2},
                new Ship(){ShipId = 3, ShipName = "CR90 corvette", ShipType = "Corvette", CompanyId = 3},
                new Ship(){ShipId = 4, ShipName = "X-wing starfighter", ShipType = "Starfighter", CompanyId = 10},
                new Ship(){ShipId = 5, ShipName = "BTL Y-wing starfighter", ShipType = "Starfighter", CompanyId = 9},
                new Ship(){ShipId = 6, ShipName = "Executor-class Star Dreadnought", ShipType = "Dreadnought", CompanyId = 2},
                new Ship(){ShipId = 7, ShipName = "Imperial II-class Star Destroyer", ShipType = "Capital Ship", CompanyId = 2},
                new Ship(){ShipId = 8, ShipName = "Victory II-class Star Destroyer", ShipType = "Frigate", CompanyId = 2},
                new Ship(){ShipId = 9, ShipName = "Tartan-class patrol cruiser", ShipType = "Corvette", CompanyId = 5},
                new Ship(){ShipId = 10, ShipName = "TIE Fighter", ShipType = "Starfighter", CompanyId = 6},
                new Ship(){ShipId = 11, ShipName = "TIE Bomber", ShipType = "Starfighter", CompanyId = 6},
                new Ship(){ShipId = 12, ShipName = "TIE Interceptor", ShipType = "Starfighter", CompanyId = 6},
                new Ship(){ShipId = 13, ShipName = "Aggressor-class Star Destroyer", ShipType = "Capital ship", CompanyId = 7},
                new Ship(){ShipId = 14, ShipName = "Interceptor IV Frigate", ShipType = "Frigate", CompanyId = 3},
                new Ship(){ShipId = 15, ShipName = "Vengeance-class frigate ", ShipType = "Frigate", CompanyId = 3},
                new Ship(){ShipId = 16, ShipName = "VenatorI-classStarDestroyer", ShipType = "Capital ship", CompanyId = 2},
                new Ship(){ShipId = 17, ShipName = "Delta-7 Aethersprite-class light interceptor", ShipType = "Starfighter", CompanyId = 2},
                new Ship(){ShipId = 18, ShipName = "Lucrehulk-class LH-3210 cargo freighter", ShipType = "Capital ship", CompanyId = 11},
                new Ship(){ShipId = 19, ShipName = "Providence-class Dreadnought", ShipType = "Capital ship", CompanyId = 4},
                new Ship(){ShipId = 20, ShipName = "Droid tri-fighter", ShipType = "Starfighter", CompanyId = 12},
                new Ship(){ShipId = 21, ShipName = "N-1 starfighter", ShipType = "Starfighter", CompanyId = 8}
            });
        }
    }
}
