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
            Blog blog = new Blog()
            {
                ID = 5,
                Title = "Blog Title Five",
            };
            Post post = new Post()
            {
                BlogId = 5,
                Id = 1,
                PostContent = "HFT Post Content",
                Title = "HFT Rules",
                Likes = 115,                
            };

            Tag tag = new Tag()
            {
                Id = 1,
                Name = "Tag One",
                PostId = 1
            };

            mockedBlog.Setup(x => x.GetOne(It.IsAny<int>())).Returns(blog);
            mockedPost.Setup(x => x.GetOne(It.IsAny<int>())).Returns(post);
            mockedTag.Setup(x => x.GetOne(It.IsAny<int>())).Returns(tag);

            mockedBlog.Setup(x => x.GetAll()).Returns(this.FakeBlogObjects);
            mockedTag.Setup(x => x.GetAll()).Returns(this.FakeTagObjects);
            mockedPost.Setup(x => x.GetAll()).Returns(this.FakePostObjects);

            blogLogic = new BlogLogic(mockedBlog.Object, mockedPost.Object, mockedTag.Object);
            postLogic = new PostLogic(mockedPost.Object, mockedBlog.Object, mockedTag.Object);
            tagLogic = new TagLogic(mockedTag.Object, mockedPost.Object);
        }

        [Test]
        public void GetBlogPostTitleByIdTestReturnTrue()
        {
            var posts = this.blogLogic.GetBlogPostTitleById();
            
            Assert.That(posts.Any(x => x.Value.Contains("Post One")), Is.EqualTo(true));
        }

        [TestCase(2)]
        public void GetTagsByPostId(int id)
        {
            var tags = this.postLogic.GetTagsCountGroupByPost();
            
            Assert.That(tags.Select(x => x.Value).Contains(2), Is.EqualTo(true));
        }

        [Test]
        public void GetPostByTagId()
        {
            var post = this.tagLogic.GetPostByTagId(1);           
            Assert.That(post.Select(x => x).Contains("Post One"), Is.EqualTo(true));
        }

        [Test]
        public void GetAllBlogTagNameById()
        {
            var tags = this.blogLogic.GetAllBlogTagNameById(5);

            Assert.That(tags.Any(x => x.Contains("Tag One")), Is.EqualTo(false));
        }

        [Test]
        public void GetAllBlogPostGroupByBlogTitle()
        {
            var postByBlog = this.blogLogic.GetSumOfPostLikesByBlog();

            Assert.That(postByBlog.Any(x => x.Value == 12), Is.EqualTo(true));
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
        [Test]
        public void UpdatePostErrors()
        {

            Assert.That(() => this.postLogic.ChangePostTitle(new Post() { 
                Title = "Updated", Id = 99, PostContent = "New Content", Likes = 96
            }), Throws.TypeOf<Exception>());
        }
        [Test]
        public void GetOneBlog()
        {
            Assert.That(() => this.blogLogic.GetBlogById(5).Title, Is.EqualTo("Blog Title Five"));
        }


        private IQueryable<Post> FakePostObjects()
        {
            Blog b1 = new Blog() { ID = 1, Title = "Title One" };
            Blog b2 = new Blog() { ID = 2, Title = "Title Two" };
            Blog b3 = new Blog() { ID = 3, Title = "Title Three" };
            Blog b4 = new Blog() { ID = 4, Title = "Title Four" };

            Post p1 = new Post() { Id = 1, PostContent = "Lorem", Title = "Post One", BlogId = 1, Likes = 56 };
            Post p2 = new Post() { Id = 2, PostContent = "Ipsum", Title = "Post Two", BlogId = 2, Likes = 156 };
            Post p3 = new Post() { Id = 3, PostContent = "Dolores", Title = "Post Three", BlogId = 3, Likes = 98 };
            Post p4 = new Post() { Id = 4, PostContent = "Est", Title = "Post Four", BlogId = 2, Likes = 59 };
            Post p5 = new Post() { Id = 5, PostContent = "Baeiu", Title = "Post Five", BlogId = 4, Likes = 12 };
            Post p6 = new Post() { Id = 6, PostContent = "Gorag", Title = "Post Six", BlogId = 1, Likes = 96 };

            Tag t1 = new Tag() { Id = 1, Name = "Tag One", PostId = 1 };
            Tag t2 = new Tag() { Id = 2, Name = "Tag Two", PostId = 2 };
            Tag t3 = new Tag() { Id = 3, Name = "Tag Three", PostId = 4 };
            Tag t4 = new Tag() { Id = 4, Name = "Tag Four", PostId = 1 };
            Tag t5 = new Tag() { Id = 5, Name = "Tag Five", PostId = 5 };

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

            Post p1 = new Post() { Id = 1, PostContent = "Lorem", Title = "Post One", BlogId = 1, Likes = 56 };
            Post p2 = new Post() { Id = 2, PostContent = "Ipsum", Title = "Post Two", BlogId = 2, Likes = 156 };
            Post p3 = new Post() { Id = 3, PostContent = "Dolores", Title = "Post Three", BlogId = 3, Likes = 98 };
            Post p4 = new Post() { Id = 4, PostContent = "Est", Title = "Post Four", BlogId = 2, Likes = 59 };
            Post p5 = new Post() { Id = 5, PostContent = "Baeiu", Title = "Post Five", BlogId = 4, Likes = 12 };
            Post p6 = new Post() { Id = 6, PostContent = "Gorag", Title = "Post Six", BlogId = 1, Likes = 96 };

            Tag t1 = new Tag() { Id = 1, Name = "Tag One", PostId = 1 };
            Tag t2 = new Tag() { Id = 2, Name = "Tag Two", PostId = 2 };
            Tag t3 = new Tag() { Id = 3, Name = "Tag Three", PostId = 4 };
            Tag t4 = new Tag() { Id = 4, Name = "Tag Four", PostId = 1 };
            Tag t5 = new Tag() { Id = 5, Name = "Tag Five", PostId = 5 };
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

            Post p1 = new Post() { Id = 1, PostContent = "Lorem", Title = "Post One", BlogId = 1, Likes = 56 };
            Post p2 = new Post() { Id = 2, PostContent = "Ipsum", Title = "Post Two", BlogId = 2, Likes = 156 };
            Post p3 = new Post() { Id = 3, PostContent = "Dolores", Title = "Post Three", BlogId = 3, Likes = 98 };
            Post p4 = new Post() { Id = 4, PostContent = "Est", Title = "Post Four", BlogId = 2, Likes = 59 };
            Post p5 = new Post() { Id = 5, PostContent = "Baeiu", Title = "Post Five", BlogId = 4, Likes = 12 };
            Post p6 = new Post() { Id = 6, PostContent = "Gorag", Title = "Post Six", BlogId = 1, Likes = 96 };

            Tag t1 = new Tag() { Id = 1, Name = "Tag One", PostId = 1 };
            Tag t2 = new Tag() { Id = 2, Name = "Tag Two", PostId = 2 };
            Tag t3 = new Tag() { Id = 3, Name = "Tag Three", PostId = 4 };
            Tag t4 = new Tag() { Id = 4, Name = "Tag Four", PostId = 1 };
            Tag t5 = new Tag() { Id = 5, Name = "Tag Five", PostId = 5 };

            List<Blog> blogs = new List<Blog>();

            blogs.Add(b1);
            blogs.Add(b2);
            blogs.Add(b3);
            blogs.Add(b4);

            return blogs.AsQueryable();
        }
    }


}