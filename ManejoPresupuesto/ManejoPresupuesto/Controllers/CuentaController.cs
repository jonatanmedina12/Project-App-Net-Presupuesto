using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManejoPresupuesto.Controllers
{

    public class CuentaController : Controller
    {
        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;
        private readonly IservicioUsuarios iservicioUsuarios;

        public CuentaController(IRepositorioTiposCuentas repositorioTiposCuentas,IservicioUsuarios iservicioUsuarios) {
            this.repositorioTiposCuentas = repositorioTiposCuentas;
            this.iservicioUsuarios = iservicioUsuarios;
        }
        [HttpGet]
        public async Task< IActionResult> Crear()
        {
            var usuarioId = iservicioUsuarios.ObtenerUsuariosId();
            var tiposCuentas = await repositorioTiposCuentas.Obtener("",usuarioId);
            var modelo = new CuentaCreacionViewModel();
            modelo.TiposCuentas = tiposCuentas.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(x.Nombre, x.Id.ToString()));
            return View(modelo);

        }
    }
}
