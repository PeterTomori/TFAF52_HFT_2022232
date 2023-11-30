using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFAF52_HFT_2022232.Endpoint.Services;
using TFAF52_HFT_2022232.Logic;
using TFAF52_HFT_2022232.Models;

namespace TFAF52_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        IPlanetLogic logic;
        IHubContext<SignalRHub> hub;

        public PlanetController(IPlanetLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("PlanetCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Planet value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("PlanetUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var planetToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("PlanetDeleted", planetToDelete);
        }
    }
}
