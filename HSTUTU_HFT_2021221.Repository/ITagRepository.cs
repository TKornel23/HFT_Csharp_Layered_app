using HSTUTU_HFT_2021221.Models;

namespace HSTUTU_HFT_2021221.Repository
{
    public interface ITagRepository : IReposotiry<Tag>
    {
        void UpdateTag(Tag tag);
    }
}
