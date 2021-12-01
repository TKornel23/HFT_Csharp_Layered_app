using HSTUTU_HFT_2021221.Models;
using HSTUTU_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HSTUTU_HFT_2021221.Logic
{
    public class TagLogic : ITagLogic
    {
        ITagRepository repo;

        public TagLogic(ITagRepository repo)
        {
            this.repo = repo;
        }

        public void UpdateTag(Tag tag)
        {
            if (repo.GetAll().FirstOrDefault(x => x.Id == tag.Id) != null)
            { repo.Update(tag); }
            else
            {
                throw new Exception("Bad ID");
            }
        }

        public void CreateTag(Tag newTag)
        {
            if(newTag.Name == null || newTag.Name == "")
            {
                throw new InvalidOperationException("Name required");
            }
            else
            {
                repo.Create(newTag);
            }
            
        }

        public void DeleteTag(int id)
        {
            if (repo.GetAll().FirstOrDefault(x => x.Id == id) != null)
            { repo.Delete(id); }
            else
            {
                throw new Exception("Bad ID");
            }
        }

        public IList<Tag> GetAllTags()
        {
            return repo.GetAll().ToList<Tag>();
        }

        public Tag GetTagById(int id)
        {
            if (repo.GetAll().FirstOrDefault(x => x.Id == id) != null)
            { return repo.GetOne(id); }
            else
            {
                throw new Exception("Bad ID");
            }
        }

        public IEnumerable<string> GetPostByTagId(int id)
        {
            return repo.GetOne(id).PostTags.Select(x => x.Post.Title).ToList();
        }
    }
}
