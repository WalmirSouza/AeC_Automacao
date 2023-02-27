using Upd8.Domains;
using Upd8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Upd8.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipioController : ControllerBase
    {
        private readonly IMunicipioService _municipioService;

        public MunicipioController(IMunicipioService municipioService)
        {
            this._municipioService = municipioService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Municipios>> GetAll()
        {
            return Ok(_municipioService.GetAll());
        }

        [HttpGet("{uf}")]
        public ActionResult<IEnumerable<Municipios>> GetByUf(string uf)
        {
            return Ok(_municipioService.GetByUf(uf));
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UfController : ControllerBase
    {
        private readonly IMunicipioService _municipioService;

        public UfController(IMunicipioService municipioService)
        {
            this._municipioService = municipioService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Uf>> GetAll()
        {
            return Ok(_municipioService.GetUfs());
        }
    }
}
