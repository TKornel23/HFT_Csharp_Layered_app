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

            foreach (var item in ctx.Blogs)
            {
                Console.WriteLine("***********************");
                Console.WriteLine("Blog Title");
                Console.WriteLine("---------------------");
                Console.WriteLine(item.Title);
                foreach (var asd in ctx.Posts)
                {
                    if(asd.BlogId == item.ID)
                    {
                        Console.WriteLine("**********************");
                        Console.WriteLine("Post Title");
                        Console.WriteLine("----------------------");
                        Console.WriteLine(asd.Title);
                        Console.WriteLine(asd.PostContent);
                    }
                }
            }
            ;
        }
    }
}
