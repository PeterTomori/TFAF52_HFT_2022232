using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFAF52_HFT_2022232.Logic;
using TFAF52_HFT_2022232.Models;
using TFAF52_HFT_2022232.Repository;

namespace TFAF52_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShipController : ControllerBase
    {
        IShipLogic logic;

        public ShipController(IShipLogic logic)
        {
            this.logic = logic;
        }


        [HttpGet]
        public IEnumerable<Ship> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Ship Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Ship value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] Ship value)
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
