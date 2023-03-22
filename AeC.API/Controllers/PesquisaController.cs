using AeC.Domains;
using AeC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AeC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PesquisaController : ControllerBase
    {
        private readonly IPesquisaService _pesquisaService;
        public PesquisaController(IPesquisaService pesquisaService)
        {
            _pesquisaService = pesquisaService;
        }

        [HttpGet("{termo}")]
        public ActionResult<IEnumerable<Pesquisa>> BuscaPorTermo(string termo)
        {
            var pesquisas = _pesquisaService.BuscaPorTermo(termo);

            return Ok(pesquisas);
        }
    }
}
