using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace _72HourProject.Models
{
    public class CommentCreate
    {
        [Required]
        [MaxLength(6000)]
        public string Text { get; set; }
    }
}
