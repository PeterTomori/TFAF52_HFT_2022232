using System.Collections.Generic;
using System.Linq;
using TFAF52_HFT_2022232.Models;

namespace TFAF52_HFT_2022232.Logic
{
    public interface IShipLogic
    {
        void Create(Ship item);
        void Delete(int id);
        Ship Read(int id);
        IQueryable<Ship> ReadAll();
        void Update(Ship item);
        IEnumerable<Company> ShipManufacturers(string element);
        IEnumerable<FactionCounted> ShipFactions(); 
    }
}