using Upd8.Datas.Data;
using Upd8.Domains;

namespace Upd8.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>
    {
        public ClienteRepository(Upd8APIContext context) : base(context)
        {
        }
    }
}
