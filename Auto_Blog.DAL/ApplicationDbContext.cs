using Auto_Blog.Domain.Entity;
using Auto_Blog.Domain.Enum;
using Auto_Blog.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Auto_Blog.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<CarType> CarsTypes { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Car>()
            .HasOne(c => c.CarType)
            .WithMany(ct => ct.Cars)
            .HasForeignKey(c => c.CarTypeId);

            modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Post>()
            .HasOne(p => p.Car)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CarId);

            var lightCar = new CarType { Id = 1, TypeName = "lightCar" };
            var sedan = new CarType { Id = 2, TypeName = "sedan" };
            var sportCar = new CarType { Id = 3, TypeName = "sportCar" };
            var truck = new CarType { Id = 4, TypeName = "truck" };
            var bus = new CarType { Id = 5, TypeName = "bus" };
            modelBuilder.Entity<CarType>().HasData(lightCar, sedan, sportCar, truck, bus);

            var Car1 = new Car { Id = 1, Name = "BMW X6", Description = "Love", DateCreate = new DateTime(2021, 7, 2), CarTypeId = lightCar.Id};
            var Car2 = new Car { Id = 2, Name = "BMW X5", Description = "Love", DateCreate = new DateTime(2022, 7, 2), CarTypeId = sedan.Id };
            var Car3 = new Car { Id = 3, Name = "Porshe Panamera", Description = "Love", DateCreate = new DateTime(2023, 7, 2), CarTypeId = sportCar.Id };
            var Car4 = new Car { Id = 4, Name = "Audi R8", Description = "Love", DateCreate = new DateTime(2019, 7, 2), CarTypeId = truck.Id };
            var Car5 = new Car { Id = 5, Name = "Camri 3-5", Description = "Love", DateCreate = new DateTime(2021, 7, 2), CarTypeId = sedan.Id };
            modelBuilder.Entity<Car>().HasData(Car1, Car2, Car3, Car4, Car5);

            var User1 = new User { Id = 1, Name = "Main", Password = HashPasswordHelper.HashPassowrd("123123"), Email = "fortun2@list.ru", Role = Role.Admin };
            var User2 = new User { Id = 2, Name = "Alex", Password = HashPasswordHelper.HashPassowrd("123123"), Email = "fortun3@list.ru", Role = Role.User };
            var User3 = new User { Id = 3, Name = "Petr", Password = HashPasswordHelper.HashPassowrd("123123"), Email = "fortun4@list.ru", Role = Role.User };
            modelBuilder.Entity<User>().HasData(User1, User2, User3);

            string imagePath = "wwwroot/StartDB/1.png";
            byte[] imageBytes_1 = File.ReadAllBytes(imagePath);
            imagePath = "wwwroot/StartDB/2.png";
            byte[] imageBytes_2 = File.ReadAllBytes(imagePath);
            imagePath = "wwwroot/StartDB/3.png";
            byte[] imageBytes_3 = File.ReadAllBytes(imagePath);
            imagePath = "wwwroot/StartDB/4.png";
            byte[] imageBytes_4 = File.ReadAllBytes(imagePath);
            imagePath = "wwwroot/StartDB/5.png";
            byte[] imageBytes_5 = File.ReadAllBytes(imagePath);

            var Post1 = new Post { Id = 1, UserId = User1.Id, CarId = Car1.Id, Name = "Любимая тачка", Description = "BMW X6 - это луксозный кроссовер среднего размера, выпускаемый немецким автопроизводителем BMW. Он представляет собой спортивный и стильный автомобиль, сочетающий в себе черты купе и внедорожника.\r\n\r\nВнешний дизайн BMW X6 характеризуется динамичными линиями, скругленным профилем крыши и массивными колесными арками. Это создает впечатление элегантности и спортивности. Фирменная решетка радиатора BMW и характерные светодиодные фары добавляют автомобилю узнаваемости.\r\n\r\nИнтерьер BMW X6 сочетает в себе роскошь, комфорт и передовые технологии. Просторный салон выполнен из высококачественных материалов, а сиденья обеспечивают комфортную посадку и поддержку. Многофункциональный руль, центральный дисплей с информационно-развлекательной системой и передовые системы безопасности создают современное и удобное рабочее место для водителя.\r\n\r\nBMW X6 оснащен мощными и эффективными двигателями, которые обеспечивают динамичное ускорение и отличную проходимость. Различные варианты привода, включая полный привод xDrive, позволяют адаптироваться к различным дорожным условиям.\r\n\r\nЭтот автомобиль предлагает широкий спектр технологий и функций, включая современные системы связи, развлечений и помощи водителю. Навигационная система, система адаптивного круиз-контроля, система распознавания дорожных знаков и множество других возможностей делают вождение комфортным и безопасным.\r\n\r\nBMW X6 является элегантным и спортивным автомобилем, предлагающим высокую производительность и роскошь. Он идеально подходит для тех, кто ценит стиль, комфорт и уверенность на дороге.", DateCreate = new DateTime(2018, 7, 2), EditTime = new DateTime(2018, 7, 2), IsPublic = true, Avatar = imageBytes_1 };
            var Post2 = new Post { Id = 2, UserId = User2.Id, CarId = Car2.Id, Name = "Нравится гонять ?", Description = "BMW X5 - это престижный и мощный внедорожник, выпускаемый немецким автопроизводителем BMW. Этот автомобиль сочетает в себе элегантность, комфорт и высокую производительность, предлагая водителю и пассажирам уникальный опыт вождения.\r\n\r\nВнешний дизайн BMW X5 выделяется гармоничными пропорциями, утонченными линиями и уверенным характером. Крупные колеса, массивные бамперы и характерная двухъярусная решетка радиатора создают сильный и впечатляющий облик. Все эти детали воплощают силу и динамизм.\r\n\r\nИнтерьер BMW X5 сочетает в себе роскошь, функциональность и передовые технологии. Просторный салон с отделкой из высококачественных материалов предлагает комфортабельные сиденья и множество опций для настройки и наслаждения путешествием. Интуитивно понятная информационно-развлекательная система и передовые системы безопасности обеспечивают удобство и защиту.\r\n\r\nBMW X5 предлагает широкий спектр мощных и эффективных двигателей, обеспечивающих динамичное ускорение и высокую проходимость. Продвинутые системы подвески и привода, включая полный привод xDrive, позволяют адаптироваться к различным дорожным условиям и обеспечивают уверенное управление.\r\n\r\nЭтот автомобиль оснащен передовыми технологиями, которые делают вождение более комфортным и безопасным. Навигационная система с широкими возможностями, системы активного круиз-контроля, помощи при парковке и множество других инноваций помогают водителю контролировать автомобиль и максимально наслаждаться путешествием.\r\n\r\nBMW X5 сочетает в себе роскошь, мощность и современные технологии, предлагая высокую производительность и комфорт.", DateCreate = new DateTime(2019, 7, 2), EditTime = new DateTime(2019, 7, 2), IsPublic = true, Avatar = imageBytes_2 };
            var Post3 = new Post { Id = 3, UserId = User2.Id, CarId = Car3.Id, Name = "Сел и больше не вставал", Description = "Porsche Panamera - это легендарный спортивный седан, выпускаемый немецким автопроизводителем Porsche. Этот автомобиль сочетает в себе роскошь, производительность и уникальный дизайн, предлагая водителю и пассажирам непревзойденный комфорт и динамичное вождение.\r\n\r\nВнешний дизайн Porsche Panamera выделяется изящными линиями, спортивными пропорциями и уникальной харизмой. Этот автомобиль сочетает в себе элементы спортивного купе и элегантного седана, создавая впечатляющий образ. Характерная передняя часть с яркими фарами, динамичный профиль и аэродинамические детали подчеркивают его спортивный характер.\r\n\r\nИнтерьер Porsche Panamera олицетворяет роскошь и современность. Отделка высококачественными материалами, комфортабельные сиденья и передовые технологии создают атмосферу уюта и комфорта. Приборная панель и центральная консоль оборудованы передовыми системами информационно-развлекательного комплекса и навигации, предлагая широкий выбор функций и настроек.\r\n\r\nPorsche Panamera предлагает широкий спектр мощных двигателей, которые обеспечивают впечатляющую производительность и динамические характеристики. Отзывчивость и мощность двигателей Porsche в сочетании с передовыми системами подвески и управления обеспечивают уверенное и плавное вождение.", DateCreate = new DateTime(2020, 7, 2), EditTime = new DateTime(2020, 7, 2), IsPublic = true, Avatar = imageBytes_3 };
            var Post4 = new Post { Id = 4, UserId = User3.Id, CarId = Car4.Id, Name = "Люблю ее больше жены", Description = "Audi R8 - это легендарный спортивный автомобиль, производимый немецким автопроизводителем Audi. Этот автомобиль является воплощением роскоши, стиля и высокой производительности, предлагая уникальный опыт вождения и эстетическое удовольствие.\r\n\r\nВнешний дизайн Audi R8 восхищает своим агрессивным и элегантным видом. Отличительными особенностями являются острые линии, мускулистые формы и харизматичная передняя часть с узнаваемой решеткой радиатора. Особое внимание уделяется аэродинамике, что делает автомобиль стабильным и управляемым даже при высоких скоростях.\r\n\r\nИнтерьер Audi R8 отражает роскошь и инновационность. Качественные материалы, современный дизайн и внимание к деталям создают уникальную атмосферу комфорта и элегантности. Интуитивно понятная панель приборов, передовые системы информационно-развлекательного комплекса и эргономичные элементы управления позволяют водителю полностью наслаждаться вождением.\r\n\r\nAudi R8 оснащен мощным и эффективным двигателем, который обеспечивает поразительную производительность и динамичные характеристики. Звук двигателя R8 создает неповторимую атмосферу, наполняя салон спортивной энергией. Технологии передачи мощности, такие как полный привод quattro, обеспечивают уверенное сцепление с дорогой и высокую маневренность.", DateCreate = new DateTime(2021, 7, 2), EditTime = new DateTime(2021, 7, 2), IsPublic = true, Avatar = imageBytes_4 };
            var Post5 = new Post { Id = 5, UserId = User3.Id, CarId = Car5.Id, Name = "Дота или колёса?", Description = "Toyota Camry - это седан среднего размера, который представляет собой сочетание стиля, комфорта и надежности. Он отличается элегантным внешним дизайном, привлекательной передней решеткой и плавными линиями. Camry обладает просторным и удобным салоном, предлагая комфортные сиденья и высокое качество отделки.\r\n\r\nПри вождении Camry вы будете наслаждаться плавностью хода и отзывчивостью автомобиля. Он оснащен эффективными двигателями, которые обеспечивают достаточную мощность для комфортного передвижения как в городе, так и на трассе. Технологии безопасности, такие как системы предупреждения столкновений и поддержания полосы движения, обеспечивают дополнительный уровень защиты во время поездки.\r\n\r\n", DateCreate = new DateTime(2022, 7, 2), EditTime = new DateTime(2022, 7, 2), IsPublic = true, Avatar = imageBytes_5 };
            modelBuilder.Entity<Post>().HasData(Post1, Post2, Post3, Post4, Post5);


        }

    }
}
