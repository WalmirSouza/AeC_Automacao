using AeC.Datas.Data;
using AeC.Domains;

namespace AeC.Repositories
{
    public class PesquisaRepository : RepositoryBase<Pesquisa>
    {
        public PesquisaRepository(AeCAPIContext context) : base(context)
        {
        }
    }
}
