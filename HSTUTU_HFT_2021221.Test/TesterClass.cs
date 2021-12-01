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
                PostTags = new List<PostTag>()
            };
            Post post = new Post()
            {
                BlogId = 5,
                Id = 1,
                PostContent = "HFT Post Content",
                Title = "HFT Rules",
                Likes = 115,
                PostTags = new List<PostTag>()
            };

            Tag tag = new Tag()
            {
                Id = 1,
                Name = "Tag One",
                PostTags = new List<PostTag>()
            };
            blog.PostTags.Add(new PostTag() { BlogId = 5, PostId = 1, TagId = 1, Blog = blog, Tag = tag, Post = post });
            tag.PostTags.Add(new PostTag() { BlogId = 5, PostId = 1, TagId = 1, Blog = blog, Tag = tag, Post = post });
            post.PostTags.Add(new PostTag() { BlogId = 5, PostId = 1, TagId = 1, Blog = blog, Tag = tag, Post = post });

            mockedBlog.Setup(x => x.GetOne(It.IsAny<int>())).Returns(blog);
            mockedPost.Setup(x => x.GetOne(It.IsAny<int>())).Returns(post);
            mockedTag.Setup(x => x.GetOne(It.IsAny<int>())).Returns(tag);

            mockedBlog.Setup(x => x.GetAll()).Returns(this.FakeBlogObjects);
            mockedTag.Setup(x => x.GetAll()).Returns(this.FakeTagObjects);
            mockedPost.Setup(x => x.GetAll()).Returns(this.FakePostObjects);

            blogLogic = new BlogLogic(mockedBlog.Object, mockedPost.Object);
            postLogic = new PostLogic(mockedPost.Object);
            tagLogic = new TagLogic(mockedTag.Object);
        }

        [Test]
        public void GetBlogPostTitleByIdTestReturnFalse()
        {
            var posts = this.blogLogic.GetBlogPostTitleById(5);
            
            Assert.That(posts.Any(x => x.Contains("HFT Rules")), Is.EqualTo(false));
        }

        [TestCase(2)]
        public void GetTagsByPostId(int id)
        {
            var tags = this.postLogic.GetTagsByPostId(id);
            
            Assert.That(tags.Any(x => x.Contains("No comments")), Is.EqualTo(false));
        }

        [Test]
        public void GetPostByTagId()
        {
            var post = this.tagLogic.GetPostByTagId(1);

            Assert.That(post.Any(x => x.Contains("HFT Rules")), Is.EqualTo(true));
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

            Post p1 = new Post() { Id = 1, PostContent = "Lorem", Title = "Post One", Likes = 12, BlogId = 1 };
            Post p2 = new Post() { Id = 2, PostContent = "Ipsum", Title = "Post Two", Likes = 87, BlogId = 3 };
            Post p3 = new Post() { Id = 3, PostContent = "Dolores", Title = "Post Three", Likes = 123, BlogId = 2 };
            Post p4 = new Post() { Id = 4, PostContent = "Est", Title = "Post Four", Likes = 91, BlogId = 3 };
            Post p5 = new Post() { Id = 5, PostContent = "Baeiu", Title = "Post Five", Likes = 36, BlogId = 4 };
            Post p6 = new Post() { Id = 6, PostContent = "Gorag", Title = "Post Six", Likes = 74, BlogId = 4 };

            Tag t1 = new Tag() { Id = 1, Name = "Tag One" };
            Tag t2 = new Tag() { Id = 2, Name = "Tag Two" };
            Tag t3 = new Tag() { Id = 3, Name = "Tag Three" };
            Tag t4 = new Tag() { Id = 4, Name = "Tag Four" };
            Tag t5 = new Tag() { Id = 5, Name = "Tag Five" };

            PostTag pt1 = new PostTag() { BlogId = 1, TagId = 1, PostId = 1, Blog = b1, Post = p1, Tag = t1 };
            PostTag pt2 = new PostTag() { BlogId = 2, TagId = 2, PostId = 3, Blog = b2, Post = p3, Tag = t2 };
            PostTag pt3 = new PostTag() { BlogId = 3, TagId = 5, PostId = 2, Blog = b3, Tag = t5, Post = p2 };
            PostTag pt4 = new PostTag() { BlogId = 4, TagId = 1, PostId = 6, Blog = b4, Tag = t1, Post = p6 };
            PostTag pt5 = new PostTag() { BlogId = 3, TagId = 4, PostId = 4, Blog = b3, Post = p4, Tag = t4 };
            PostTag pt6 = new PostTag() { BlogId = 4, TagId = 5, PostId = 5, Blog = b4, Tag = t5, Post = p5};
            PostTag pt7 = new PostTag() { BlogId = 1, TagId = 1, PostId = 1, Blog = b1, Post = p1, Tag = t1 };

            List<Post> posts = new List<Post>();
            p1.PostTags = new List<PostTag>();
            p2.PostTags = new List<PostTag>();
            p3.PostTags = new List<PostTag>();
            p4.PostTags = new List<PostTag>();
            p5.PostTags = new List<PostTag>();
            p6.PostTags = new List<PostTag>();
            p1.PostTags.Add(pt1);
            p1.PostTags.Add(pt7);
            p2.PostTags.Add(pt3);
            p3.PostTags.Add(pt2);
            p4.PostTags.Add(pt5);
            p5.PostTags.Add(pt6);
            p6.PostTags.Add(pt4);

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

            PostTag pt1 = new PostTag() { BlogId = 1, TagId = 1, PostId = 1, Blog = b1, Post = p1, Tag = t1 };
            PostTag pt2 = new PostTag() { BlogId = 2, TagId = 2, PostId = 3, Blog = b2, Post = p3, Tag = t2 };
            PostTag pt3 = new PostTag() { BlogId = 3, TagId = 5, PostId = 2, Blog = b3, Tag = t5, Post = p2 };
            PostTag pt4 = new PostTag() { BlogId = 4, TagId = 1, PostId = 6, Blog = b4, Tag = t1, Post = p6 };
            PostTag pt5 = new PostTag() { BlogId = 3, TagId = 4, PostId = 4, Blog = b3, Post = p4, Tag = t4 };
            PostTag pt6 = new PostTag() { BlogId = 4, TagId = 5, PostId = 5, Blog = b4, Tag = t5, Post = p5 };
            PostTag pt7 = new PostTag() { BlogId = 1, TagId = 1, PostId = 1, Blog = b1, Post = p1, Tag = t1 };

            List<Tag> tags = new List<Tag>();

            t1.PostTags = new List<PostTag>();
            t2.PostTags = new List<PostTag>();
            t3.PostTags = new List<PostTag>();
            t4.PostTags = new List<PostTag>();
            t5.PostTags = new List<PostTag>();
            t1.PostTags.Add(pt7);
            t1.PostTags.Add(pt1);
            t1.PostTags.Add(pt1);
            t2.PostTags.Add(pt2);
            t4.PostTags.Add(pt5);
            t5.PostTags.Add(pt6);
            t5.PostTags.Add(pt3);


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

            PostTag pt1 = new PostTag() { BlogId = 1, TagId = 1, PostId = 1, Blog = b1, Post = p1, Tag = t1 };
            PostTag pt2 = new PostTag() { BlogId = 2, TagId = 2, PostId = 3, Blog = b2, Post = p3, Tag = t2 };
            PostTag pt3 = new PostTag() { BlogId = 3, TagId = 5, PostId = 2, Blog = b3, Tag = t5, Post = p2 };
            PostTag pt4 = new PostTag() { BlogId = 4, TagId = 1, PostId = 6, Blog = b4, Tag = t1, Post = p6 };
            PostTag pt5 = new PostTag() { BlogId = 3, TagId = 4, PostId = 4, Blog = b3, Post = p4, Tag = t4 };
            PostTag pt6 = new PostTag() { BlogId = 4, TagId = 5, PostId = 5, Blog = b4, Tag = t5, Post = p5 };
            PostTag pt7 = new PostTag() { BlogId = 1, TagId = 1, PostId = 1, Blog = b1, Post = p1, Tag = t1 };

            List<Blog> blogs = new List<Blog>();
            b1.PostTags = new List<PostTag>();
            b2.PostTags = new List<PostTag>();
            b3.PostTags = new List<PostTag>();
            b4.PostTags = new List<PostTag>();

            b1.PostTags.Add(pt1);
            b1.PostTags.Add(pt2);
            b2.PostTags.Add(pt5);
            b3.PostTags.Add(pt4);
            b3.PostTags.Add(pt6);
            b4.PostTags.Add(pt3);

            blogs.Add(b1);
            blogs.Add(b2);
            blogs.Add(b3);
            blogs.Add(b4);

            return blogs.AsQueryable();
        }
    }


}