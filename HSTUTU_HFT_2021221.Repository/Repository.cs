using HSTUTU_HFT_2021221.Data;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTUTU_HFT_2021221.Repository
{
    public abstract class Repository<T> : IReposotiry<T> where T : class
    {
        protected BlogDbContext _ctx;

        public Repository(BlogDbContext ctx)
        {
            this._ctx = ctx;
        }

        public void Update(T t, int id)
        {
            throw new NotImplementedException();
        }

        public abstract void Create(T item);

        public abstract void Delete(int id);

        public IQueryable<T> GetAll()
        {
            return _ctx.Set<T>();
        }

        public abstract T GetOne(int id);
    }
}
