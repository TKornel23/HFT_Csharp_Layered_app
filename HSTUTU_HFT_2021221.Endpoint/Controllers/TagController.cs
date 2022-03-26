using HSTUTU_HFT_2021221.Endpoint.Services;
using HSTUTU_HFT_2021221.Logic;
using HSTUTU_HFT_2021221.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        IHubContext<SignalRHub> hub;

        public TagController(ITagLogic tagLogic, IHubContext<SignalRHub> hub)
        {
            this.tagLogic = tagLogic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("TagCreated", newTag);
        }

        [HttpPut]
        public void Put([FromBody] Tag tag)
        {
            tagLogic.UpdateTag(tag);
            this.hub.Clients.All.SendAsync("TagUpdated", tag);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var tag = tagLogic.GetTagById(id);
            tagLogic.DeleteTag(id);
            this.hub.Clients.All.SendAsync("TagDeleted", tag);
        }
    }
}
