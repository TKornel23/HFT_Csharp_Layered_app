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
    [Route("[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        IBlogLogic blogLogic;

        public BlogController(IBlogLogic blogLogic)
        {
            this.blogLogic = blogLogic;
        }

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
        }

        [HttpPut]
        public void Put([FromBody] Blog blog)
        {
            blogLogic.ChangeBlogTitle(blog);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            blogLogic.DeleteBlog(id);
        }
    }
}
