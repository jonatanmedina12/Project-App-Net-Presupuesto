using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController: Controller
        
    {
        private readonly RepositorioTiposCuentas repositorioTiposCuentas1;
        private readonly IservicioUsuarios iservicioUsuarios;

        public TiposCuentasController(RepositorioTiposCuentas repositorioTiposCuentas,IservicioUsuarios iservicioUsuarios)
        {
            repositorioTiposCuentas1 = repositorioTiposCuentas;
            this.iservicioUsuarios = iservicioUsuarios;
        }

        public IActionResult crear()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            var usuarioId = iservicioUsuarios.ObtenerUsuariosId();
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
            var usuarioId = iservicioUsuarios.ObtenerUsuariosId();
            var yaExisteTipoCuenta=await repositorioTiposCuentas1.Existe(nombre, usuarioId);

            if (yaExisteTipoCuenta)
            {
                return Json($"El nombre {nombre} ya existe");
            }
            return Json(true);

        }
    }
}
