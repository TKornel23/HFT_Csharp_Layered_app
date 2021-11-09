using System;
using System.Collections.Generic;
using System.Linq;
using HSTUTU_HFT_2021221.Data;
using HSTUTU_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using HSTUTU_HFT_2021221.Repository;

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

            TagRepository tagrep = new TagRepository(ctx);

            foreach (var item in ctx.Tags)
            {
                Console.WriteLine(item.Name);
            }
            tagrep.ChangeTagName(1, "nemis");
            foreach (var item in ctx.Tags)
            {
                Console.WriteLine(item.Name);
            }
            ;
        }
    }
}
