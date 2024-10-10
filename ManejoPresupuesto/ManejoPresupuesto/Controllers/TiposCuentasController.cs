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

            return View(tipoCuenta);

        }
    }
}
