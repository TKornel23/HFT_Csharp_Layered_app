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
        public void Update(Tag tag)
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

        public Tag GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Tag t, int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateTag(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}
