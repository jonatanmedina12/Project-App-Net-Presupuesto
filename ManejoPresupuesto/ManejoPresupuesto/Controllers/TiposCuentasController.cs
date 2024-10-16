using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController: Controller
        
    {
        private readonly RepositorioTiposCuentas repositorioTiposCuentas1;
        public TiposCuentasController(RepositorioTiposCuentas repositorioTiposCuentas)
        {
            repositorioTiposCuentas1 = repositorioTiposCuentas;
        }

        public IActionResult crear()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            var usuarioId = 1;
            var nombre = "";
            var tiposCuentas = await repositorioTiposCuentas1.Obtener(nombre,usuarioId);
            return View(tiposCuentas);

        }

        [HttpPost]
        public async Task<IActionResult> Crear(TipoCuenta tipoCuenta)
        {
            if (!ModelState.IsValid)
            {
                return View(tipoCuenta);
            }
            var yaExisteTipoCuenta = await repositorioTiposCuentas1.Existe(tipoCuenta.Nombre, tipoCuenta.UsuarioId);

            if (yaExisteTipoCuenta)
            {
                ModelState.AddModelError(nameof(tipoCuenta.Nombre), $"El nombre {tipoCuenta.Nombre} ya existe.");
                return View(tipoCuenta);
            }
             await repositorioTiposCuentas1.crear(tipoCuenta);

            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult>VerificarExisteTipoCuenta(string nombre)
        {
            var usuarioId = 1;
            var yaExisteTipoCuenta=await repositorioTiposCuentas1.Existe(nombre, usuarioId);

            if (yaExisteTipoCuenta)
            {
                return Json($"El nombre {nombre} ya existe");
            }
            return Json(true);

        }
    }
}
