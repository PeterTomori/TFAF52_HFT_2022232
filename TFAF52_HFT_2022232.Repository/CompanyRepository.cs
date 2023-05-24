using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFAF52_HFT_2022232.Models;

namespace TFAF52_HFT_2022232.Repository
{
    public class CompanyRepository : Repository<Company>, IRepository<Company>
    {
        public CompanyRepository(ShipDbContext context) : base(context)
        {
        }

        public override Company Read(int id)
        {
            return context.Companies.FirstOrDefault(t => t.CompanyId == id);
        }

        public override void Update(Company item)
        {
            var old = Read(item.CompanyId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            context.SaveChanges();
        }
    }
}
