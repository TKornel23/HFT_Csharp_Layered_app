using HSTUTU_HFT_2021221.Client;
using HSTUTU_HFT_2021221.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HSTUTU_HFT_2021221
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(8000);
            Console.Clear();
            RestService rest = new RestService("http://localhost:57125");

            try
            {

                bool showMenu = true;
                while (showMenu)
                {
                    showMenu = MainMenu();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) R-READ ALL (FROM blog,post,tag)...");
            Console.WriteLine("2) R-READ ONE (FROM blog,post,tag)...");
            Console.WriteLine("3) C-CREATE (blog,post,tag)...");
            Console.WriteLine("4) U-UPDATE (FROM existing database data)...");
            Console.WriteLine("5) D-DELETE (FROM existing database data)...");
            Console.WriteLine("6) Write out every Blog with it's Tags");
            Console.WriteLine("7) Write tags by Blog Id...");
            Console.WriteLine("8) Write out Likes count groupped by blog...");
            Console.WriteLine("9) Write out how many tags a blog has...");
            Console.WriteLine("10) Write out all the tag's post by id...");
            Console.WriteLine("11) Exit...");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Read();
                    return true;
                case "2":
                    ReadOne();
                    return true;
                case "3":
                    Create();
                    return true;
                case "4":
                    Update();
                    return true;
                case "5":
                    Delete();
                    return true;
                case "6":
                    blogposttile();
                    return true;
                case "7":
                    blogtagname();
                    return true;
                case "8":
                    likesum();
                    return true;
                case "9":
                    tagsbypost();
                    return true;
                case "10":
                    postsbytag();
                    return true;
                case "11":
                    return false;
                default:
                    MainMenu();
                    return true;
            }
        }
        private static void ReadOne()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:57125");
            Console.WriteLine("Which table should I display ? (blog,post,tag)");
            string table = Console.ReadLine();
            if (table == "blog" || table == "post" || table == "tag")
            {
                switch (table)
                {
                    case "blog":
                        Console.WriteLine("Write doen an ID: ");
                        int id = int.Parse(Console.ReadLine());
                        var Blog = rest.Get<Blog>(id,"/blog");
                        Console.WriteLine(Blog.ID + " " + Blog.Title);
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "post":
                        Console.WriteLine("Write doen an ID: ");
                        int idA = int.Parse(Console.ReadLine());
                        var Post = rest.Get<Post>(idA,"/post");
                        Console.WriteLine(Post.Id + " " + Post.Title + " " + Post.PostContent + " " + Post.Likes);              
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "tag":
                        Console.WriteLine("Write doen an ID: ");
                        int idB = int.Parse(Console.ReadLine());
                        var Tag = rest.Get<Tag>(idB,"/tag");
                        Console.WriteLine(Tag.Id + " " + Tag.Name);
                        Console.ReadKey();
                        MainMenu();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Incorrect table name!");
                Console.ReadKey();
                MainMenu();
            }
        }

        private static void blogposttile()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:57125");
            var item = rest.GetSingle<IEnumerable<KeyValuePair<string, IEnumerable<string>>>>("/stat/blogposttile/");
            foreach (var post in item)
            {
                Console.WriteLine("Blog Title: "+post.Key);
                foreach (var asd in post.Value)
                {
                    Console.WriteLine("It's Tag: " + asd);
                }
                Console.WriteLine("");
            }
            Console.ReadKey();
            MainMenu();
        }

        private static void blogtagname()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:57125");
            Console.WriteLine("Write down a blog ID: ");
            string id = Console.ReadLine();
            var item = rest.GetSingle<IEnumerable<string>>("/stat/blogtagname/" + id);
            Console.WriteLine("Tag name of id: " + id + " blog: ");
            foreach (var tag in item)
            {
                Console.WriteLine(tag);
            }
            Console.ReadKey();
            MainMenu();
        }

        private static void likesum()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:57125");
            var item = rest.GetSingle<IEnumerable<KeyValuePair<string, int>>>("/stat/likesum/");
            foreach (var tag in item)
            {
                Console.WriteLine("Blog Title: " + tag.Key);
                Console.WriteLine(" - Likes: " + tag.Value);
            }
            Console.ReadKey();
            MainMenu();
        }

        private static void tagsbypost()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:57125");
            var item = rest.GetSingle<IEnumerable<KeyValuePair<string, int>>>("/stat/tagsbypost/");
            foreach (var tag in item)
            {
                Console.WriteLine("Post name: " + tag.Key);
                Console.WriteLine("Count of it's tags: " + tag.Value);
            }
            Console.ReadKey();
            MainMenu();
        }

        private static void postsbytag()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:57125");
            Console.WriteLine("Write down a blog ID: ");
            string id = Console.ReadLine();
            var item = rest.GetSingle<IEnumerable<string>>("/stat/postsbytag/" + id);
            Console.WriteLine("The " + id + " blog tags: ");
            foreach (var tag in item)
            {
                Console.WriteLine("-" + tag);
            }
            Console.ReadKey();
            MainMenu();
        }


        private static void Delete()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:57125");
            Console.WriteLine("Which table you want me to delete from ? (blog,post,tag)");
            string table = Console.ReadLine();
            Console.WriteLine("Which value with which ID do you want to delete ?");
            int id = int.Parse(Console.ReadLine());
            rest.Delete(id, table);
            Console.WriteLine("The item has been successfully deleted!");
            Console.ReadKey();
            MainMenu();
        }
        private static void Create()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:57125");
            Console.WriteLine("What type of element do you want me to insert ? (blog,post,tag)");
            string table = Console.ReadLine();
            if (table == "blog" || table == "post"|| table == "tag")
            {
                switch(table)
                {
                    case "blog":
                        Console.WriteLine("Title of the blog: ");
                        string title = Console.ReadLine();
                        Console.WriteLine("Setup the connections between entites!");
                        Console.WriteLine("Connected post id: ");
                        int postId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Connected tag id: ");
                        int tagId = int.Parse(Console.ReadLine());
                        Blog newBlog = new Blog()
                        {
                            Title = title
                        };
                        var blogs = rest.Get<Blog>("/blog");
                        rest.Post(newBlog, "/blog");
                        Console.WriteLine("Item successfully added!");                       
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "post":
                        Console.WriteLine("Title of the Post: ");
                        string Posttitle = Console.ReadLine();
                        Console.WriteLine("Enter the Content of the Post");
                        string content = Console.ReadLine();
                        Console.WriteLine("Please enter the ID of the Connected Blog: ");
                        int blogId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter the likes count: ");
                        int likes = int.Parse(Console.ReadLine());
                        Post newPost = new Post()
                        {
                            Title = Posttitle,
                            Likes = likes,
                            PostContent = content,
                            BlogId = blogId

                        };
                        rest.Post(newPost, "/post");
                        Console.WriteLine("Item successfully added!");                      
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "tag":
                        Console.WriteLine("What is the name of the Tag ?");
                        string name = Console.ReadLine();
                        Console.WriteLine("Please enter the ID of the Post: ");
                        postId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Item successfully added!");
                        Tag newTag = new Tag()
                        {
                            Name = name, PostId = postId

                        };
                        rest.Post(newTag, "/tag");
                        Console.WriteLine("Item successfully added!");
                        Console.ReadKey();
                        MainMenu();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Incorrect table name!");
                Console.ReadKey();
                MainMenu();
            }
        }
        private static void Read()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:57125");
            Console.WriteLine("Which table should I display ? (blog,post,tag)");
            string table = Console.ReadLine();
            if (table == "blog" || table == "post" || table == "tag")
            {
                switch (table)
                {
                    case "blog":
                        var Blogs = rest.Get<Blog>("/blog");
                        foreach (var item in Blogs)
                        {
                            Console.WriteLine(item.ID + " " + item.Title);
                        }
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "post":
                        var Posts = rest.Get<Post>("/post");
                        foreach (var item in Posts)
                        {
                            Console.WriteLine(item.Id + " " + item.Title + " " + item.PostContent + " " + item.Likes + " " + item.BlogId);
                        }
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "tag":
                        var Tags = rest.Get<Tag>("/tag");
                        foreach (var item in Tags)
                        {
                            Console.WriteLine(item.Id + " " + item.Name + " " + item.PostId);
                        }
                        Console.ReadKey();
                        MainMenu();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Incorrect table name!");
                Console.ReadKey();
                MainMenu();
            }
        }
        private static void Update()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:57125");
            Console.WriteLine("What type of element do you want me to update ? (blog,tag,post)");
            string table = Console.ReadLine();
            if (table == "blog" || table == "tag" || table == "post")
            {
                switch (table)
                {
                    case "blog":
                        Console.WriteLine("Please enter the ID of the Blog: ");
                        int blogId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Title of the blog: ");
                        string title = Console.ReadLine();
                        rest.Put(new Blog()
                        {
                            Title = title,
                            ID = blogId

                        }, "/blog");
                        Console.WriteLine("Item successfully updated!");
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "post":
                        Console.WriteLine("Please enter the ID of the Post: ");
                        int postId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Title of Post: ");
                        string postTitle = Console.ReadLine();
                        Console.WriteLine("Enter the count of likes: ");
                        int likes = int.Parse(Console.ReadLine());
                        Console.WriteLine("Write down the content os the Post: ");
                        string content = Console.ReadLine();
                        Console.WriteLine("Write down the ID of the connected Blog: ");
                        int blogIdforPost = int.Parse(Console.ReadLine());
                        rest.Put(new Post()
                        {
                            Title = postTitle,
                            Likes = likes,
                            PostContent = content,
                            Id = postId,
                            BlogId = blogIdforPost

                        }, "/post");
                        Console.WriteLine("Item successfully added!");
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "tag":
                        Console.WriteLine("Please enter the ID of the tag: ");
                        int tagid = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter the ID of is's post: ");
                        int postid = int.Parse(Console.ReadLine());
                        Console.WriteLine("What is the name of the Tag ?");
                        string TagName = Console.ReadLine();        
                        Console.WriteLine("Item successfully added!");
                        rest.Post(new Tag()
                        {
                            Name = TagName, Id = tagid, PostId = postid

                        }, "/tag");
                        Console.ReadKey();
                        MainMenu();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Incorrect table name!");
                Console.ReadKey();
                MainMenu();
            }

        }
    }           
}
