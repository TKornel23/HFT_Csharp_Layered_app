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

        public PostLogic(IPostRepository repoPar)
        {
            this.repo = repoPar;
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

        public IEnumerable<string> GetTagsByPostId(int id)
        {
            return repo.GetAll().Select(x => x).Where(x => x.Id == id).SelectMany(x => x.PostTags.Select(x => x.Tag.Name)).ToList();
        }
    }
}
