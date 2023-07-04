using Auto_Blog.Domain.Entity;
using Auto_Blog.Domain.Enum;
using Auto_Blog.Domain.Helpers;
using Auto_Blog.Domain.Responce;
using Auto_Blog.Domain.ViewModels.User;
using Auto_Blog.Service.Interfaces;
using System.Security.Claims;
namespace Auto_Blog.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IMainRepository<User> _UserRepository;

        public UserService(IMainRepository<User> UserRepository)
        {
            _UserRepository = UserRepository;
        }
        public async Task<IBaseResponse<bool>> DeleteUser(int id)
        {
            try
            {
                var user = await _UserRepository.GetOne(id);

                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "user not found",
                        Status = ErrorStatus.UserNotFound,
                        Data = false
                    };
                }

                if (user.Role == Role.Admin)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Вы не можете удалить аккаунт с правами равными вашим",
                        Status = ErrorStatus.InternalServerError,
                        Data = false
                    };
                }

                await _UserRepository.Delete(user);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Status = ErrorStatus.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteCar] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var user = _UserRepository.GetAll();

                if (!user.Any())
                {
                    return new BaseResponse<IEnumerable<User>>()
                    {
                        Description = "Найдено 0 элементов",
                        Status = ErrorStatus.CarNotFound
                    };
                }

                return new BaseResponse<IEnumerable<User>>()
                {
                    Data = user,
                    Status = ErrorStatus.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<User>>()
                {
                    Description = $"[GetUsers] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _UserRepository.GetOneByName(model.Name);

                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден"
                    };
                }

                if (user.Password != HashPasswordHelper.HashPassowrd(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль"
                    };
                }

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Status = ErrorStatus.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    Status = ErrorStatus.InternalServerError
                };
            }
        }
        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };

            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _UserRepository.GetOneByName(model.Name);

                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с таким логином уже есть",
                    };
                }

                user = new User()
                {
                    Name = model.Name,
                    Role = Role.User,
                    Email = model.Email,
                    Password = HashPasswordHelper.HashPassowrd(model.Password),
                };

                await _UserRepository.Create(user);
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Объект добавился",
                    Status = ErrorStatus.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<User>> GetUser(string name)
        {
            try
            {
                var user = await _UserRepository.GetOneByName(name);

                if (user == null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = "Машины не найден",
                        Status = ErrorStatus.CarNotFound
                    };
                }

                return new BaseResponse<User>()
                {
                    Status = ErrorStatus.Success,
                    Data = user
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetCar] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

    }
}
