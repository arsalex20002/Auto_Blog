
using Auto_Blog.Domain.Enum;

namespace Auto_Blog.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public Role Role { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
