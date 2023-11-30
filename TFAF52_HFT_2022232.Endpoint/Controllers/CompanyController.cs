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
    public class CompanyController : ControllerBase
    {
        ICompanyLogic logic;
        IHubContext<SignalRHub> hub;

        public CompanyController(ICompanyLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Company> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Company Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Company value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("CompanyCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Company value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("CompanyUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var companyToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("CompanyDeleted", companyToDelete);
        }
    }
}
