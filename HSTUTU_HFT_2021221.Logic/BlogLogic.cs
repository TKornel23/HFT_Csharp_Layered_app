using HSTUTU_HFT_2021221.Models;
using HSTUTU_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTUTU_HFT_2021221.Logic
{
    public class BlogLogic : IBlogLogic
    {
        IBlogRepository repo;

        public BlogLogic(IBlogRepository repoPar)
        {
            this.repo = repoPar;
        }

        public void ChangeBlogTitle(int id, string title)
        {
            repo.ChangeBlogTitle(id, title);
        }

        public void CreateBlog(Blog newBlog)
        {
            repo.Create(newBlog);
        }

        public void DeleteBlog(int id)
        {
            repo.Delete(id);
        }

        public IList<Blog> GetAllBlogs()
        {
            return repo.GetAll().ToList<Blog>();
        }

        public Blog GetBlogById(int id)
        {
            return repo.GetOne(id);
        }
    }
}
