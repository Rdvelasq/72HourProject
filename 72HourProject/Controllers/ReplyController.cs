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
        private readonly ReplyDbContext _context = new ReplyDbContext();

        // POST(Create) a Reply to a Comment using a Foreign Key relationship(required)
        [HttpPost]
        public async Task<IHttpActionResult> CreateReply([FromBody] Reply model)
        {
            if (model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }
            if (ModelState.IsValid)
            {
                // Store the model into the database
                _context.Reply.Add(model);
                await _context.saveChangesAsync();

                return ok("Your reply is successfully posted!");

            }

            // if the modelstate is not valid
            return BadRequest(ModelState);
        }

        //GET Replies By Comment Id(required)
        // api/Reply/{id
        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            Reply reply = await _context.Replys.FindAsync(id);
            if (reply != null)
            {
                return Ok(reply);
            }

            return NotFound();
        }


    }
}
