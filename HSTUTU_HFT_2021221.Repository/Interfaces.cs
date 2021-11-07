using HSTUTU_HFT_2021221.Models;
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

        void DeleteItem(int id);
        void CreateItem(T item);
    }

    public interface IBlogRepository : IReposotiry<Blog>
    {
        void ChangeTitle(int id, string title);
    }

    public interface ITagRepository : IReposotiry<Tag>
    {
        void ChangeTagName(int id, string name);
    }

    public interface IPostRepository : IReposotiry<Post>
    {
        void ChangePostTitle(int id, string name);
    }
}
