﻿using Microsoft.AspNetCore.Mvc;
using SistemaInventario.Utilidades;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarcaController : Controller
    {

        private readonly IUnidadTrabajo _unidadTrabajo;

        public MarcaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Marca marca = new Marca();

            if (id== null)
            {
                // Crear una nueva Categoria
                marca.Estado = true;
                return View(marca);
            }
            // Actualizamos Categoria
            marca = await _unidadTrabajo.Marca.Obtener(id.GetValueOrDefault());
            if(marca ==null)
            {
                return NotFound();
            }
            return View(marca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Upsert(Marca marca)
        {
            if(ModelState.IsValid)
            {
                if(marca.Id == 0)
                {
                    await _unidadTrabajo.Marca.Agregar(marca);
                    TempData[DS.Exitosa] = "Marca creada Exitosamente";
                }
                else
                {
                    _unidadTrabajo.Marca.Actualizar(marca);
                    TempData[DS.Exitosa] = "Marca Actualizada Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar Marca";
            return View(marca);
        }

        #region API Data Table para importar las tablas de trabajo

        [HttpGet]

        public async Task<IActionResult> ObtenerTodos() 
        {
            var todos = await _unidadTrabajo.Marca.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var marcaDB = await _unidadTrabajo.Marca.Obtener(id);
            if (marcaDB == null)
            {
                return Json(new { success = false, message = "Error al borrar Marca" });
            }
            _unidadTrabajo.Marca.Remover(marcaDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Marca borrada exitosamente" });
        }

        [ActionName("ValidarNombre")]

        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Marca.ObtenerTodos();
            if(id==0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id !=id);
            }
            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });
        }

        #endregion

    }
}
