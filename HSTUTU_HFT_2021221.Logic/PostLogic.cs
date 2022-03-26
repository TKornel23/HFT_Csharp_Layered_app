using HSTUTU_HFT_2021221.Models;
using HSTUTU_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTUTU_HFT_2021221.Logic
{
    public class PostLogic : IPostLogic
    {
        IPostRepository repo;
        IBlogRepository blogrepo;
        ITagRepository tagrepo;


        public PostLogic(IPostRepository repoPar, IBlogRepository blogrepo, ITagRepository tagrepo)
        {
            this.repo = repoPar;
            this.blogrepo = blogrepo;
            this.tagrepo = tagrepo;
        }

        public void CreatePost(Post newPost)
        {
            if(newPost.Title == null || newPost.Title == "")
            {
                throw new InvalidOperationException();               
            }
            else
            {
                repo.Create(newPost);
            }
        }

        public void ChangePostTitle(Post post)
        {
            if (repo.GetAll().FirstOrDefault(x => x.Id == post.Id) != null)
            { repo.Update(post); }
            else
            {
                throw new Exception("Bad ID");
            }
        }

        public void DeletePost(int id)
        {
            if (repo.GetAll().FirstOrDefault(x => x.Id == id) != null)
            { repo.Delete(id); }
            else
            {
                throw new Exception("Bad ID");
            }
        }

        public IList<Post> GetAllPosts()
        {
            return repo.GetAll().ToList<Post>();
        }

        public Post GetOnePost(int id)
        {

            if (repo.GetAll().FirstOrDefault(x => x.Id == id) != null)
            { return repo.GetOne(id); }
            else
            {
                throw new Exception("Bad ID");
            }
        }

        public IEnumerable<KeyValuePair<string, int>> GetTagsCountGroupByPost()
        {

            var q1 = from x in repo.GetAll()
                     join y in tagrepo.GetAll() on x.Id equals y.PostId
                     let joinedItem = new
                     {
                         x.Title,
                         y.Id
                     }
                     group joinedItem by joinedItem.Title into g
                     select new KeyValuePair<string, int>
                     (
                         g.Key, g.Select(x => x.Id).Count()
                     );


            return q1;
        }

        public IEnumerable<Post> GetPostsByBlogId(int id)
        {
            return repo.GetPostsByBlogId(id);
        }
    }
}
