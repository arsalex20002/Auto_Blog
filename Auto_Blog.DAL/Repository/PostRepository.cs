using Auto_Blog.Domain.Entity;
using Auto_Blog.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Auto_Blog.DAL.Repository
{
    public class PostRepository : IMainRepository<Post>
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Post model)
        {
            await _context.Posts.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Post> GetAll()
        {
            return _context.Posts.Include(p => p.Car).ThenInclude(c => c.CarType);
        }

        public async Task Delete(Post model)
        {
            _context.Posts.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<Post> Update(Post model)
        {
            _context.Posts.Update(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<Post> GetOne(int Id)
        {
            return await _context.Posts.Include(p => p.User).Include(p => p.Car).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public Task<Post> GetOneByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
