using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFAF52_HFT_2022232.Endpoint.Services;
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
        IHubContext<SignalRHub> hub;

        public ShipController(IShipLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("ShipCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Ship value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("ShipUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var shipToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("ShipDeleted", shipToDelete);
        }
    }
}
