using Microsoft.EntityFrameworkCore;
using Auto_Blog.Domain.Entity;
using Auto_Blog.Service.Interfaces;

namespace Auto_Blog.DAL.Repository
{
    public class CarRepository : IMainRepository<Car>
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Car model)
        {
            await _context.Cars.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Car> GetAll()
        {
            return _context.Cars.Include(c => c.CarType);
        }

        public async Task Delete(Car model)
        {
            _context.Cars.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<Car> Update(Car model)
        {
            _context.Cars.Update(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<Car> GetOne(int Id)
        {
            return await _context.Cars.Include(c => c.CarType).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Car> GetOneByName(string name)
        {
            return await _context.Cars.Include(u => u.Posts).FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
