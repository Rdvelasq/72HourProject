using _72HourProject.Data;
using _72HourProject.Models;
using _72HourProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72HourProject.Services
{
    public class CommentService
    {
        private readonly Guid _userId;

        public CommentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateComment(CommentCreate model)
        {
            var entity =
                new Comment() // This will create an instance of Comment.
                {
                    Text = model.Text,
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CommentListReplies> GetComments()
        {
            using (var ctx = new ApplicationDbContext()) // This method will allow us to see all replies tht belong to a specific user.
            {
                var query =
                    ctx
                        .Comments
                        .Where(c => c.AuthorId == _userId)
                        .Select(
                        c =>
                        new CommentListReplies
                        {
                            Id = c.Id,
                            Text = c.Text,
                        });
                return query.ToArray();
            }
        }

        public CommentDetails GetCommentByPostId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Comments
                        .Single(c => c.PostId == id);
                return
                    new CommentDetails
                    {
                        AuthorId = entity.AuthorId,
                        Text = entity.Text,
                    };
            }
        }

        public CommentService GetCommentByAuthorId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Comments
                        .Single(c => c.AuthorId == id);
                return
                    new CommentDetails
                    {
                        PostId = entity.PostId,
                        Text = entity.Text,
                    };
            }
        }

        public bool UpdateComment(CommentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Comments
                        .Single(c => c.AuthorId == _userId);

                entity.Text = model.Text;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteComment(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Comments
                        .Single(c => c.AuthorId == _userId);
                ctx.Comments.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
