using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Drawing;
using Upd8.Web.Models;
using Upd8.Web.Services;

namespace Upd8.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClienteService _clienteService;

        private readonly LocalidadeService _localidadeService;

        public HomeController(ClienteService clienteService, LocalidadeService localidadeService)
        {
            _clienteService = clienteService;
            _localidadeService = localidadeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            AtualizaCombos();

            return View();
        }

        private void AtualizaCombos(string uf = "")
        {
            var siglasUf = _localidadeService.BuscarUfs().Result;
            ViewBag.ListaEstado = siglasUf.Select(x => new SelectListItem
            {
                Value = x,
                Text = x
            }).ToList();

            if (string.IsNullOrWhiteSpace(uf))
            {
                uf = siglasUf.First();
            }

            var municipiosPorUf = _localidadeService.BuscarMunicipiosPorUf(uf).Result;

            ViewBag.ListaMunicipio = municipiosPorUf.Select(x => new SelectListItem
            {
                Value = x,
                Text = x
            }).ToList();
        }

        [HttpGet]
        public IActionResult BuscarLocalidade(string estado)
        {
            var municipiosPorUf = _localidadeService.BuscarMunicipiosPorUf(estado).Result;

            ViewBag.ListaMunicipio = municipiosPorUf.Select(x => new SelectListItem
            {
                Value = x,
                Text = x
            }).ToList();

            return Json(ViewBag.ListaMunicipio);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var clienteEncontrado = await _clienteService.ObterPorId(id);
            return View(clienteEncontrado);
        }
        public async Task<IActionResult> Details(int id)
        {
            var clienteEncontrado = await _clienteService.ObterPorId(id);
            return View(clienteEncontrado);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var clienteEncontrado = await _clienteService.ObterPorId(id);

            AtualizaCombos(clienteEncontrado.Estado);

            return View(clienteEncontrado);
        }

        public async Task<IActionResult> List()
        {
            var listaClientes = await _clienteService.ObterTodos();

            return View(listaClientes);
        }

        [HttpPost]
        public IActionResult Create(ClienteViewModel clienteViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _clienteService.Cadastrar(clienteViewModel);

                return RedirectToAction("List");

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _clienteService.ObterTodos();

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _clienteService.ObterPorId(id);

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClienteViewModel clienteViewModel)
        {
            try
            {
                if (clienteViewModel.Id <= 0)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest();

                _clienteService.Alterar(clienteViewModel);

                return RedirectToAction("List");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, ClienteViewModel clienteViewModel)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();

                _clienteService.Delete(id);

                return RedirectToAction("List");

            }
            catch (Exception)
            {

                throw;
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}