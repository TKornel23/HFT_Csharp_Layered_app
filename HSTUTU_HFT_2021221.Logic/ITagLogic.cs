using HSTUTU_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HSTUTU_HFT_2021221.Logic
{
    public interface ITagLogic
    {
        Tag GetTagById(int id);
        void UpdateTag(Tag tag);
        IList<Tag> GetAllTags();
        void DeleteTag(int id);
        void CreateTag(Tag newBlog);
    }
}
