using AeC.Domains;

namespace AeC.Services.Interfaces
{
    public interface IPesquisaService : IService<Pesquisa>
    {
        IEnumerable<Pesquisa> BuscaPorTermo(string termo);
    }
}
