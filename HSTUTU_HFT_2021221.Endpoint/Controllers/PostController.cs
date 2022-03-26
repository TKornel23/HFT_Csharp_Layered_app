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
    [Route("/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        IPostLogic postLogic;
        IHubContext<SignalRHub> hub;

        public PostController(IPostLogic postLogic, IHubContext<SignalRHub> hub)
        {
            this.postLogic = postLogic;
            this.hub = hub;
        }
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return postLogic.GetAllPosts();
        }

        [HttpGet("{id}")]
        public Post Get(int id)
        {
            return postLogic.GetOnePost(id);
        }

        [HttpPost]
        public void Post([FromBody] Post newPost)
        {
            postLogic.CreatePost(newPost);
            this.hub.Clients.All.SendAsync("PostCreated", newPost);
        }

        [HttpPut]
        public void Put([FromBody] Post post)
        {
            postLogic.ChangePostTitle(post);
            this.hub.Clients.All.SendAsync("PostUpdated", post);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var post = postLogic.GetOnePost(id);
            postLogic.DeletePost(id);
            this.hub.Clients.All.SendAsync("PostDeleted", post);
        }

        [HttpGet("getposts/{id}")]
        public IEnumerable<Post> GetPostsByBlogId(int id)
        {
            return postLogic.GetPostsByBlogId(id);
        }
    }
}
