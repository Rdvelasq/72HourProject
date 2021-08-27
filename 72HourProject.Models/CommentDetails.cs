using _72HourProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72HourProject.Models
{
    public class CommentDetails
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Guid AuthorId { get; set; }

        public List<Reply> Replies { get; set; } = new List<Reply>();

        public int PostId { get; set; }
    }
}
