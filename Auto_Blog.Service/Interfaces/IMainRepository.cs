namespace Auto_Blog.Service.Interfaces
{ 
  public interface IMainRepository<T>
    {
        Task Create(T entity);

        IQueryable<T> GetAll();

        Task<T> GetOne(int Id);

        Task<T> GetOneByName(string name);

        Task Delete(T entity);

        Task<T> Update(T entity);
    }
}
