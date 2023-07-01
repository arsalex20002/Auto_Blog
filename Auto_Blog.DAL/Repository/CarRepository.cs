using Auto_Blog.DAL.Interfaces;
using Auto_Blog.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;

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
            return _context.Cars;
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
    }
}
