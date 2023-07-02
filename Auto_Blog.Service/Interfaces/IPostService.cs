using Auto_Blog.Domain.Entity;
using Auto_Blog.Domain.Responce;
using Auto_Blog.Domain.ViewModels.Post;

namespace Auto_Blog.Service.Interfaces
{
    public interface IPostService
    {
        Task<IBaseResponse<IEnumerable<Post>>> GetPosts(string Name = null, string Type = null, int PostDate = 0, int CarDate = 0);
        Task<IBaseResponse<Post>> GetPost(int id);
        Task<IBaseResponse<Post>> Create(PostViewModel car, string userName);
        Task<IBaseResponse<bool>> DeletePost(int id);
        Task<IBaseResponse<Post>> Edit(int id, PostViewModel model);
    }
}
