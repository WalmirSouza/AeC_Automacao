using Upd8.Domains;
using Newtonsoft.Json;
using RestSharp;

namespace Upd8.Services
{
    public class ApiIbgeService : IApiIbgeService
    {
        private readonly RestClient _restCliente;

        public ApiIbgeService(RestClient restCliente)
        {
            _restCliente = restCliente;
        }

        public IEnumerable<Municipios> GetAll()
        {
            var restRequest = new RestRequest("localidades/municipios", Method.Get);
            var restResponse = _restCliente.Execute(restRequest);
            if (restResponse.IsSuccessStatusCode)
            {
                var jsonString = restResponse.Content;
                return JsonConvert.DeserializeObject<List<Municipios>>(jsonString);
            }

            return new List<Municipios>();
        }
    }
}