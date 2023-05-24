using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFAF52_HFT_2022232.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        public ShipDbContext context;

        public Repository(ShipDbContext context)
        {
            this.context = context;
        }

        public void Create(T item)
        {
            context.Set<T>().Add(item);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Remove<T>(Read(id));
            context.SaveChanges();
        }

        public abstract T Read(int id);

        public IQueryable<T> ReadAll()
        {
            return context.Set<T>();
        }

        public abstract void Update(T item);
    }
}
