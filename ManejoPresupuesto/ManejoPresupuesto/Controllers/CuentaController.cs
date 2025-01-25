using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupuesto.Controllers
{

    public class CuentaController : Controller
    {
        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;
        private readonly IservicioUsuarios iservicioUsuarios;
        private readonly IrepositorioCuentas irepositorioCuentas;
        private readonly IrepositorioCuentas repositorioCuentas;

        public CuentaController(IRepositorioTiposCuentas repositorioTiposCuentas, IservicioUsuarios iservicioUsuarios, IrepositorioCuentas irepositorioCuentas,IrepositorioCuentas repositorioCuentas) {
            this.repositorioTiposCuentas = repositorioTiposCuentas;
            this.iservicioUsuarios = iservicioUsuarios;
            this.irepositorioCuentas = irepositorioCuentas;
            this.repositorioCuentas = repositorioCuentas;
        }
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var usuarioId = iservicioUsuarios.ObtenerUsuariosId();
            var tiposCuentas = await repositorioTiposCuentas.Obtener("", usuarioId);
            var modelo = new CuentaCreacionViewModel();
            modelo.TiposCuentas = tiposCuentas.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(x.Nombre, x.Id.ToString()));
            return View(modelo);

        }
        [HttpPost]
        public async Task<IActionResult> Crear(CuentaCreacionViewModel cuenta)
        {
            var usuarioId =  iservicioUsuarios.ObtenerUsuariosId();
            var tiposCuentas = await repositorioTiposCuentas.Obtener("", usuarioId);
            if(tiposCuentas is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            if (!ModelState.IsValid)
            {
                cuenta.TiposCuentas = await ObtenerTiposCuentas(usuarioId);
                return View(cuenta);
            }
            await irepositorioCuentas.crear(cuenta);
            return View(cuenta);
        }
        private async Task<IEnumerable<SelectListItem>>ObtenerTiposCuentas(int usuarioId)
        {
            var tiposCuentas = await repositorioTiposCuentas.Obtener("", usuarioId);

            return tiposCuentas.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(x.Nombre, x.Id.ToString())); ;
        }

        [HttpPost]
        public async Task<IActionResult> crear(CuentaCreacionViewModel cuenta)
        {
            var usuarioId = iservicioUsuarios.ObtenerUsuariosId();
            var tipocuenta = await repositorioTiposCuentas.Obtener(cuenta.TipoCUentaId, usuarioId);

            if (tipocuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            if (!ModelState.IsValid)
            {

            }
        
               


        }




        
        public async Task<IEnumerable<SelectListItem>>ObtenerTiposCuentas2(int usuarioId)
        {

            var tiposCuentas = await repositorioTiposCuentas.Obtener("", usuarioId);

            return tiposCuentas.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(x.Nombre, x.Id.ToString())); ;
        }


    }
}
