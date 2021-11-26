﻿using HSTUTU_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTUTU_HFT_2021221.Logic
{
    public interface IBlogLogic
    {
        Blog GetBlogById(int id);
        void ChangeBlogTitle(Blog blog);
        IList<Blog> GetAllBlogs();
        void DeleteBlog(int id);
        void CreateBlog(Blog newBlog);
        IEnumerable<string> GetBlogPostTitleById(int id);
        IEnumerable<string> GetAllBlogTagNameById(int id);
        IEnumerable<KeyValuePair<string, int>> GetSumOfPostLikesByBlog();
    }
}
