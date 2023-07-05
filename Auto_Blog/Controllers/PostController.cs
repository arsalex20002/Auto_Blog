using Auto_Blog.Domain.Entity;
using Auto_Blog.Domain.Enum;
using Auto_Blog.Domain.ViewModels.Post;
using Auto_Blog.Service.Implementations;
using Auto_Blog.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Auto_Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICarService _carService;
        public PostController(IPostService postService, ICarService carService)
        {
            _postService = postService;
            _carService = carService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts(string TypeCar, string NameCar, int PostDate, int CarDate)
        {
            var response_cars = await _carService.GetCars();
            var response_posts = await _postService.GetPosts(TypeCar, NameCar, PostDate, CarDate);

            if (response_posts.Status == ErrorStatus.Success)
            {
                List<PostViewModel> posts = new List<PostViewModel>();

                foreach(var post in response_posts.Data)
                {
                    PostViewModel postViewModel = new PostViewModel
                    {
                        Id = post.Id,
                        Name = post.Name,
                        Car = post.Car.CarType.TypeName,
                        Slug = post.slug,
                        Image = post.Avatar,
                        Description = post.Description
                    };

                    posts.Add(postViewModel);
                }

                List<string> names = new List<string>();

                foreach (var car in response_cars.Data)
                    names.Add(car.Name);
                

                PostGetViewModel postGetView = new PostGetViewModel
                {
                    PostViewModel = posts,
                    TypeName = TypeCar,
                    CarName = NameCar,
                    PostDate = PostDate,
                    CarDate = CarDate,
                    CarNames = names
                };

                return View(postGetView);

            }
            return View("Error", $"{response_posts.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> GetPost(int id, string slug)
        {
            var response = await _postService.GetPost(id);

            if (response.Status == Domain.Enum.ErrorStatus.Success)
                return View(response.Data);
            
            return View("Error", $"{response.Description}");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var responce_post = await _postService.GetPost(id);

                if (responce_post.Data.User.Name == User.Identity.Name || User.IsInRole("Admin"))
                {
                    var response_post_delete = await _postService.DeletePost(id);
                    return response_post_delete.Status == Domain.Enum.ErrorStatus.Success
                        ? RedirectToAction("GetPosts", "Post")
                        : View("Error", $"{response_post_delete.Description}");
                    
                }

                return View("Error", "Ошибка доступа");
            }
            
            return View("Error", "Ошибка доступа");
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<string> names = new List<string>();
                var response = await _carService.GetCars();

                foreach (var name in response.Data)
                    names.Add(name.Name);
                

                var Data = new PostCreateViewModel()
                {
                    PostViewModel = null,
                    CarNames = names
                };

                return response.Status == Domain.Enum.ErrorStatus.Success
                    ? View(Data)
                    : View("Error", $"{response.Description}");
               
            }

            return View("Error", "Ошибка доступа к форме");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, PostCreateViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == 0)
                {
                    ModelState.Remove("CarNames");
                    ModelState.Remove("PostViewModel.Image");
                    ModelState.Remove("PostViewModel.Slug");
                    if (ModelState.IsValid)
                    {
                        await _postService.Create(model.PostViewModel, User.Identity.Name);
                        return RedirectToAction("GetPosts", "Post");
                    }
                    
                    return View("Error", "Ошибка отправки формы");
                    
                }
                else
                {
                    ModelState.Remove("CarNames");
                    ModelState.Remove("PostViewModel.Image");
                    ModelState.Remove("PostViewModel.Slug");

                    if (ModelState.IsValid)
                    {
                        var post = await _postService.GetPost(id);

                        if (post.Data.User.Name == User.Identity.Name || User.IsInRole("Admin"))
                        {
                            await _postService.Edit(id, model.PostViewModel);
                            return RedirectToAction("GetPosts", "Post");
                        }
                        
                        return View("Error", "Ошибка доступа к форме");
                        
                    }
                    
                    return View("Error", "Ошибка доступа к форме");
                    


                }
            }
            return View("Error", "Ошибка доступа к форме");
        }
    }
}

   