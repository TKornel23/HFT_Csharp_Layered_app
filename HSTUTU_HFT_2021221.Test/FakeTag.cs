using HSTUTU_HFT_2021221.Models;
using HSTUTU_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTUTU_HFT_2021221.Test
{
    public class FakeTag : ITagRepository
    {
        public void ChangeTagName(int id, string name)
        {
            throw new NotImplementedException();
        }

        public void Create(Tag item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tag> GetAll()
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

        public Tag GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Tag t, int id)
        {
            throw new NotImplementedException();
        }
    }
}
