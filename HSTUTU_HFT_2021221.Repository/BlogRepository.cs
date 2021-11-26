using HSTUTU_HFT_2021221.Data;
using HSTUTU_HFT_2021221.Models;
using System.Collections.Generic;
using System.Linq;

namespace HSTUTU_HFT_2021221.Repository
{
    public class BlogRepository : Repository<Blog>, IBlogRepository, IReposotiry<Blog>
    {
        public BlogRepository(BlogDbContext ctx) : base(ctx)
        {

        }

        public void ChangeBlogTitle(Blog blog)
        {
            var currentBlog = _ctx.Blogs.FirstOrDefault(x => x.ID == blog.ID);
            currentBlog.Title = blog.Title;
            currentBlog.PostTags = blog.PostTags;
        }

        public override void Create(Blog item)
        {
            _ctx.Blogs.Add(item);
            _ctx.SaveChanges();
        }

        public override void Delete(int id)
        {
            Blog item = _ctx.Blogs.FirstOrDefault<Blog>(x => x.ID == id);
            _ctx.Blogs.Remove(item);
            _ctx.SaveChanges();
        }

        public override Blog GetOne(int id)
        {
            return GetAll().FirstOrDefault(x => x.ID == id);
        }
    }
}
