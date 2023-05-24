using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFAF52_HFT_2022232.Models;
using TFAF52_HFT_2022232.Repository;

namespace TFAF52_HFT_2022232.Logic
{
    public class CompanyLogic : ICompanyLogic
    {
        IRepository<Company> repo;

        public CompanyLogic(IRepository<Company> repo)
        {
            this.repo = repo;
        }

        public void Create(Company item)
        {
            if (item.CompanyName.Length < 3)
            {
                throw new ArgumentException();
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Company Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Company> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Company item)
        {
            this.repo.Update(item);
        }

        //Returns given Company's Planet(s) 
        public IEnumerable<Planet> OwnedByCompany(string company)
        {
            var planetsOfCompanies = (from x in this.repo.ReadAll()
                                      where x.CompanyName == company
                                      select x.Planets).SelectMany(t => t);
            return planetsOfCompanies;
        }

        //Returns a Faction's Ships
        public IEnumerable<Ship> ShipOfFactions(string faction)
        {
            var byfaction = (from x in this.repo.ReadAll()
                             where x.Faction == faction
                             select x.Ships).SelectMany(t => t);
            return byfaction;
        }
    }
}
