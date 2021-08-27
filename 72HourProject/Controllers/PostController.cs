using _72HourProject.Models.PostModels;
using _72HourProject.Services.PostServices;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace _72HourProject.Controllers
{
    public class PostController : ApiController
    {
        private PostServices CreatePostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var postServices = new PostServices(userId);
            return postServices;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(PostCreate post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreatePostService();
            if (await service.Post(post))
            {
                return InternalServerError();
            }
            return Ok();
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            PostServices postServices = CreatePostService();
            var post = await postServices.Get();
            return Ok(post);
        }
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            PostServices postServices = CreatePostService();
            var post = await postServices.GetById(id);
            return Ok(post);
        }

        public async Task<IHttpActionResult> Put(PostEdit post, int editPostId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreatePostService();
            if (await service.Put(post, editPostId))
            {
                return Ok();
            }
            return InternalServerError();
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var service = CreatePostService();
            if (await service.Delete(id))
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}
