using System;
using HSTUTU_HFT_2021221.Data;
using HSTUTU_HFT_2021221.Logic;
using NUnit.Framework;
using Moq;
using HSTUTU_HFT_2021221.Repository;
using System.Linq;
using HSTUTU_HFT_2021221.Models;
using System.Collections.Generic;

namespace HSTUTU_HFT_2021221.Test
{
    [TestFixture]
    public class TesterClass
    {
        private BlogLogic blogLogic { get; set; }

        [SetUp]
        public void Setup()
        {
            Mock<IBlogRepository> mockedBlog = new Mock<IBlogRepository>();
            Mock<ITagRepository> mockedTag = new Mock<ITagRepository>();
            Mock<IPostRepository> mockedPost = new Mock<IPostRepository>();

            mockedBlog.Setup(x => x.GetOne(It.IsAny<int>())).Returns(
                new Models.Blog()
                {
                    ID = 1,
                    Title = "Blog Title Uno",                   
                }
             );

            mockedBlog.Setup(x => x.GetAll()).Returns(this.FakeBlogObjects);
            mockedTag.Setup(x => x.GetAll()).Returns(this.FakeTagObjects);
            mockedPost.Setup(x => x.GetAll()).Returns(this.FakePostObjects);

            blogLogic = new BlogLogic(mockedBlog.Object, mockedPost.Object);
        }

        public IQueryable<Post> FakePostObjects()
        {
            Blog b1 = new Blog() { ID = 1, Title = "Title One" };
            Blog b2 = new Blog() { ID = 2, Title = "Title Two" };
            Blog b3 = new Blog() { ID = 3, Title = "Title Three" };
            Blog b4 = new Blog() { ID = 4, Title = "Title Four" };

            Post p1 = new Post() { Id = 1, PostContent = "Lorem", Title = "Post One" };
            Post p2 = new Post() { Id = 2, PostContent = "Ipsum", Title = "Post Two" };
            Post p3 = new Post() { Id = 3, PostContent = "Dolores", Title = "Post Three" };
            Post p4 = new Post() { Id = 4, PostContent = "Est", Title = "Post Four" };
            Post p5 = new Post() { Id = 5, PostContent = "Baeiu", Title = "Post Five" };
            Post p6 = new Post() { Id = 6, PostContent = "Gorag", Title = "Post Six" };

            Tag t1 = new Tag() { Id = 1, Name = "Tag One" };
            Tag t2 = new Tag() { Id = 2, Name = "Tag Two" };
            Tag t3 = new Tag() { Id = 3, Name = "Tag Three" };
            Tag t4 = new Tag() { Id = 4, Name = "Tag Four" };
            Tag t5 = new Tag() { Id = 5, Name = "Tag Five" };

            PostTag pt1 = new PostTag() { BlogId = 1, TagId = 1, PostId = 1 };
            PostTag pt2 = new PostTag() { BlogId = 2, TagId = 2, PostId = 3 };
            PostTag pt3 = new PostTag() { BlogId = 3, TagId = 5, PostId = 2 };
            PostTag pt4 = new PostTag() { BlogId = 4, TagId = 1, PostId = 6 };
            PostTag pt5 = new PostTag() { BlogId = 5, TagId = 4, PostId = 4 };
            PostTag pt6 = new PostTag() { BlogId = 6, TagId = 5, PostId = 1 };

            List<Post> posts = new List<Post>();

            posts.Add(p1);
            posts.Add(p2);
            posts.Add(p3);
            posts.Add(p4);
            posts.Add(p5);

            return posts.AsQueryable();
        }

        public IQueryable<Tag> FakeTagObjects()
        {
            Blog b1 = new Blog() { ID = 1, Title = "Title One" };
            Blog b2 = new Blog() { ID = 2, Title = "Title Two" };
            Blog b3 = new Blog() { ID = 3, Title = "Title Three" };
            Blog b4 = new Blog() { ID = 4, Title = "Title Four" };

            Post p1 = new Post() { Id = 1, PostContent = "Lorem", Title = "Post One" };
            Post p2 = new Post() { Id = 2, PostContent = "Ipsum", Title = "Post Two" };
            Post p3 = new Post() { Id = 3, PostContent = "Dolores", Title = "Post Three" };
            Post p4 = new Post() { Id = 4, PostContent = "Est", Title = "Post Four" };
            Post p5 = new Post() { Id = 5, PostContent = "Baeiu", Title = "Post Five" };
            Post p6 = new Post() { Id = 6, PostContent = "Gorag", Title = "Post Six" };

            Tag t1 = new Tag() { Id = 1, Name = "Tag One" };
            Tag t2 = new Tag() { Id = 2, Name = "Tag Two" };
            Tag t3 = new Tag() { Id = 3, Name = "Tag Three" };
            Tag t4 = new Tag() { Id = 4, Name = "Tag Four" };
            Tag t5 = new Tag() { Id = 5, Name = "Tag Five" };

            PostTag pt1 = new PostTag() { BlogId = 1, TagId = 1, PostId = 1 };
            PostTag pt2 = new PostTag() { BlogId = 1, TagId = 2, PostId = 3 };
            PostTag pt3 = new PostTag() { BlogId = 4, TagId = 5, PostId = 2 };
            PostTag pt4 = new PostTag() { BlogId = 3, TagId = 1, PostId = 6 };
            PostTag pt5 = new PostTag() { BlogId = 2, TagId = 4, PostId = 4 };
            PostTag pt6 = new PostTag() { BlogId = 3, TagId = 5, PostId = 1 };

            List<Tag> tags = new List<Tag>();

            tags.Add(t1);
            tags.Add(t2);
            tags.Add(t3);
            tags.Add(t4);
            tags.Add(t5);

            return tags.AsQueryable();
        }

        public IQueryable<Blog> FakeBlogObjects()
        {
            Blog b1 = new Blog() { ID = 1, Title = "Title One" };
            Blog b2 = new Blog() { ID = 2, Title = "Title Two" };
            Blog b3 = new Blog() { ID = 3, Title = "Title Three" };
            Blog b4 = new Blog() { ID = 4, Title = "Title Four" };

            Post p1 = new Post() { Id = 1, PostContent = "Lorem", Title = "Post One" };
            Post p2 = new Post() { Id = 2, PostContent = "Ipsum", Title = "Post Two" };
            Post p3 = new Post() { Id = 3, PostContent = "Dolores", Title = "Post Three" };
            Post p4 = new Post() { Id = 4, PostContent = "Est", Title = "Post Four" };
            Post p5 = new Post() { Id = 5, PostContent = "Baeiu", Title = "Post Five" };
            Post p6 = new Post() { Id = 6, PostContent = "Gorag", Title = "Post Six" };

            Tag t1 = new Tag() { Id = 1, Name = "Tag One" };
            Tag t2 = new Tag() { Id = 2, Name = "Tag Two" };
            Tag t3 = new Tag() { Id = 3, Name = "Tag Three" };
            Tag t4 = new Tag() { Id = 4, Name = "Tag Four" };
            Tag t5 = new Tag() { Id = 5, Name = "Tag Five" };

            PostTag pt1 = new PostTag() { BlogId = 1, TagId = 1, PostId = 1 };
            PostTag pt2 = new PostTag() { BlogId = 1, TagId = 2, PostId = 3 };
            PostTag pt3 = new PostTag() { BlogId = 4, TagId = 5, PostId = 2 };
            PostTag pt4 = new PostTag() { BlogId = 3, TagId = 1, PostId = 6 };
            PostTag pt5 = new PostTag() { BlogId = 2, TagId = 4, PostId = 4 };
            PostTag pt6 = new PostTag() { BlogId = 3, TagId = 5, PostId = 1 };

            List<Blog> blogs = new List<Blog>();

            blogs.Add(b1);
            blogs.Add(b2);
            blogs.Add(b3);
            blogs.Add(b4);

            return blogs.AsQueryable();
        }
    }


}