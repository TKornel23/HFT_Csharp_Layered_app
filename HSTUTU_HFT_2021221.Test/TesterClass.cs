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
        private PostLogic postLogic { get; set; }
        private TagLogic tagLogic { get; set; }

        [SetUp]
        public void Setup()
        {
            Mock<IBlogRepository> mockedBlog = new Mock<IBlogRepository>();
            Mock<ITagRepository> mockedTag = new Mock<ITagRepository>();
            Mock<IPostRepository> mockedPost = new Mock<IPostRepository>();

            mockedBlog.Setup(x => x.GetAll()).Returns(this.FakeBlogObjects);
            mockedTag.Setup(x => x.GetAll()).Returns(this.FakeTagObjects);
            mockedPost.Setup(x => x.GetAll()).Returns(this.FakePostObjects);

            blogLogic = new BlogLogic(mockedBlog.Object, mockedPost.Object);
            postLogic = new PostLogic(mockedPost.Object);
            tagLogic = new TagLogic(mockedTag.Object);
        }

        [Test]
        public void GetBlogPostTitleByIdTest()
        {
            var posts = this.blogLogic.GetBlogPostTitleById(1);
            
            Assert.That(posts.Any(x => x.Contains("Post One")), Is.EqualTo(1));
        }

        [Test]
        public void GetTagsByPostId()
        {
            var tags = this.postLogic.GetTagsByPostId(2);

            Assert.That(tags.Any(x => x.Contains("Tag Five")), Is.EqualTo(1));
        }

        [Test]
        public void GetPostByTagId()
        {
            var post = this.tagLogic.GetPostByTagId(1);

            Assert.That(post.Any(x => x.Contains("Tag One")), Is.EqualTo(1));
        }

        [Test]
        public void GetAllBlogTagNameById()
        {
            var tags = this.blogLogic.GetAllBlogTagNameById(2);

            Assert.That(tags.Any(x => x.Contains("Tag Two")), Is.EqualTo(1));
        }

        [Test]
        public void GetAllBlogPostGroupByBlogTitle()
        {
            var postByBlog = this.blogLogic.GetAllBlogPostGroupByBlogTitle();

            Assert.That(postByBlog.Any(x => x.Value.Contains("Post One")), Is.EqualTo(1));
        }

        [Test]
        public void TestTagCreateError()
        {
            Tag newTag = new Tag() { Id = 6, Name = "" };

            Assert.That(() => tagLogic.CreateTag(newTag), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void TestBlogCreateError()
        {
            Blog newBlog = new Blog() { ID = 5, Title = null };

            Assert.That(() => blogLogic.CreateBlog(newBlog), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void TestPostCreateError()
        {
            Post newPost = new Post() { Id = 7, Title = "", PostContent = null};

            Assert.That(() => postLogic.CreatePost(newPost), Throws.TypeOf<InvalidOperationException>());
        }

        [TestCase(1, null)]
        [TestCase(1, "")]
        [TestCase(1000, "asd")]
        [TestCase(0,"New Title")]
        public void UpdatePostErrors(int id, string title)
        {
            Assert.That(() => this.postLogic.ChangePostTitle(id, title), Throws.TypeOf<Exception>());
        }
        [Test]
        public void GetOneBlog()
        {
            Assert.That(() => this.blogLogic.GetBlogById(1), !Throws.TypeOf<Exception>());
        }


        private IQueryable<Post> FakePostObjects()
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
            PostTag pt7 = new PostTag() { BlogId = 1, TagId = 1, PostId = 1 };

            List<Post> posts = new List<Post>();

            posts.Add(p1);
            posts.Add(p2);
            posts.Add(p3);
            posts.Add(p4);
            posts.Add(p5);
            posts.Add(p6);

            return posts.AsQueryable();
        }

        private IQueryable<Tag> FakeTagObjects()
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
            PostTag pt7 = new PostTag() { BlogId = 1, TagId = 1, PostId = 1 };

            List<Tag> tags = new List<Tag>();

            tags.Add(t1);
            tags.Add(t2);
            tags.Add(t3);
            tags.Add(t4);
            tags.Add(t5);

            return tags.AsQueryable();
        }

        private IQueryable<Blog> FakeBlogObjects()
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
            PostTag pt7 = new PostTag() { BlogId = 1, TagId = 1, PostId = 1 };

            List<Blog> blogs = new List<Blog>();

            blogs.Add(b1);
            blogs.Add(b2);
            blogs.Add(b3);
            blogs.Add(b4);

            return blogs.AsQueryable();
        }
    }


}