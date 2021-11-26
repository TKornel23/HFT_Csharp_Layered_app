using HSTUTU_HFT_2021221.Logic;
using HSTUTU_HFT_2021221.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSTUTU_HFT_2021221.Endpoint.Controllers
{
    [Route("/tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        ITagLogic tagLogic;

        public TagController(ITagLogic tagLogic)
        {
            this.tagLogic = tagLogic;
        }
        [HttpGet]
        public IEnumerable<Tag> Get()
        {
            return tagLogic.GetAllTags();
        }

        [HttpGet("{id}")]
        public Tag Get(int id)
        {
            return tagLogic.GetTagById(id);
        }

        [HttpPost]
        public void Post([FromBody] Tag newTag)
        {
            tagLogic.CreateTag(newTag);
        }

        [HttpPut]
        public void Put([FromBody] Tag tag)
        {
            tagLogic.UpdateTag(tag);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            tagLogic.DeleteTag(id);
        }
    }
}
