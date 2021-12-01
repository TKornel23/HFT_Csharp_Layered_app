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

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }           
        }
        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) C-CREATE (blog,post,tag)...");
            Console.WriteLine("2) R-READ (FROM blog,post,tag)...");
            Console.WriteLine("3) U-UPDATE (FROM existing database data)...");
            Console.WriteLine("4) D-DELETE (FROM existing database data)...");
            Console.WriteLine("5) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Create();
                    return true;
                case "2":
                    Read();
                    return true;
                case "3":
                    Update();
                    return true;
                case "4":
                    Delete();
                    return true;
                case "5":
                    return false;
                default:
                    MainMenu();
                    return true;
            }
        }

        private static void Delete()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:57125");
            Console.WriteLine("Which table you want me to delete from ? (blogs,posts,tags)");
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
                            Title = title,
                            PostTags = new List<PostTag>()

                        };
                        var tags = rest.Get<Tag>("/tag");
                        var posts = rest.Get<Post>("/post");
                        var blogs = rest.Get<Blog>("/blog");
                        var selectedTag = tags.FirstOrDefault(x => x.Id == tagId);
                        var selectedPost = posts.FirstOrDefault(x => x.Id == postId);
                        var maxId = blogs.Max(x => x.ID);
                        if(selectedPost is null && selectedTag is null)
                        {
                            Console.WriteLine("Invalid Post or Tag ID, try again!");
                        }
                        else
                        {
                            PostTag newPostTag = new PostTag() { Blog = newBlog, BlogId = maxId+1, Post = selectedPost, PostId = postId, Tag = selectedTag, TagId = tagId };
                            newBlog.PostTags.Add(newPostTag);
                            selectedTag.PostTags = new List<PostTag>() { newPostTag };
                            selectedPost.PostTags = new List<PostTag>() { newPostTag };
                            rest.Put<Tag>(selectedTag, "/tag");
                            rest.Put<Post>(selectedPost, "/post");
                            rest.Post(newBlog, "/blog");
                            Console.WriteLine("Item successfully added!");
                        }                       
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
                        Console.WriteLine("Please enter the ID of the connected Tag: ");
                        tagId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter the likes count: ");
                        int likes = int.Parse(Console.ReadLine());
                        Post newPost = new Post()
                        {
                            Title = Posttitle,
                            PostTags = new List<PostTag>(),
                            Likes = likes,
                            PostContent = content,
                            BlogId = blogId

                        };
                        var tagsA = rest.Get<Tag>("/tag");
                        var postsA = rest.Get<Post>("/post");
                        var blogsA= rest.Get<Blog>("/blog");
                        var selectedTagA = tagsA.FirstOrDefault(x => x.Id == tagId);
                        var selectedBlogA = blogsA.FirstOrDefault(x => x.ID == blogId);
                        maxId = postsA.Max(x => x.Id);
                        if (selectedBlogA == null && selectedTagA == null)
                        {
                            Console.WriteLine("Invalid Blog or Tag ID, try again!");
                        }
                        else
                        {
                            PostTag newPostTag = new PostTag() { Blog = selectedBlogA, BlogId = blogId, Post = newPost, PostId = maxId+1, Tag = selectedTagA, TagId = tagId };
                            newPost.PostTags.Add(newPostTag);
                            selectedTagA.PostTags = new List<PostTag>() { newPostTag };
                            selectedBlogA.PostTags = new List<PostTag>() { newPostTag };
                            rest.Put<Tag>(selectedTagA, "/tag");
                            rest.Put<Blog>(selectedBlogA, "/blog");
                            rest.Post(newPost, "/post");
                            Console.WriteLine("Item successfully added!");
                        }                       
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "tag":
                        Console.WriteLine("What is the name of the Tag ?");
                        string name = Console.ReadLine();
                        Console.WriteLine("Please enter the ID of the Post: ");
                        postId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter the ID of the Blog: ");
                        blogId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Item successfully added!");
                        Tag newTag = new Tag()
                        {
                            Name = name,
                            PostTags = new List<PostTag>()

                        };
                        var tagsB = rest.Get<Tag>("/tag");
                        var postsB = rest.Get<Post>("/post");
                        var blogsB = rest.Get<Blog>("/blog");
                        var selectedPostB = postsB.FirstOrDefault(x => x.Id == postId);
                        var selectedBlogB = blogsB.FirstOrDefault(x => x.ID == blogId);
                        maxId = tagsB.Max(x => x.Id);
                        if (selectedBlogB is null && selectedPostB is null)
                        {
                            Console.WriteLine("Invalid Blog or Tag ID, try again!");
                        }
                        else
                        {
                            PostTag newPostTag = new PostTag() { Blog = selectedBlogB, BlogId = blogId, Post = selectedPostB, PostId = postId, Tag = newTag, TagId = maxId+1 };
                            newTag.PostTags.Add(newPostTag);
                            selectedPostB.PostTags = new List<PostTag>() { newPostTag };
                            selectedBlogB.PostTags = new List<PostTag>() { newPostTag };
                            rest.Put<Blog>(selectedBlogB, "/blog");
                            rest.Put<Post>(selectedPostB, "/post");
                            rest.Post(newTag, "/tag");
                            Console.WriteLine("Item successfully added!");
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
                            Console.WriteLine(item.Id + " " + item.Title + " " + item.PostContent + " " + item.Likes);
                        }
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "tag":
                        var Tags = rest.Get<Tag>("/tag");
                        foreach (var item in Tags)
                        {
                            Console.WriteLine(item.Id + " " + item.Name);
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
                        if (rest.Get<Blog>("/blog").FirstOrDefault(x => x.ID == blogIdforPost) != null)
                        {
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
                        }
                        else
                        {
                            Console.WriteLine("Invalid connected blog Id");
                            Console.ReadKey();
                            MainMenu();
                            break;
                        }
                    case "tag":
                        Console.WriteLine("Please enter the ID of the tag: ");
                        int tagid = int.Parse(Console.ReadLine());
                        Console.WriteLine("What is the name of the Tag ?");
                        string TagName = Console.ReadLine();                        
                        Console.WriteLine("Item successfully added!");
                        rest.Post(new Tag()
                        {
                            Name = TagName, Id = tagid

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
