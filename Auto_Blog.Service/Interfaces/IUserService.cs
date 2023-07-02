using Auto_Blog.Domain.Entity;
using Auto_Blog.Domain.Responce;
using Auto_Blog.Domain.ViewModels.User;
using System.Security.Claims;


namespace Auto_Blog.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
        Task<IBaseResponse<IEnumerable<User>>> GetUsers();
        Task<IBaseResponse<User>> GetUser(string name);
        Task<IBaseResponse<bool>> DeleteUser(int id);

    }
}
