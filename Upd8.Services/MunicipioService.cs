using Upd8.Domains;
using Upd8.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using System.Diagnostics.CodeAnalysis;

namespace Upd8.Services
{
    public class MunicipioService : IMunicipioService
    {
        private const string _cacheKey = "IEnumerable<Municipios>";
        private readonly TimeSpan CacheTimeOut = TimeSpan.FromHours(2);
        private readonly IMemoryCache _memoryCache;
        private readonly IApiIbgeService _apiIbgeService;

        public MunicipioService(IApiIbgeService apiIbgeService, IMemoryCache memoryCache)
        {
            _apiIbgeService = apiIbgeService;
            _memoryCache = memoryCache;
        }

        private IEnumerable<Municipios> GetFromCache()
        {
            var municipios = _memoryCache.Get<IEnumerable<Municipios>>(_cacheKey);
            if (municipios == null)
            {
                municipios = _apiIbgeService.GetAll();
                _memoryCache.Set(_cacheKey, municipios, CacheTimeOut);
            }
            return municipios;
        }

        public IEnumerable<Municipios> GetAll() => GetFromCache();

        public IEnumerable<Municipios> GetByUf(string uf)
        {
            var municipios = GetAll();

            return municipios.Where(municipio => municipio.Microrregiao.Mesorregiao.Uf.Sigla == uf).OrderBy( x=> x.Microrregiao.Mesorregiao.Uf.Sigla);
        }

        public IEnumerable<Uf> GetUfs()
        {
            var municipios = GetAll();
            var uf1 = new Uf();
            var uf2 = new Uf();


            var c1 = UfComparer.Instance.GetHashCode(uf1);
            var c2 = UfComparer.Instance.GetHashCode(uf2);

            var igual = (c1 == c2) && UfComparer.Instance.Equals(uf1, uf2);


            return municipios.GroupBy(m => m.Microrregiao.Mesorregiao.Uf, UfComparer.Instance).Select(g => g.Key);
        }
    }

    public class UfComparer : IEqualityComparer<Uf>
    {
        public static readonly UfComparer Instance = new UfComparer();
        private UfComparer() { }
        public bool Equals(Uf x, Uf y) => object.Equals(x.Id, y.Id);

        public int GetHashCode([DisallowNull] Uf obj) => obj.Id;
    }
}