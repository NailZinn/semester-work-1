using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local_server.Models
{
    internal class Post
    {
        public int PostId { get; }
        public DateTime Date { get; }
        public int UserId { get; }
        public string Content { get; }
    }
}
