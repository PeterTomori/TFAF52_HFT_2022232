using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFAF52_HFT_2022232.Logic;
using TFAF52_HFT_2022232.Models;

namespace TFAF52_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        IPlanetLogic logic;

        public PlanetController(IPlanetLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Planet> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Planet Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Planet value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] Planet value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
