using Auto_Blog.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Auto_Blog.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarType> CarsTypes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=AutoBlog;Username=postgres;Password=123123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarType>().HasData(
            new CarType[]
            {
                new CarType {Id = 1, TypeName = "Легковая машина"},
                new CarType {Id = 2, TypeName = "Седан"},
                new CarType {Id = 3, TypeName = "Спорткар"},
                new CarType {Id = 4, TypeName = "груовая"},
                new CarType {Id = 5, TypeName = "автобус"},
            });
            modelBuilder.Entity<Car>()
                .HasOne(c => c.CarType)
                .WithMany(ct => ct.Cars)
                .HasForeignKey(c => c.CarTypeId);
            modelBuilder.Entity<Car>().HasData(
            new Car[]
            {
                new Car { Id = 1, Name = "BMW X6", Description = "Love", DateCreate = new DateTime(2021,7,2), CarTypeId = 1},
                new Car { Id = 2, Name = "BMW X5", Description = "Love", DateCreate = new DateTime(2022,7,2), CarTypeId = 2},
                new Car { Id = 3, Name = "Porshe Panamera", Description = "Love", DateCreate = new DateTime(2023, 7, 2), CarTypeId = 3},
                new Car { Id = 4, Name = "Audi R8", Description = "Love", DateCreate = new DateTime(2019,7,2), CarTypeId = 4},
                new Car { Id = 5, Name = "Camri 3-5", Description = "Love", DateCreate = new DateTime(2021, 7, 2), CarTypeId = 5},
            });
        }

    }
}
