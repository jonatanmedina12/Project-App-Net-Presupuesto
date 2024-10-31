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
        public async Task<IActionResult>Borrar (int id)
        {
            var usuarioID = iservicioUsuarios.ObtenerUsuariosId();
            var nombre = "";

            var tipocuenta = await repositorioTiposCuentas1.Obtener(nombre, id);

            if(tipocuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            return View(tipocuenta);

        }
        [HttpDelete]
        public async Task<IActionResult>BorrarTipoCuenta(int id)
        {
            var usuarioID = iservicioUsuarios.ObtenerUsuariosId();
            var nombre = "";

            var tipocuenta = await repositorioTiposCuentas1.Obtener(nombre, id);

            if (tipocuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            await repositorioTiposCuentas1.Borrar(id);
            return RedirectToAction("index");
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

        [HttpPost]
        public async Task <IActionResult> Ordenar([FromBody] int[] ids)
        {
            var usuarioId = iservicioUsuarios.ObtenerUsuariosId();
            var tiposCuentas = await repositorioTiposCuentas1.Obtener("",usuarioId);
            var idsTiposCuentas = tiposCuentas.Select(x => x.Id);
            var idsTiposCUentaNoPerteneceAlUsuario = ids.Except(idsTiposCuentas).ToList();
            if (idsTiposCUentaNoPerteneceAlUsuario.Count >0)
            {
                return Forbid();
            }
            var tiposCuentasOrdenes = ids.Select((valor, indice) => new TipoCuenta() { Id = valor, Orden = indice + 1 });
            await repositorioTiposCuentas1.Ordenar(tiposCuentasOrdenes);
            return Ok();
        }
    }
}
