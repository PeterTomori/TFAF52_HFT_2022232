using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TFAF52_HFT_2022232.Models;
using TFAF52_HFT_2022232.Repository;

namespace TFAF52_HFT_2022232.Logic
{
    public class ShipLogic : IShipLogic
    {
        IRepository<Ship> srepo;

        public ShipLogic(IRepository<Ship> repo)
        {
            srepo = repo;
        }

        public void Create(Ship item)
        {
            if (item.ShipName.Length < 3)
            {
                throw new ArgumentException();
            }
            this.srepo.Create(item);
        }

        public void Delete(int id)
        {
            this.srepo.Delete(id);
        }

        public Ship Read(int id)
        {
            return this.srepo.Read(id);
        }

        public IQueryable<Ship> ReadAll()
        {
            return this.srepo.ReadAll();
        }

        public void Update(Ship item)
        {
            this.srepo.Update(item);
        }

        //Returns the Company who builds the given Ship
        public IEnumerable<Company> ShipManufacturers(string shipname)
        {
            var ships = from x in srepo.ReadAll()
                        where x.ShipName == shipname
                        select x.Company;
            return ships;
        }

        //Returns how many ship each faction has
        public IEnumerable<FactionCounted> ShipFactions()
        {
            var factions = from x in srepo.ReadAll()
                           group x by x.Company.Faction into grp
                           select new FactionCounted()
                           {
                               Faction = grp.Key,
                               ShipCount = grp.Count()
                           };
            return factions;
        }


    }
}
