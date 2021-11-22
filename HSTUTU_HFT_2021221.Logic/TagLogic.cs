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

        public void ChangeTagName(int id, string title)
        {
            repo.ChangeTagName(id, title);
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
            repo.Delete(id);
        }

        public IList<Tag> GetAllBlogs()
        {
            return repo.GetAll().ToList<Tag>();
        }

        public Tag GetTagById(int id)
        {
            return repo.GetOne(id);
        }

        public IEnumerable<string> GetPostByTagId(int id)
        {
            return repo.GetOne(id).PostTags.Select(x => x.Post.Title).ToList();
        }
    }
}
