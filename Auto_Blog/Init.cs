using Auto_Blog.DAL;
using Auto_Blog.DAL.Repository;
using Auto_Blog.Domain.Entity;
using Auto_Blog.Service.Implementations;
using Auto_Blog.Service.Interfaces;
namespace Auto_Blog
{
    public static class Init
    {
        public static void InitializeContext(this IServiceCollection services)
        {
            services.AddTransient<ApplicationDbContext>();
        }
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddTransient<IMainRepository<Car>, CarRepository>();
            services.AddTransient<IMainRepository<User>, UserRepository>();
            services.AddTransient<IMainRepository<Post>, PostRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddTransient<ICarService, CarService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
        }
    }
}
