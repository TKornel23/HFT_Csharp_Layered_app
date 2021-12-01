using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTUTU_HFT_2021221.Repository
{
    public interface IReposotiry<T> where T : class
    {
        T GetOne(int id);
        IQueryable<T> GetAll();
        void Update(T t);
        void Delete(int id);
        void Create(T item);
    }
}
