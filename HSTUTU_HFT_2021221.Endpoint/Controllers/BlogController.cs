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
    [Route("/blog")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        IBlogLogic blogLogic;
        IHubContext<SignalRHub> hub;

        public BlogController(IBlogLogic blogLogic, IHubContext<SignalRHub> hub)
        {
            this.blogLogic = blogLogic;
            this.hub = hub;
        }
        [HttpGet]
        public IEnumerable<Blog> Get()
        {
            return blogLogic.GetAllBlogs();
        }

        [HttpGet("{id}")]
        public Blog Get(int id)
        {
            return blogLogic.GetBlogById(id);
        }

        [HttpPost]
        public void Post([FromBody] Blog newBlog)
        {
            blogLogic.CreateBlog(newBlog);
            this.hub.Clients.All.SendAsync("BlogCreated", newBlog);
        }

        [HttpPut]
        public void Put([FromBody] Blog blog)
        {
            blogLogic.Update(blog);
            this.hub.Clients.All.SendAsync("BlogUpdated", blog);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var blogToDelete = this.blogLogic.GetBlogById(id);
            blogLogic.DeleteBlog(id);
            this.hub.Clients.All.SendAsync("BlogDeleted", blogToDelete);
        }
    }
}
