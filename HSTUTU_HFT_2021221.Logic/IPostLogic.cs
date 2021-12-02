using HSTUTU_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTUTU_HFT_2021221.Logic
{
    public interface IPostLogic
    {
        Post GetOnePost(int id);
        IList<Post> GetAllPosts();
        void DeletePost(int id);
        void CreatePost(Post newPost);
        void ChangePostTitle(Post post);
        public IEnumerable<KeyValuePair<string, int>> GetTagsCountGroupByPost();
    }
}
