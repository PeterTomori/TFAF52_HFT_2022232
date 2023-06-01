using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFAF52_HFT_2022232.Models;

namespace TFAF52_HFT_2022232.Repository
{
    public class ShipRepository : Repository<Ship>, IRepository<Ship>
    {
        public ShipRepository(ShipDbContext context) : base(context)
        {
        }

        public override Ship Read(int id)
        {
            return context.Ships.FirstOrDefault(t => t.ShipId == id);
        }

        public override void Update(Ship item)
        {
            var old = Read(item.ShipId);
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
