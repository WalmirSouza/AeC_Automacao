using Upd8.Domains;
using System.Collections.Generic;

namespace Upd8.Services.Interfaces
{
    public interface IMunicipioService 
    {
        IEnumerable<Municipios> GetAll();
        IEnumerable<Municipios> GetByUf(string uf);
        IEnumerable<Uf> GetUfs();
    }
}
