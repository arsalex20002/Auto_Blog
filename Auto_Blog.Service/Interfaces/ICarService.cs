using Auto_Blog.Domain.Entity;
using Auto_Blog.Domain.Responce;

namespace Auto_Blog.Service.Interfaces
{
    public interface ICarService
    {
        Task<IBaseResponse<IEnumerable<Car>>> GetCars();

        Task<IBaseResponse<Car>> GetCar(int id);

        Task<IBaseResponse<Car>> GetCarByName(string name);

    }
}
