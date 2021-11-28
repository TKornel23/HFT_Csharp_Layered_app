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

            RestService rest = new RestService("http://localhost:57125");


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
                BlogId = 3,
                Likes = 789,
                Title = "Post Three",
                PostContent = "Lorem ipsum dolores est",
                Blog = newBlog,
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
                BlogId = 3,
                Likes = 125,
                Title = "Post Three",
                PostContent = "Lorem ipsum dolores est",
                Blog = newBlog,
                PostTags = new List<PostTag>()
            };

            Post newPost3 = new Post()
            {
                BlogId = 3,
                Likes = 36,
                Title = "Post Three",
                PostContent = "Lorem ipsum dolores est",
                Blog = newBlog,
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
                Blog = blog,
                BlogId = 4,
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
    }
}
