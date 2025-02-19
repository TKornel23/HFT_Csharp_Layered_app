﻿using HSTUTU_HFT_2021221.Endpoint.Services;
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
    [Route("/stat")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IBlogLogic blogLogic;
        ITagLogic tagLogic;
        IPostLogic postLogic;
        IHubContext<SignalRHub> hub;

        public StatController(IBlogLogic blogLogic, ITagLogic tagLogic, IPostLogic postLogic, IHubContext<SignalRHub> hub)
        {
            this.blogLogic = blogLogic;
            this.tagLogic = tagLogic;
            this.postLogic = postLogic;
            this.hub = hub;
        }

       [HttpGet("blogposttile")]
        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> GetBlogPostTitleById()
        {
            var q1 = blogLogic.GetBlogPostTitleById();
            return q1;
        }

        [HttpGet("blogtagname/{id}")]
        public IEnumerable<string> GetAllBlogTagNameById(int id)
        {
            return blogLogic.GetAllBlogTagNameById(id);
        }

        [HttpGet("likesum")]
        public IEnumerable<KeyValuePair<string, int>> GetSumOfPostLikesByBlog()
        {
            return blogLogic.GetSumOfPostLikesByBlog();
        }

        [HttpGet("tagsbypost/")]
        public IEnumerable<KeyValuePair<string, int>> GetTagsCountGroupByPost()
        {
            return postLogic.GetTagsCountGroupByPost();
        }

        [HttpGet("postsbytag/{id}")]
        public IEnumerable<string> GetPostByTagId(int id)
        {
            return tagLogic.GetPostByTagId(id);
        }
    }
}
