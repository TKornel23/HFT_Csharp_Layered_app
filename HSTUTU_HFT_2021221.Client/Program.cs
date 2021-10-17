using System;
using System.Collections.Generic;
using System.Linq;
using HSTUTU_HFT_2021221.Data;
using HSTUTU_HFT_2021221.Models;

namespace HSTUTU_HFT_2021221
{
    class Program
    {
        static void Main(string[] args)
        {
            BlogDbContext ctx = new BlogDbContext();
            foreach (var item in ctx.Blogs)
            {
                Console.WriteLine(item.Title);
            }
            Console.ReadLine();
        }
    }
}
