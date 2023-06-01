using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TFAF52_HFT_2022232.Logic;
using TFAF52_HFT_2022232.Models;
using TFAF52_HFT_2022232.Repository;

namespace TFAF52_HFT_2022232.Test
{
    [TestFixture]
    public class Tester
    {
        ShipLogic sl;
        Mock<IRepository<Ship>> mockShipRepository;

        PlanetLogic pl;
        Mock<IRepository<Planet>> mockPlanetRepository;

        CompanyLogic cl;
        Mock<IRepository<Company>> mockCompanyRepository;

        [SetUp]
        public void Init()
        {
            mockShipRepository = new Mock<IRepository<Ship>>();
            mockCompanyRepository = new Mock<IRepository<Company>>();
            mockPlanetRepository = new Mock<IRepository<Planet>>();

            List<Company> companies = new List<Company>()
            {
                new Company(){ CompanyId = 1, CompanyName = "CompanyA", Faction = "ABC"},
                new Company(){ CompanyId = 2, CompanyName = "CompanyB", Faction = "ABC"},
                new Company(){ CompanyId = 3, CompanyName = "CompanyC", Faction = "AAA"}
            };

            mockCompanyRepository.Setup(c => c.ReadAll()).Returns(companies.AsQueryable());

            List<Ship> ships = new List<Ship>()
            {
                new Ship(){ ShipId = 1, ShipName = "ShipA", Company = companies[0]},
                new Ship(){ ShipId = 2, ShipName = "ShipB", Company = companies[1]},
                new Ship(){ ShipId = 3, ShipName = "ShipC", Company = companies[2]}
            };

            mockShipRepository.Setup(s => s.ReadAll()).Returns(ships.AsQueryable());

            List<Planet> planets = new List<Planet>()
            {
                new Planet(){ PlanetId = 1, PlanetName = "PlanetA",  Company = companies[0]},
                new Planet(){ PlanetId = 2, PlanetName = "PlanetB",  Company = companies[1]},
                new Planet(){ PlanetId = 3, PlanetName = "PlanetC",  Company = companies[2]}
            };

            mockPlanetRepository.Setup(p => p.ReadAll()).Returns(planets.AsQueryable());

            pl = new PlanetLogic(mockPlanetRepository.Object);

            cl = new CompanyLogic(mockCompanyRepository.Object);

            sl = new ShipLogic(mockShipRepository.Object);
        }

        [Test]
        public void CreateShipTestWithCorrectTitle()
        {
            var ship = new Ship() { ShipName = "ABCD" };
            sl.Create(ship);
            mockShipRepository.Verify(r => r.Create(ship), Times.Once);
        }

        [Test]
        public void CreateShipTestWithIncorrectTitle()
        {
            var ship = new Ship() { ShipName = "AB" };

            try
            {
                sl.Create(ship);
            }
            catch
            {
                
            }

            mockShipRepository.Verify(s => s.Create(ship), Times.Never);
        }

        [Test]
        public void CreatePlanetTest()
        {
            var planet = new Planet() { PlanetName = "Naboo" };
            pl.Create(planet);
            mockPlanetRepository.Verify(p => p.Create(planet), Times.Once);
        }

        [Test]
        public void CreateCompanyTest()
        {
            var company = new Company { CompanyName = "KFT" };
            cl.Create(company);
            mockCompanyRepository.Verify(c => c.Create(company), Times.Once);
        }

        [Test]
        public void ShipManufacturersTester()
        {
            var result = sl.ShipManufacturers("ShipA").ToArray();

            Assert.That(result[0].CompanyId == 1 &&
                        result[0].CompanyName == "CompanyA" &&
                        result[0].Faction == "ABC");
        }

        [Test]
        public void ShipFactionsTester_WithCorrectResult()
        {
            var result = sl.ShipFactions().ToArray();
            var expected = new List<FactionCounted>() { new FactionCounted() { Faction = "ABC", ShipCount = 2 } };

            Assert.That(result[0].Faction, Is.EqualTo(expected[0].Faction));
            Assert.That(result[0].ShipCount, Is.EqualTo(expected[0].ShipCount));
        }

        [Test]
        public void ShipFactionsTester_WithIncorrectResult()
        {
            var result = sl.ShipFactions().ToArray();
            var expected = new List<FactionCounted>() { new FactionCounted() { Faction = "ABC", ShipCount = 3 } };

            //Assert.That(result[0].Faction != expected[0].Faction);
            Assert.That(result[0].ShipCount != expected[0].ShipCount);
        }

        [Test]
        public void OwnerOfPlanetTester()
        {
            var result = pl.OwnerOfPlanet("PlanetA").ToArray();

            Assert.That(result[0].CompanyId == 1 &&
                        result[0].CompanyName == "CompanyA" &&
                        result[0].Faction == "ABC");
        }
    }
}
