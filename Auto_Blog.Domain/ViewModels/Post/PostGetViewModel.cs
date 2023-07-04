using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Blog.Domain.ViewModels.Post
{
    public class PostGetViewModel
    {
        public string TypeName { get; set; }
        public int PostDate { get; set; }
        public int CarDate { get; set; }
        public string CarName { get; set; }
        public List<string> CarNames { get; set; }
        public List<PostViewModel> PostViewModel { get; set; }
    }
}
