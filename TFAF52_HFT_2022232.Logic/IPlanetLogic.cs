using System.Collections.Generic;
using System.Linq;
using TFAF52_HFT_2022232.Models;

namespace TFAF52_HFT_2022232.Logic
{
    public interface IPlanetLogic
    {
        void Create(Planet item);
        void Delete(int id);
        Planet Read(int id);
        IQueryable<Planet> ReadAll();
        void Update(Planet item);
        IEnumerable<Company> OwnerOfPlanet(string planet);
    }
}