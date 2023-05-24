using Moq;
using NUnit.Framework;
using System;
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



    }
}
