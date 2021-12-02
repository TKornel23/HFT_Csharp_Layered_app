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
        ITagRepository tagrepo;

        public BlogLogic(IBlogRepository repoPar, IPostRepository posttrepo, ITagRepository tagrepo)
        {
            this.repo = repoPar;
            this.posttrepo = posttrepo;
            this.tagrepo = tagrepo;
        }

        public void Update(Blog blog)
        {
            if (repo.GetAll().FirstOrDefault(x => x.ID == blog.ID) != null)
            { repo.Update(blog); }
            else
            {
                throw new Exception("Bad ID");
            }
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
            if (repo.GetAll().FirstOrDefault(x => x.ID == id) != null)
            { repo.Delete(id); }
            else
            {
                throw new Exception("Bad ID");
            }
        }

        public IList<Blog> GetAllBlogs()
        {
            return repo.GetAll().ToList<Blog>();
        }

        public Blog GetBlogById(int id)
        {
            var blog = repo.GetOne(id);
            if (blog != null)
            {
                return repo.GetOne(id);
            }
            else
            {
                throw new Exception("ID nem található");
            }
        }

        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> GetBlogPostTitleById()
        {
            var q3 = from x in posttrepo.GetAll().AsEnumerable()
                     join y in repo.GetAll().AsEnumerable() on x.BlogId equals y.ID
                     let joinedItem = new
                     {
                         Key = y.Title,
                         Value = x.Title
                     }

                     group joinedItem by joinedItem.Key into g
                     select new KeyValuePair<string, IEnumerable<string>>
                     (
                        g.Key, g.Select(x => x.Value)
                     );


            return q3;
        }

        public IEnumerable<string> GetAllBlogTagNameById(int id)
        {
            var q1 = posttrepo.GetAll().Where(x => x.Id == id).Select(x =>x.Id);
            var q2 = tagrepo.GetAll().Where(x => q1.Contains(x.Id));

            var q3 = q2.Select(x => x.Name);
            

            return q3;
        }

        public IEnumerable<KeyValuePair<string, int>> GetSumOfPostLikesByBlog()
        {

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
