using _72HourProject.Data;
using _72HourProject.Models;
using _72HourProject.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _72HourProject.Controllers
{
    public class ReplyController : ApiController
    {
        private ReplyService CreateReplyService()
        {
            var authorId = Guid.Parse(User.Identity.GetUserId());
            var replyService = new ReplyService(authorId);
            return replyService;
        }

        // POST(Create) a Reply to a Comment using a Foreign Key relationship(required)
        [HttpPost]
        public IHttpActionResult CreateReply(ReplyCreate model)
        {
            if (model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }

            var service = CreateReplyService();

            if (!service.CreateReply(model))
            {
                return InternalServerError();
            }
            return Ok();

        }


        //GET Replies By Comment Id(required)
        // api/Reply/{id}
        [HttpGet]
        public IHttpActionResult GetById(int commentId)
        {
            {
                ReplyService replyService = CreateReplyService();
                var reply = replyService.GetReplyByCommentId(commentId);
                return Ok(reply);
            }


        }
    }
}
