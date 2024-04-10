using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Modelos.ViewModels;
using SistemaInventario.Utilidades;
using System.Security.Claims;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrdenController : Controller
    {
        private readonly IUnidadTrabajo _uniidadTrabajo;
        [BindProperty]
        public OrdenDetalleVM ordenDetalleVM { get; set; }

        public OrdenController(IUnidadTrabajo uniidadTrabajo)
        {
            _uniidadTrabajo = uniidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detalle(int id)
        {
            ordenDetalleVM = new OrdenDetalleVM()
            {
                Orden = await _uniidadTrabajo.Orden.ObtenerPrimero(o => o.Id == id, incluirPropiedades: "UsuarioAplicacion"),
                OrdenDetalleLista = await _uniidadTrabajo.OrdenDetalle.ObtenerTodos(d => d.OrdenId == id, incluirPropiedades: "Producto")
            };
            return View(ordenDetalleVM);
        }

        [Authorize(Roles =DS.Role_Admin)]
        public async Task<IActionResult> Procesar(int id)
        {
            var orden = await _uniidadTrabajo.Orden.ObtenerPrimero(o => o.Id == id);
            orden.EstadoOrden = DS.EstadoEnProceso;
            await _uniidadTrabajo.Guardar();
            TempData[DS.Exitosa] = "Orden cambiada a Estado en Proceso";
            return RedirectToAction("Detalle" , new { id = id});
        }

        [HttpPost]
        [Authorize(Roles = DS.Role_Admin)]
        public async Task<IActionResult> EnviarOrden(OrdenDetalleVM ordenDetalleVM)
        {
            var orden = await _uniidadTrabajo.Orden.ObtenerPrimero(o => o.Id == ordenDetalleVM.Orden.Id);
            orden.EstadoOrden = DS.EstadoEnviado;
            orden.Carrier = ordenDetalleVM.Orden.Carrier;
            orden.NumeroEnvio = ordenDetalleVM.Orden.NumeroEnvio;
            orden.FechaEnvio = DateTime.Now;
            await _uniidadTrabajo.Guardar();
            TempData[DS.Exitosa] = "Orden cambiada a Estado Enviado";
            return RedirectToAction("Detalle", new { id = ordenDetalleVM.Orden.Id });
        }

        #region
        [HttpGet]
        public async Task<IActionResult> ObtenerOrdenLista(string estado)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            IEnumerable<Orden> todos;
            if (User.IsInRole(DS.Role_Admin)) // Validar el Rol del usuario
            {
                todos = await _uniidadTrabajo.Orden.ObtenerTodos(incluirPropiedades: "UsuarioAplicacion");
            }
            else
            {
                todos = await _uniidadTrabajo.Orden.ObtenerTodos(o => o.UsuarioAplicacionId == claim.Value, incluirPropiedades: "UsuarioAplicacion");
            }
            // Validar el Estado
            switch (estado)
            {
                case "aprobado":
                    todos = todos.Where(o => o.EstadoOrden == DS.EstadoAprobado); 
                    break;
                case "completado":
                    todos = todos.Where(o => o.EstadoOrden == DS.EstadoEnviado); 
                    break;
                default:
                    break;
            }


            return Json(new { data = todos });
        }

        #endregion

    }
}
