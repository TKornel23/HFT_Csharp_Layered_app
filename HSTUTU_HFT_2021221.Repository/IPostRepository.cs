using HSTUTU_HFT_2021221.Models;

namespace HSTUTU_HFT_2021221.Repository
{
    public interface IPostRepository : IReposotiry<Post>
    {
        void UpdatePost(Post post);
    }
}
