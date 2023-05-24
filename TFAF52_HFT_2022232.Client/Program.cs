using System;
using System.Linq;
using TFAF52_HFT_2022232.Logic;
using TFAF52_HFT_2022232.Models;
using TFAF52_HFT_2022232.Repository;

namespace TFAF52_HFT_2022232.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var ctx = new ShipDbContext();

            var crepo = new CompanyRepository(ctx);
            var srepo = new ShipRepository(ctx);
            var prepo = new PlanetRepository(ctx);

            var companyLogic = new CompanyLogic(crepo);
            var shipLogic = new ShipLogic(srepo);
            var planetLogic = new PlanetLogic(prepo);

            var nc = planetLogic.OwnerOfPlanet("Lothal");
            ;

        }
    }
}
