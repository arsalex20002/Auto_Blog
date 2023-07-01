using Auto_Blog.DAL.Interfaces;
using Auto_Blog.DAL.Repository;
using Auto_Blog.Domain.Entity;
using Auto_Blog.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auto_Blog.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var response = await _carService.GetCars();
            if (response.Status == Domain.Enum.ErrorStatus.Success)
            {
                return View(response.Data.ToList());
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> GetCar(int id)
        {
            var response = await _carService.GetCar(id);
            if (response.Status == Domain.Enum.ErrorStatus.Success)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }

    }
}
