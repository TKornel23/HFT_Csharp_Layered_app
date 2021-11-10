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
            Blog blog = new Blog();
            Tag tag = new Tag();
            Post post = new Post();

            BlogRepository rep = new BlogRepository(ctx);
            PostRepository pt = new PostRepository(ctx);

            PostLogic logicp = new PostLogic(pt);

            var q1 = logicp.GetTagsByPostId(2);
            ;
        }
    }
}
