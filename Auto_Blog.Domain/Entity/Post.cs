using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Blog.Domain.Entity
{
    public class Post
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime EditTime { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
        public Car? Car { get; set; }
        public int CarId { get; set; }
        public bool IsPublic { get; set; }
        public byte[]? Avatar { get; set; }
    }
}
