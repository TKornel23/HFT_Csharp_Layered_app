using HSTUTU_HFT_2021221.Models;
using System.Collections.Generic;

namespace HSTUTU_HFT_2021221.Repository
{
    public interface IPostRepository : IReposotiry<Post>
    {
        void Update(Post post);

        IEnumerable<Post> GetPostsByBlogId(int id);
    }
}
