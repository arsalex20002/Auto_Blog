using Auto_Blog.Domain.Entity;
using Auto_Blog.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Auto_Blog.DAL.Repository
{
    public class UserRepository : IMainRepository<User>
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(User model)
        {
            await _context.Users.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }

        public async Task Delete(User model)
        {
            _context.Users.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<User> Update(User model)
        {
            _context.Users.Update(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<User> GetOne(int Id)
        {
            return await _context.Users.Include(u => u.Posts).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<User> GetOneByName(string name)
        {
            return await _context.Users.Include(u => u.Posts).FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
