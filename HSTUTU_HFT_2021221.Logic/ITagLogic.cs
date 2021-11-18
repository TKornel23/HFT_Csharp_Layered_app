using HSTUTU_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTUTU_HFT_2021221.Logic
{
    interface ITagLogic
    {
        Tag GetBlogById(int id);
        void ChangeTagTitle(int id, string title);
        IList<Tag> GetAllBlogs();
        void DeleteTag(int id);
        void CreateTag(Tag newBlog);
    }
}
