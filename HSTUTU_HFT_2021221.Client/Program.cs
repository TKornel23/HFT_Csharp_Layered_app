using System;
using System.Collections.Generic;
using System.Linq;
using HSTUTU_HFT_2021221.Data;
using HSTUTU_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using HSTUTU_HFT_2021221.Repository;
using HSTUTU_HFT_2021221.Logic;

namespace HSTUTU_HFT_2021221
{
    class Program
    {
        static void Main(string[] args)
        {
            BlogDbContext ctx = new BlogDbContext();

            BlogRepository rep = new BlogRepository(ctx);
            PostRepository pt = new PostRepository(ctx);
            TagRepository tp = new TagRepository(ctx);

            PostLogic logicp = new PostLogic(pt);
            TagLogic tagicp = new TagLogic(tp);
            BlogLogic blicp = new BlogLogic(rep, pt);

            var asd = blicp.GetBlogById(1);
            ;
            var q1 = logicp.GetTagsByPostId(2);
            var q2 = tagicp.GetPostByTagId(1);
            var q3 = blicp.GetBlogPostTitleById(1);
            var q4 = blicp.GetAllBlogTagNameById(2);
            var q5 = blicp.GetAllBlogPostGroupByBlogTitle();
        }
    }
}
