using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFAF52_HFT_2022232.Logic;
using TFAF52_HFT_2022232.Models;


namespace TFAF52_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IShipLogic shipLogic;
        ICompanyLogic companyLogic;
        IPlanetLogic planetLogic;

        public StatController(IShipLogic shiplogic, ICompanyLogic companyLogic, IPlanetLogic planetLogic)
        {
            this.shipLogic = shiplogic;
            this.companyLogic = companyLogic;
            this.planetLogic = planetLogic;
        }

        [HttpGet]
        public IEnumerable<FactionCounted> ShipFactions()
        {
            return shipLogic.ShipFactions();
        }

        [HttpGet("{shipname}")]
        public IEnumerable<Company> ShipManufacturers(string shipname)
        {
            return shipLogic.ShipManufacturers(shipname);
        }
        
        [HttpGet("{faction}")]
        public IEnumerable<Ship> ShipOfFactions(string faction)
        {
            return companyLogic.ShipOfFactions(faction);
        }

        [HttpGet("{company}")]
        public IEnumerable<Planet> OwnedByCompany(string company)
        {
            return companyLogic.OwnedByCompany(company);
        }

        [HttpGet("{planet}")]
        public IEnumerable<Company> OwnerOfPlanet(string planet)
        {
            return planetLogic.OwnerOfPlanet(planet);
        }
    }
}
