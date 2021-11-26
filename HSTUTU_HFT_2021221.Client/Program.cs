using HSTUTU_HFT_2021221.Client;
using HSTUTU_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HSTUTU_HFT_2021221
{
    class Program
    {
        static void Main(string[] args)
        {
            RestService rest = new RestService("http://localhost:54276");

            var blogs = rest.Get<Blog>("blog");
            var tags = rest.Get<Tag>("tag");
            var posts = rest.Get<Post>("tag");
        }
    }
}
