using Auto_Blog.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Auto_Blog.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        public async Task<IActionResult> GetCarModels()
        {
            var response = await _carService.GetCars();

            var models = new List<string>();

            foreach (var item in response.Data)
                models.Add(item.Name);
            
            return Json(models);
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var response = await _carService.GetCars();

            return response.Status == Domain.Enum.ErrorStatus.Success
                ? View(response.Data.ToList())
                : View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> GetCar(int id)
        {
            var response = await _carService.GetCar(id);

            return response.Status == Domain.Enum.ErrorStatus.Success
                ? View(response.Data)
                : View("Error", $"{response.Description}");
        }

    }
}
