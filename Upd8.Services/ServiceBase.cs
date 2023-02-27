using Upd8.Repositories.Interfaces;
using Upd8.Services.Interfaces;

namespace Upd8.Services
{
    public class ServiceBase<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;
        public ServiceBase(IRepository<T> repository)
        {
            _repository = repository;
        }
        public async Task Delete(T obj)
        {
            if (obj == null)
                return;
            
            await _repository.Delete(obj);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<T> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Save(T obj)
        {
            await _repository.Save(obj);
        }

        public async Task Update(T obj)
        {
            await _repository.Update(obj);
        }
    }
}
