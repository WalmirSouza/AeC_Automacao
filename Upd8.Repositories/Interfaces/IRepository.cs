namespace Upd8.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Save(T obj);
        Task Update(T obj);
        Task Delete(T obj);

    }
}
