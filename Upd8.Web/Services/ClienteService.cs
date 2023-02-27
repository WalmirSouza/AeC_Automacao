using Microsoft.AspNetCore.Mvc;
using Upd8.Web.Models;

namespace Upd8.Web.Services
{
    public class ClienteService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ClienteService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("BaseUrl"));
        }


        public async Task  Cadastrar(ClienteViewModel clienteViewModel)
        {
            await _httpClient.PostAsJsonAsync("Cliente", clienteViewModel);

        }

        public async Task<IEnumerable<ClienteViewModel>> ObterTodos()
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<ClienteViewModel>>("Cliente");

            return result;
        }

        public async Task Alterar(ClienteViewModel clienteViewModel)
        {
            await _httpClient.PutAsJsonAsync<ClienteViewModel>($"Cliente/{clienteViewModel.Id}", clienteViewModel);
        }

        public async Task<ClienteViewModel> ObterPorId(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ClienteViewModel>($"Cliente/{id}");

            return result;
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"Cliente/{id}");
        }

    }
}
