using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using TFAF52_HFT_2022232.Models;

namespace TFAF52_HFT_2022232.Client
{
    class Program
    {
        static RestService rest;

        static void List(string entity)
        {
            if (entity == "Ship")
            {
                List<Ship> ships = rest.Get<Ship>("ship");
                foreach (var item in ships)
                {
                    Console.WriteLine($"{item.ShipId} {item.ShipName} {item.ShipType}");
                }
            }
            if (entity == "Company")
            {
                List<Company> companies = rest.Get<Company>("company");
                foreach (var item in companies)
                {
                    Console.WriteLine($"{item.CompanyId} {item.CompanyName} {item.Faction}");
                }
            }
            if (entity == "Planet")
            {
                List<Planet> planets = rest.Get<Planet>("planet");
                foreach (var item in planets)
                {
                    Console.WriteLine($"{item.PlanetId} {item.PlanetName}");
                }
            }
            Console.ReadLine();
        }

        static void Create(string entity)
        {
            if (entity == "Ship")
            {
                Console.Write("Enter the new ship's ID: ");
                int shipID = int.Parse(Console.ReadLine());
                Console.Write("Enter the ship's name: ");
                string shipName = Console.ReadLine();
                Console.Write("Enter the ship's type: ");
                string type = Console.ReadLine();
                rest.Post(new Ship() { ShipId = shipID, ShipName = shipName, ShipType = type }, "ship");
                Console.WriteLine("\n***New ship created!***");
            }
            else if (entity == "Company")
            {
                Console.Write("Enter the new company's ID: ");
                int companyID = int.Parse(Console.ReadLine());
                Console.Write("Enter the company's name: ");
                string companyName = Console.ReadLine();
                Console.Write("Enter the company's faction: ");
                string faction = Console.ReadLine();
                rest.Post(new Company() { CompanyId = companyID, CompanyName = companyName, Faction = faction }, "company");
                Console.WriteLine("\n***New company created!***");
            }
            else if (entity == "Planet")
            {
                Console.Write("Enter the new planet's ID: ");
                int planetID = int.Parse(Console.ReadLine());
                Console.Write("Enter the planet's name: ");
                string planetName = Console.ReadLine();
                rest.Post(new Planet() { PlanetId = planetID, PlanetName = planetName}, "planet");
                Console.WriteLine("\n***New planet created!***");
            }
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }

        static void Update(string entity)
        {
            if (entity == "Ship")
            {
                Console.Write("Enter the ship's ID you want to update: ");
                int shipID = int.Parse(Console.ReadLine());
                Ship shipToBeUpdated = rest.Get<Ship>(shipID, "ship");
                Console.Write($"Enter the new name for this ship [old name: {shipToBeUpdated.ShipName}]: ");
                string newName = Console.ReadLine();
                Console.Write($"Enter the new type for this ship [old type: {shipToBeUpdated.ShipType}]: ");
                string newType = Console.ReadLine();
                shipToBeUpdated.ShipName = newName;
                shipToBeUpdated.ShipType = newType;
                rest.Put(shipToBeUpdated, "ship");
                Console.WriteLine("\n***Ship updated!***");
            }
            else if (entity == "Company")
            {
                Console.Write("Enter the company's ID you want to update: ");
                int companyID = int.Parse(Console.ReadLine());
                Company companyToBeUpdated = rest.Get<Company>(companyID, "company");
                Console.Write($"Enter the new name for this company [old name: {companyToBeUpdated.CompanyName}]: ");
                string newName = Console.ReadLine();
                Console.Write($"Enter the new faction for this company [old faction: {companyToBeUpdated.Faction}]: ");
                string newFaction = Console.ReadLine();
                companyToBeUpdated.CompanyName = newName;
                companyToBeUpdated.Faction = newFaction;
                rest.Put(companyToBeUpdated, "company");
                Console.WriteLine("\n***Company updated!***");
            }
            else if (entity == "Planet")
            {
                Console.Write("Enter the planet's ID you want to update: ");
                int planetID = int.Parse(Console.ReadLine());
                Planet planetToBeUpdated = rest.Get<Planet>(planetID, "planet");
                Console.Write($"Enter the new name for this planet [old name: {planetToBeUpdated.PlanetName}]: ");
                string newName = Console.ReadLine();
                Console.Write($"Enter the new company's for this planet [old companyID: {planetToBeUpdated.CompanyId}]: ");
                int newcompanyID = int.Parse(Console.ReadLine());
                planetToBeUpdated.PlanetName = newName;
                planetToBeUpdated.CompanyId = newcompanyID;
                rest.Put(planetToBeUpdated, "planet");
                Console.WriteLine("\n***Planet updated!***");
            }
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }

        static void Delete(string entity)
        {
            if (entity == "Ship")
            {
                Console.Write("Enter the ship's ID you want to delete: ");
                int shipID = int.Parse(Console.ReadLine());
                rest.Delete(shipID, "ship");
                Console.WriteLine("\n***Ship destroyed!***");
            }
            if (entity == "Company")
            {
                Console.Write("Enter the company's ID you want to delete: ");
                int companyID = int.Parse(Console.ReadLine());
                rest.Delete(companyID, "company");
                Console.WriteLine("\n***Company deleted!***");
            }
            if (entity == "Planet")
            {
                Console.Write("Enter the planet's ID you want to delete: ");
                int planetID = int.Parse(Console.ReadLine());
                rest.Delete(planetID, "planet");
                Console.WriteLine("\n***Planet deleted!***");
            }
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }

        static void NonCruds(string entity, string method)
        {
            if (method == "ShipManufacturers")
            {
                Console.Write("Enter the ship's name, it will return its Company: ");
                string shipName = Console.ReadLine();
                var companies = rest.Get<Company>("Stat/ShipManufacturers/" + shipName);
                var company = companies[0];
                Console.WriteLine($"Here is the ship's Company: {company.CompanyName}");
                Console.WriteLine($"Here is the ship's Company: {company.CompanyId}");
                Console.WriteLine($"Here is the ship's Company: {company.Faction}");
            }
            else if (method == "ShipFactions")
            {
                List<FactionCounted> factions = rest.Get<FactionCounted>("Stat/ShipFactions/");
                Console.WriteLine();
                foreach (var item in factions)
                {
                    Console.WriteLine($"{item.Faction}: {item.ShipCount}");
                }
            }
            else if (method == "OwnedByCompany")
            {
                Console.Write("Give a company, whose planets you wish to know: ");
                string companyName = Console.ReadLine();
                var result = rest.Get<Planet>("Stat/OwnedByCompany/" + companyName);
                foreach (var item in result)
                {
                    Console.WriteLine(item.PlanetName);
                }
                Console.WriteLine("\n***Planet(s) listed!***");
            }
            else if (method == "ShipOfFactions")
            {
                Console.Write("Enter the faction's name: ");
                string faction = Console.ReadLine();
                List<Ship> result = rest.Get<Ship>("Stat/ShipOfFactions/" + faction);
                foreach (var item in result)
                {
                    Console.WriteLine(item.ShipName);
                }
                Console.WriteLine("\n***Task completed!***");
            }
            else if (method == "OwnerOfPlanet")
            {
                Console.WriteLine("Returns the owner company of a planet.");
                Console.Write("Enter a planet's name: ");
                string companyName = Console.ReadLine();
                var result = rest.Get<Company>("Stat/OwnerOfPlanet/" + companyName);
                foreach (var item in result)
                {
                    Console.WriteLine(item.CompanyId + ", " + item.CompanyName + ", " + item.Faction);
                }
                Console.WriteLine("\n***Company listed!***");
            }
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:27110/", "swagger");

            var shipSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Ship"))
                .Add("Create", () => Create("Ship"))
                .Add("Update", () => Update("Ship"))
                .Add("Delete", () => Delete("Ship"))
                .Add("Exit", ConsoleMenu.Close);

            var companySubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Company"))
                .Add("Create", () => Create("Company"))
                .Add("Update", () => Update("Company"))
                .Add("Delete", () => Delete("Company"))
                .Add("Exit", ConsoleMenu.Close);

            var planetSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Planet"))
                .Add("Create", () => Create("Planet"))
                .Add("Update", () => Update("Planet"))
                .Add("Delete", () => Delete("Planet"))
                .Add("Exit", ConsoleMenu.Close);

            var noncrudSubMenu = new ConsoleMenu(args, level: 1)
                .Add("ShipManufacturers", () => NonCruds("Ship", "ShipManufacturers"))
                .Add("ShipFactions", () => NonCruds("Ship", "ShipFactions"))
                .Add("OwnedByCompany", () => NonCruds("Company", "OwnedByCompany"))
                .Add("ShipOfFactions", () => NonCruds("Company", "ShipOfFactions"))
                .Add("OwnerOfPlanet", () => NonCruds("Planet", "OwnerOfPlanet"))
                .Add("Exit", ConsoleMenu.Close);

            var mainMenu = new ConsoleMenu(args, level: 0)
                .Add("Ships", () => shipSubMenu.Show())
                .Add("Companies", () => companySubMenu.Show())
                .Add("Planets", () => planetSubMenu.Show())
                .Add("Non-Cruds", () => noncrudSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            mainMenu.Show();
        }
    }
}
