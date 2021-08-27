using _72HourProject.Data;
using _72HourProject.Models.PostModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72HourProject.Services.PostServices
{
    public class PostServices
    {
        private readonly Guid _id;

        public PostServices(Guid id)
        {
            _id = id;
        }

        public async Task<bool> Post(PostCreate post)
        {
            Post entity = new Post
            {
                Title = post.Title
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Posts.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<IEnumerable<PostListItem>> Get()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    await
                    ctx
                    .Posts
                    .Where(d => d.AuthorId == _id)
                    .Select(d => new PostListItem
                    {
                        Id = d.Id,
                        Title = d.Title,
                    }).ToListAsync();
                return query;
            }
        }

        public async Task<PostDetail> GetById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var post =
                    await
                    ctx
                    .Posts
                    .Where(d => d.AuthorId == _id)
                    .SingleOrDefaultAsync(d => d.Id == id);
                if (post is null)
                {
                    return null;
                }

                return new PostDetail
                {
                    Title = post.Title,
                    Id = post.Id,
                    Text = post.Text,
                    Comments = post.Comments

                };

            }
        }

        public async Task<bool> Put(PostEdit post, int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldPostData = await ctx.Posts.FindAsync(id);
                if (oldPostData is null)
                {
                    return false;
                }

                oldPostData.Id = post.Id;
                oldPostData.Title = post.Title;
                oldPostData.Text = post.Text;

                return await ctx.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldPostData = await ctx.Posts.FindAsync(id);
                if (oldPostData is null)
                {
                    return false;
                }
                ctx.Posts.Remove(oldPostData);
                return await ctx.SaveChangesAsync() > 1;
            }

        }
    }
}
