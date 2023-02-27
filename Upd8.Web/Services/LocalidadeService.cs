using Upd8.Web.Models;

namespace Upd8.Web.Services
{
    public class LocalidadeService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public LocalidadeService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("BaseUrl"));
        }

        public async Task<IEnumerable<string>> BuscarUfs()
        {
            var Ufs = await _httpClient.GetFromJsonAsync<IEnumerable<Uf>>("Uf");

            return Ufs.Select(x => x.Sigla);
        }

        public async Task<IEnumerable<string>> BuscarMunicipiosPorUf(string uf)
        {
            var municipios = await _httpClient.GetFromJsonAsync<IEnumerable<Municipios>>($"Municipio/{uf}");

            return municipios.Select(x => x.Nome);
        }

    }
}
