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
        IPostRepository posttrepo;

        public BlogLogic(IBlogRepository repoPar, IPostRepository posttrepo)
        {
            this.repo = repoPar;
            this.posttrepo = posttrepo;
        }

        public void ChangeBlogTitle(int id, string title)
        {
            repo.ChangeBlogTitle(id, title);
        }

        public void CreateBlog(Blog newBlog)
        {
            if(newBlog.Title == null || newBlog.Title == "")
            {
                throw new InvalidOperationException("Helytelen Blog cím");
            }
            else
            {
                repo.Create(newBlog);
            }           
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
            var blog = repo.GetOne(1);
            if (blog != null)
            {
                return repo.GetOne(id);
            }
            else
            {
                throw new Exception("ID nem található");
            }
        }

        public IEnumerable<string> GetBlogPostTitleById(int id)
        {
            return repo.GetAll().Select(x => x).Where(x => x.ID == id).SelectMany(x => x.PostTags.Select(x => x.Post.Title));
        }

        public IEnumerable<string> GetAllBlogTagNameById(int id)
        {
            return repo.GetAll().Select(x => x).Where(x => x.ID == id).SelectMany(x => x.PostTags.Select(x => x.Tag.Name));
        }

        public IEnumerable<KeyValuePair<string, int>> GetSumOfPostLikesByBlog()
        {
            var q1 = posttrepo.GetAll();
            var q2 = repo.GetAll();

            var q3 = from x in posttrepo.GetAll()
                     join y in repo.GetAll() on x.BlogId equals y.ID
                     let joinedItem = new
                     {
                         y.Title,
                         x.Likes
                     }
                     group joinedItem by joinedItem.Title into g
                     select new KeyValuePair<string, int>
                     (
                         g.Key, g.Sum(x => x.Likes)
                     );
            
            
            return q3;
        }
    }
}
