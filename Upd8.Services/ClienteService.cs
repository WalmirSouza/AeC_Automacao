using Upd8.Domains;
using Upd8.Repositories.Interfaces;
using Upd8.Services.Interfaces;

namespace Upd8.Services
{
    public class ClienteService : ServiceBase<Cliente>, IClienteService
    {
        public ClienteService(IRepository<Cliente> repository) : base(repository)
        {
        }
    }
}
