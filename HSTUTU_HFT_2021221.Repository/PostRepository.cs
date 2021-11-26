using HSTUTU_HFT_2021221.Data;
using HSTUTU_HFT_2021221.Models;
using System;
using System.Linq;

namespace HSTUTU_HFT_2021221.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository, IReposotiry<Post>
    {
        public PostRepository(BlogDbContext ctx) :base(ctx)
        {

        }

        public void UpdatePost(Post post)
        {
            var currentBlog = _ctx.Blogs.FirstOrDefault(x => x.ID == post.Id);
            currentBlog.Title = post.Title;
            currentBlog.PostTags = post.PostTags;
        }

        public override void Create(Post item)
        {
            _ctx.Posts.Add(item);
            _ctx.SaveChanges();
        }

        public override void Delete(int id)
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
