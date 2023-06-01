using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFAF52_HFT_2022232.Logic;
using TFAF52_HFT_2022232.Models;
using TFAF52_HFT_2022232.Repository;

namespace TFAF52_HFT_2022232.Test
{
    [TestFixture]
    public class CompanyTester
    {
        CompanyLogic cl;
        Mock<IRepository<Company>> mockCompanyRepository;

        [SetUp]
        public void Init()
        {
            mockCompanyRepository = new Mock<IRepository<Company>>();

            Company company1 = new Company(1, "CompanyA", "AAA");
            Company company2 = new Company(2, "CompanyB", "AAA");

            Ship ship1 = new Ship(1, "ShipA", "Frigate", 1);
            Ship ship2 = new Ship(2, "ShipB", "Starfighter", 1);

            Planet planet = new Planet(1, "PlanetA", 1);

            company1.Ships.Add(ship1);
            company1.Ships.Add(ship2);

            company1.Planets.Add(planet);

            mockCompanyRepository.Setup(c => c.ReadAll()).Returns(new List<Company>()
            {
                company1, company2
            }.AsQueryable());

            cl = new CompanyLogic(mockCompanyRepository.Object);
        }

        [Test]
        public void OwnedByCompanyTester()
        {
            var result = cl.OwnedByCompany("CompanyA").ToArray();

            Assert.That(result[0].PlanetId == 1 &&
                        result[0].PlanetName == "PlanetA");
        }

        [Test]
        public void ShipOfFactionsTester()
        {
            var result = cl.ShipOfFactions("AAA").ToArray();

            Assert.That(result[0].ShipId == 1 &&
                        result[0].ShipName == "ShipA");
            Assert.That(result[1].ShipId == 2 &&
                        result[1].ShipName == "ShipB");
        }
    }
}
