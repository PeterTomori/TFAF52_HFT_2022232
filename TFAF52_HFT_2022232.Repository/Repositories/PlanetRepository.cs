using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFAF52_HFT_2022232.Models;

namespace TFAF52_HFT_2022232.Repository
{
    public class PlanetRepository : Repository<Planet>, IRepository<Planet>
    {
        public PlanetRepository(ShipDbContext context) : base(context)
        {
        }

        public override Planet Read(int id)
        {
            return context.Planets.FirstOrDefault(t => t.PlanetId == id);
        }

        public override void Update(Planet item)
        {
            var old = Read(item.PlanetId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            context.SaveChanges();
        }
    }
}
