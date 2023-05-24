using System.Collections.Generic;
using System.Linq;
using TFAF52_HFT_2022232.Models;

namespace TFAF52_HFT_2022232.Logic
{
    public interface ICompanyLogic
    {
        void Create(Company item);
        void Delete(int id);
        Company Read(int id);
        IQueryable<Company> ReadAll();
        void Update(Company item);
        IEnumerable<Ship> ShipOfFactions(string faction);
        IEnumerable<Planet> OwnedByCompany(string company);
    }
}