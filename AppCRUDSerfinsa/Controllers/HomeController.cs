using AppCRUDSerfinsa.Models;
using AppCRUDSerfinsa.Repositorios.Contrato;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

// Controlador principal que se comunica con el archivo Repositorios/Implementación/IGenericRepository

namespace AppCRUDSerfinsa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Empleado> _empleadoRepository;


        public HomeController(ILogger<HomeController> logger,
            IGenericRepository<Empleado> empleadoRepository)
        {
            _logger = logger;
            _empleadoRepository = empleadoRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Conexciones con los Métodos

        [HttpGet]
        public async Task<IActionResult> ListaEmpleados()
        {
            List<Empleado> _list = await _empleadoRepository.Lista();

            return StatusCode(StatusCodes.Status200OK, _list);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarEmpleados([FromBody] Empleado model)
        {
            bool _resultado = await _empleadoRepository.Guardar(model);

            if (_resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "Ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "Error" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditarEmpleados([FromBody] Empleado model)
        {
            bool _resultado = await _empleadoRepository.Editar(model);

            if (_resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "Ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "Error" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarEmpleados(int idEmpleado)
        {
            bool _resultado = await _empleadoRepository.Eliminar(idEmpleado);

            if (_resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "Ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "Error" });
            }
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}