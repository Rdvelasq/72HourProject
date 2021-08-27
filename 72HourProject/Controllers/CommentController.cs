using _72HourProject.Models;
using _72HourProject.Services;
using Microsoft.AspNet.Identity;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _72HourProject.Controllers
{
    [Authorize]
    public class CommentController : ApiController
    {
        private CommentService CreateCommentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new CommentService(userId);
            return svc;
        }

        // GET Comments By Post Id (required)
        [HttpGet]
        public IHttpActionResult Get(int PostID)
        {
            CommentService commentService = CreateCommentService();
            var comments = commentService.GetCommentByPostId(PostID);
            return Ok(comments);
        }

        // POST (Create) a Comment on a Post using a Foreign Key relationship (required)
        [HttpPost]
        public IHttpActionResult Post(CommentCreate comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var svc = CreateCommentService();

            if (!svc.CreateComment(comment))
                return InternalServerError();

            return Ok();
        }

        // GET Comments By Author Id
        [HttpGet]
        public IHttpActionResult GetAll(Guid authorId)
        {
            CommentService commentService = CreateCommentService();
            var comment = commentService.GetCommentByAuthorId(authorId);
            return Ok(comment);
        }

        // Update a Comment
        [HttpPut]
        public IHttpActionResult Put(CommentEdit comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var svc = CreateCommentService();

            if (!svc.UpdateComment(comment))
                return InternalServerError();

            return Ok();
        }

        // Delete a Comment
        [HttpDelete]
        public IHttpActionResult Delete(int Id)
        {
            var svc = CreateCommentService();

            if (!svc.DeleteComment(Id))
                return InternalServerError();

            return Ok();
        }
    }
}
