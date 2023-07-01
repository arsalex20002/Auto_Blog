namespace Auto_Blog.Domain.Entity
{
    public class CarType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
