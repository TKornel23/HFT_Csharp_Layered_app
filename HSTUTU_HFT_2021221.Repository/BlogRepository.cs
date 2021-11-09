using HSTUTU_HFT_2021221.Data;
using HSTUTU_HFT_2021221.Models;
using System.Collections.Generic;
using System.Linq;

namespace HSTUTU_HFT_2021221.Repository
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(BlogDbContext ctx) : base(ctx)
        {

        }

        public void ChangeBlogTitle(int id, string title)
        {
            Blog item = _ctx.Blogs.FirstOrDefault<Blog>(x => x.ID == id);
            if(item != null)
            {
                item.Title = title;
                _ctx.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException();
            }
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
            return _ctx.Blogs.FirstOrDefault<Blog>(x => x.ID == id);
        }
    }
}
