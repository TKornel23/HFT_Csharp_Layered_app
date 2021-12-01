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
        ITagRepository tegrepo;

        public PostLogic(IPostRepository repoPar, ITagRepository tegrepo)
        {
            this.repo = repoPar;
            this.tegrepo = tegrepo;
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
            repo.Update(post); 
        }

        public void DeletePost(int id)
        {
            repo.Delete(id);
        }

        public IList<Post> GetAllPosts()
        {
            return repo.GetAll().ToList<Post>();
        }

        public Post GetOnePost(int id)
        {
            return repo.GetOne(id);
        }

        public IEnumerable<string> GetTagsByPostId(int id)
        {
            return repo.GetAll().Select(x => x).Where(x => x.Id == id).SelectMany(x => x.PostTags.Select(x => x.Tag.Name)).ToList();
        }
    }
}
