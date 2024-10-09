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
        public IActionResult Crear(TipoCuenta tipoCuenta)
        {
            if (!ModelState.IsValid)
            {
                return View(tipoCuenta);
            }
            repositorioTiposCuentas1.crear(tipoCuenta);

            return View(tipoCuenta);

        }
    }
}
