namespace Auto_Blog.Domain.Entity
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CarTypeId { get; set; }
        public CarType  CarType { get; set; }

        public DateTime DateCreate { get; set; }

    }
}
