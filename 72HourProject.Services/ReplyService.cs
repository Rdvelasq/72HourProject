using _72HourProject.Data;
using _72HourProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72HourProject.Services
{
    public class ReplyService
    {
        private readonly Guid _authorId;
        private Guid authorId;

        public ReplyService(Guid autorId)
        {
            _authorId = authorId;
        }

        public bool CreateReply(ReplyCreate reply)
        {
            var entity =
                new Reply()
                {
                    AuthorId = _authorId,
                    CommentId = reply.CommentId,
                    Text = reply.Text,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Replies.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ReplyListItem> GetReplies()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Replies
                    .Where(e => e.AuthorId == _authorId)
                    .Select(
                        e =>
                        new ReplyListItem
                        {
                            ReplyId = e.Id,
                            CommentId = e.CommentId,
                            Text = e.Text
                        }
                        );
                return query.ToArray();
            }
        }

        public ReplyDetails GetReplyByCommentId(int commentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Replies
                        .Single(e => e.Id == commentId && e.AuthorId == _authorId);
                return new ReplyDetails
                {
                    ReplyId = entity.Id,
                    Text = entity.Text,
                    CommentId = entity.CommentId,
                };
            }
        }
    }
}
