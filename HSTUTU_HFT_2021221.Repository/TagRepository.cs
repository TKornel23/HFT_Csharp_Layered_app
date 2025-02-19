﻿using HSTUTU_HFT_2021221.Data;
using HSTUTU_HFT_2021221.Models;
using System.Collections.Generic;
using System.Linq;

namespace HSTUTU_HFT_2021221.Repository
{
    public class TagRepository : Repository<Tag>, ITagRepository, IReposotiry<Tag>
    {
        public TagRepository(BlogDbContext ctx) : base(ctx)
        {
        }

        public override void Update(Tag tag)
        {
            var currentTag = _ctx.Tags.FirstOrDefault(x => x.Id == tag.Id);
            currentTag.Name = tag.Name;
            _ctx.SaveChanges();
        }

        public override void Create(Tag item)
        {
            _ctx.Tags.Add(item);
            _ctx.SaveChanges();
        }

        public override void Delete(int id)
        {
            Tag item = _ctx.Tags.FirstOrDefault<Tag>(x => x.Id == id);
            _ctx.Tags.Remove(item);
            _ctx.SaveChanges();
        }

        public override Tag GetOne(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
