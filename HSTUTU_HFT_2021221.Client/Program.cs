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


            //READ ALL
            var blogs = rest.Get<Blog>("/blog");
            var tags = rest.Get<Tag>("/tag");
            var posts = rest.Get<Post>("/post");
           ///READ ONE
            var blog = rest.Get<Blog>(1,"/blog");
            var tag = rest.Get<Blog>(1,"/tag");
            var post = rest.Get<Blog>(1,"/post");
            //POST
            Blog newBlog = new Blog()
            {
                Title = "Blog three",
                PostTags = new List<PostTag>()
            };
            Post newPost = new Post()
            {
                Likes = 789,
                Title = "Post Three",
                PostContent = "Lorem ipsum dolores est",
                PostTags = new List<PostTag>()
            };
            Tag newTag = new Tag()
            {
                Name = "Tag Four",
                PostTags = new List<PostTag>()
            };

            PostTag newPostTag = new PostTag()
            {
                Blog = newBlog,
                Tag = newTag,
                Post = newPost,
                BlogId = 3,
                PostId = 3,
                TagId = 4
            };

            Post newPost2 = new Post()
            {
                Likes = 125,
                Title = "Post Three",
                PostContent = "Lorem ipsum dolores est",
                PostTags = new List<PostTag>()
            };

            Post newPost3 = new Post()
            {
                Likes = 36,
                Title = "Post Three",
                PostContent = "Lorem ipsum dolores est",
                PostTags = new List<PostTag>()
            };

            PostTag newPostTag2 = new PostTag()
            {
                Blog = newBlog,
                Tag = newTag,
                Post = newPost2,
                BlogId = 3,
                PostId = 4,
                TagId = 4
            };
            PostTag newPostTag3 = new PostTag()
            {
                Blog = newBlog,
                Tag = newTag,
                Post = newPost3,
                BlogId = 3,
                PostId = 5,
                TagId = 4
            };


            newBlog.PostTags.Add(newPostTag);
            newBlog.PostTags.Add(newPostTag2);
            newBlog.PostTags.Add(newPostTag3);
            newPost.PostTags.Add(newPostTag);
            newTag.PostTags.Add(newPostTag);

            JsonConvert.SerializeObject(newBlog, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            rest.Post<Blog>(newBlog, "/blog");
            rest.Post<Post>(newPost, "/post");
            rest.Post<Tag>(newTag, "/tag");

            //DELETE
            rest.Delete(1, "/blog");
            rest.Delete(1, "/post");
            rest.Delete(1, "/tag");

            //UPDATE
            Blog updatedBlog = new Blog()
            {
                Title = "Blog threeV2",
                ID = 3,
                PostTags = new List<PostTag>()
            };
            updatedBlog.PostTags.Add(newPostTag);
            Tag updatedTag = new Tag()
            {
                Id = 2,
                Name = "Tag Two V2",
                PostTags = new List<PostTag>()
            };
            updatedTag.PostTags.Add(newPostTag);
            Post updatedPost = new Post()
            {
                Id = 2,
                Likes = 1564,
                PostContent = "Post content V2",
                Title = "Post Title 2 V2",
                PostTags = new List<PostTag>()
            };
            updatedPost.PostTags.Add(newPostTag);
            rest.Put<Blog>(updatedBlog, "/blog");
            rest.Put<Tag>(updatedTag, "/tag/");
            rest.Put<Post>(updatedPost, "/post/");

            var blogposttile =  rest.Get<IEnumerable<string>>(2, "stat/blogposttile");
            var blogtagname = rest.Get<IEnumerable<string>>(2, "stat/blogtagname");
            var likesum = rest.GetSingle<IEnumerable<KeyValuePair<string, int>>>("stat/likesum");
            var tagsbypost = rest.Get<IEnumerable<string>>(3, "stat/tagsbypost");
            var postsbytag = rest.Get<IEnumerable<string>>(3, "stat/postsbytag");
        }
        /*
         * static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(8000);
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }

        }
        */private static bool MainMenu()
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
                            rest.Post(newBlog, "blogs");
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
                            PostContent = content

                        };
                        var tagsA = rest.Get<Tag>("/tag");
                        var postsA = rest.Get<Post>("/post");
                        var blogsA= rest.Get<Blog>("/blog");
                        var selectedTagA = tagsA.FirstOrDefault(x => x.Id == tagId);
                        var selectedBlogA = blogsA.FirstOrDefault(x => x.ID == blogId);
                        maxId = postsA.Max(x => x.Id);
                        if (selectedBlogA is null && selectedTagA is null)
                        {
                            Console.WriteLine("Invalid Blog or Tag ID, try again!");
                        }
                        else
                        {
                            PostTag newPostTag = new PostTag() { Blog = selectedBlogA, BlogId = blogId, Post = newPost, PostId = maxId+1, Tag = selectedTagA, TagId = tagId };
                            newPost.PostTags.Add(newPostTag);
                            rest.Post(newPost, "posts");
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
                            rest.Post(newTag, "tags");
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
        }/*
        private static void Read()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:2509");
            Console.WriteLine("Which table should I display ? (artist,table,album)");
            string table = Console.ReadLine();
            if (table == "artist" || table == "track" || table == "album")
            {
                switch (table)
                {
                    case "artist":
                        var tempArtist = rest.Get<Artists>("artist");
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "album":
                        var temparAlbum = rest.Get<Albums>("album");
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "track":
                        var tempTrack = rest.Get<Tracks>("track");
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
            RestService rest = new RestService("http://localhost:2509");
            Console.WriteLine("What type of element do you want me to update ? (artist,track,album");
            string table = Console.ReadLine();
            if (table == "artist" || table == "track" || table == "album")
            {
                switch (table)
                {
                    case "artist":
                        Console.WriteLine("Please enter the ID of the Artist: ");
                        int artistid1 = int.Parse(Console.ReadLine());
                        Console.WriteLine("Name of the artist: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Birthday of the artist ");
                        DateTime birthday = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Where were they born ? ");
                        string nationality = Console.ReadLine();
                        Console.WriteLine("Do they have a Grammy award ? (true/false)");
                        bool grammy = bool.Parse(Console.ReadLine());
                        rest.Put(new Artists()
                        {
                            ArtistID = artistid1,
                            Name = name,
                            Birthday = birthday,
                            Nationality = nationality,
                            GrammyWinner = grammy,

                        }, "artist");
                        Console.WriteLine("Item successfully added!");
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "album":
                        Console.WriteLine("Please enter the ID of the Album: ");
                        int albumid1 = int.Parse(Console.ReadLine());
                        Console.WriteLine("Name of the Album: ");
                        string title = Console.ReadLine();
                        Console.WriteLine("Please enter the ID of the Artist: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Who published the album ?");
                        string label = Console.ReadLine();
                        Console.WriteLine("How long is the track ? ");
                        TimeSpan length = TimeSpan.Parse(Console.ReadLine());
                        Console.WriteLine("When was the album released ?");
                        DateTime releasedate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("What is the album genre ? ");
                        string genre = Console.ReadLine();
                        rest.Post(new Albums()
                        {
                            AlbumID = albumid1,
                            Title = title,
                            ArtistID = id,
                            Label = label,
                            Length = length,
                            ReleaseDate = releasedate,
                            Genre = genre
                        }, "album");
                        Console.WriteLine("Item successfully added!");
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "track":
                        Console.WriteLine("Please enter the ID of the Track: ");
                        int trackid = int.Parse(Console.ReadLine());
                        Console.WriteLine("What is the title of the track ?");
                        string tracktitle = Console.ReadLine();
                        Console.WriteLine("Please enter the ID of the Album: ");
                        int albumid = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter the ID of the Artist: ");
                        int artistid = int.Parse(Console.ReadLine());
                        Console.WriteLine("How many times was the song played ?");
                        int plays = int.Parse(Console.ReadLine());
                        Console.WriteLine("How long is the Track ?");
                        TimeSpan duration = TimeSpan.Parse(Console.ReadLine());
                        Console.WriteLine("Is the Track suitable ?");
                        bool excplicit = bool.Parse(Console.ReadLine());
                        Console.WriteLine("Item successfully added!");
                        rest.Post(new Tracks()
                        {
                            TrackID = trackid,
                            Title = tracktitle,
                            AlbumID = albumid,
                            ArtistID = artistid,
                            Plays = plays,
                            Duration = duration,
                            IsExplicit = excplicit,

                        }, "track");
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
         * 
         */
    }
}
