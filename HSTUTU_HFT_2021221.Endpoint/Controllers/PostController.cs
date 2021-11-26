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
    [Route("/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        IPostLogic postLogic;

        public PostController(IPostLogic postLogic)
        {
            this.postLogic = postLogic;
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
        }

        [HttpPut]
        public void Put([FromBody] Post post)
        {
            postLogic.ChangePostTitle(post);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            postLogic.DeletePost(id);
        }
    }
}
