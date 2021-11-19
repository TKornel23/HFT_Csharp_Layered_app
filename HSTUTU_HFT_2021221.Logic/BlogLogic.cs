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

        public IEnumerable<string> GetBlogPostTitleById(int id)
        {
            return repo.GetAll().Select(x => x).Where(x => x.ID == id).SelectMany(x => x.PostTags.Select(x => x.Post.Title));
        }

        public IEnumerable<string> GetAllBlogTagNameById(int id)
        {
            return repo.GetAll().Select(x => x).Where(x => x.ID == id).SelectMany(x => x.PostTags.Select(x => x.Tag.Name));
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllBlogPostGroupByBlogTitle()
        {
            var q1 = posttrepo.GetAll();
            var q2 = repo.GetAll();

            var q3 = from x in q1
                     join y in q2 on x.BlogId equals y.ID
                     let joinedItem = new
                     {
                         Key = y.Title,
                         Values = x.Title
                     }
                     select new KeyValuePair<string, string>(
                         joinedItem.Key, joinedItem.Values
                     );
            return q3;
        }
    }
}
