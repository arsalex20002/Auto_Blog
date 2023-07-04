using Auto_Blog.Domain.Entity;
using Auto_Blog.Domain.Enum;
using Auto_Blog.Domain.Responce;
using Auto_Blog.Service.Interfaces;

namespace Auto_Blog.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly IMainRepository<Car> _carRepository;

        public CarService(IMainRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IBaseResponse<Car>> GetCar(int id)
        {
            try
            {
                var car =  _carRepository.GetOne(id);

                if (car == null)
                    return new BaseResponse<Car>()
                    {
                        Description = "Машина не найдена",
                        Status = ErrorStatus.CarNotFound
                    };
                

                return new BaseResponse<Car>()
                {
                    Status = ErrorStatus.Success,
                    Data = car.Result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[GetCar] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Car>> GetCarByName(string name)
        {
            try
            {
                var car = await _carRepository.GetOneByName(name);
                if (car == null)
                {
                    return new BaseResponse<Car>()
                    {
                        Description = "Машины не найден",
                        Status = ErrorStatus.CarNotFound
                    };
                }

                return new BaseResponse<Car>()
                {
                    Status = ErrorStatus.Success,
                    Data = car
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[GetCar] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Car>>> GetCars()
        {
            try
            {
                var cars = _carRepository.GetAll();
                if (!cars.Any())
                {
                    return new BaseResponse<IEnumerable<Car>>()
                    {
                        Description = "Машин не найдено",
                        Status = ErrorStatus.CarNotFound
                    };
                }

                return new BaseResponse<IEnumerable<Car>>()
                {
                    Data = cars,
                    Status = ErrorStatus.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Car>>()
                {
                    Description = $"[GetCars] : {ex.Message}",
                    Status = ErrorStatus.InternalServerError
                };
            }
        }
    }
}
