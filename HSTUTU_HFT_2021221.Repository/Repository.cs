using HSTUTU_HFT_2021221.Data;
using HSTUTU_HFT_2021221.Models;
using System;
using System.Collections.Generic;
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
        public abstract void CreateItem(T item);

        public abstract void DeleteItem(int id);

        public IQueryable<T> GetAll()
        {
            return _ctx.Set<T>();
        }

        public abstract T GetOne(int id);
    }

    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(BlogDbContext ctx) : base(ctx)
        {

        }

        public void ChangeTitle(int id, string title)
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

        public override void CreateItem(Blog item)
        {
            _ctx.Blogs.Add(item);
            _ctx.SaveChanges();
        }

        public override void DeleteItem(int id)
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

    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(BlogDbContext ctx) : base(ctx)
        {
        }

        public void ChangeTagName(int id, string name)
        {
            Tag item = _ctx.Tags.FirstOrDefault<Tag>(x => x.Id == id);
            if(item != null)
            {
                item.Name = name;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public override void CreateItem(Tag item)
        {
            _ctx.Tags.Add(item);
            _ctx.SaveChanges();
        }

        public override void DeleteItem(int id)
        {
            Tag item = _ctx.Tags.FirstOrDefault<Tag>(x => x.Id == id);
            _ctx.Tags.Remove(item);
            _ctx.SaveChanges();
        }

        public override Tag GetOne(int id)
        {
            return _ctx.Tags.FirstOrDefault<Tag>(x => x.Id == id);
        }
    }

    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(BlogDbContext ctx) :base(ctx)
        {

        }

        public void ChangePostTitle(int id, string name)
        {
            Post item = _ctx.Posts.FirstOrDefault<Post>(x => x.Id == id);
            if(item != null)
            {
                item.Title = name;
            }
        }

        public override void CreateItem(Post item)
        {
            _ctx.Posts.Add(item);
            _ctx.SaveChanges();
        }

        public override void DeleteItem(int id)
        {
            Post item = _ctx.Posts.FirstOrDefault<Post>(x => x.Id == id);
            _ctx.Posts.Remove(item);
            _ctx.SaveChanges();
        }

        public override Post GetOne(int id)
        {
            return _ctx.Posts.FirstOrDefault<Post>(x => x.Id == id);
        }
    }
}
