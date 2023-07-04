using Auto_Blog.Domain.Entity;
using Auto_Blog.Domain.Enum;
using Auto_Blog.Domain.Responce;
using Auto_Blog.Domain.ViewModels.Post;
using Auto_Blog.Service.Interfaces;
using System.Reflection;

namespace Auto_Blog.Service.Implementations
{
    public class PostService : IPostService
    {
        private readonly IMainRepository<Post> _postRepository;
        private readonly ICarService _carService;
        private readonly IUserService _userService;
        public PostService(IMainRepository<Post> postRepository, IUserService userService, ICarService carService)
        {
            _postRepository = postRepository;
            _userService = userService;
            _carService = carService;
        }
        public async Task<IBaseResponse<Post>> Create(PostViewModel model, string userName)
        {
            try
            {
                var responce_car = await _carService.GetCarByName(model.Car);
                var responce_user = await _userService.GetUser(userName);
                //преобразуем фото в массив данных
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                }

                var post = new Post()
                {
                    Name = model.Name,
                    slug = model.Name?.Replace(" ", "-").ToLower() + '-' + DateTime.Now.Year.ToString(),
                    Description = model.Description,
                    DateCreate = DateTime.Now.ToUniversalTime(),
                    EditTime = DateTime.Now,
                    CarId = responce_car.Data.Id,
                    UserId = responce_user.Data.Id,
                    IsPublic = model.IsPublic,
                    Avatar = imageData

                };

                await _postRepository.Create(post);

                return new BaseResponse<Post>()
                {
                    Status = ErrorStatus.Success,
                    Data = post
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Post>()
                {
                    Description = $"[Create] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeletePost(int id)
        {
            try
            {
                var post = await _postRepository.GetOne(id);
                if (post == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "post not found",
                        Status = ErrorStatus.PostNotFound,
                        Data = false
                    };
                }

                await _postRepository.Delete(post);

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
                    Description = $"[DeletePost] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Post>> Edit(int id, PostViewModel model)
        {
            try
            {
                var post = await _postRepository.GetOne(id);
                var car = await _carService.GetCarByName(model.Car);

                if (post == null)
                {
                    return new BaseResponse<Post>()
                    {
                        Description = "Car not found",
                        Status = ErrorStatus.CarNotFound
                    };
                }

                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                }

                post.Avatar = imageData;
                post.Name = model.Name;
                post.slug = model.Name?.Replace(" ", "-").ToLower() + '-' + model.DateCreate.Year.ToString();
                post.CarId = car.Data.Id;
                post.IsPublic = model.IsPublic;
                post.Description = model.Description;
                post.EditTime = DateTime.Now;
                await _postRepository.Update(post);


                return new BaseResponse<Post>()
                {
                    Data = post,
                    Status = ErrorStatus.Success,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Post>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Post>> GetPost(int id)
        {
            try
            {
                var post = await _postRepository.GetOne(id);
                if (post == null)
                {
                    return new BaseResponse<Post>()
                    {
                        Description = "Пост не найден",
                        Status = ErrorStatus.PostNotFound
                    };
                }

                return new BaseResponse<Post>()
                {
                    Status = ErrorStatus.Success,
                    Data = post
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Post>()
                {
                    Description = $"[GetPost] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Post>>> GetPosts(string typeCar = null, string Name = null, int PostDate = 0, int CarDate = 0)
        {
            try
            {
                var posts = _postRepository.GetAll().Where(x => x.IsPublic == true);

                if (!posts.Any())
                {
                    return new BaseResponse<IEnumerable<Post>>()
                    {
                        Description = "Найдено 0 элементов",
                        Status = ErrorStatus.CarNotFound
                    };
                }

                if (PostDate != 0)
                    posts = posts.Where(x => x.DateCreate.Year == PostDate);
                
                if(typeCar != null && typeCar != "all")
                {
                    var post_list = posts.ToList();
                    foreach (var post in post_list)
                        if (post.Car.CarType.TypeName != typeCar)
                            posts = posts.Where(x => x.Id != post.Id);
                        
                }

                if (CarDate != 0)
                {
                    var post_list = posts.ToList();

                    foreach (var post in post_list)
                        if (post.Car.DateCreate.Year != CarDate)
                            posts = posts.Where(x => x.Id != post.Id);  
                }

                if (Name != null && Name != "0")
                {
                    var post_list = posts.ToList();

                    foreach (var post in post_list)
                        if (post.Car.Name != Name)
                            posts = posts.Where(x => x.Id != post.Id);
                }

                return new BaseResponse<IEnumerable<Post>>()
                {
                    Data = posts,
                    Status = ErrorStatus.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Post>>()
                {
                    Description = $"[GetPosts] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

    }
}
