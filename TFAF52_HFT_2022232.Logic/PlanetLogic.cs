using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFAF52_HFT_2022232.Models;
using TFAF52_HFT_2022232.Repository;

namespace TFAF52_HFT_2022232.Logic
{
    public class PlanetLogic : IPlanetLogic
    {
        IRepository<Planet> repo;

        public PlanetLogic(IRepository<Planet> repo)
        {
            this.repo = repo;
        }

        public void Create(Planet item)
        {
            if (item.PlanetName.Length < 3)
            {
                throw new ArgumentException();
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Planet Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Planet> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Planet item)
        {
            this.repo.Update(item);
        }

        //Returns which Company owns the given Planet
        public IEnumerable<Company> OwnerOfPlanet(string planet)
        {
            var owner = from x in this.repo.ReadAll()
                        where x.PlanetName == planet
                        select x.Company;
            return owner;
        }
    }
}