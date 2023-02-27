using Upd8.Domains;

namespace Upd8.Services
{
    public interface IApiIbgeService
    {
        IEnumerable<Municipios> GetAll();
    }
}