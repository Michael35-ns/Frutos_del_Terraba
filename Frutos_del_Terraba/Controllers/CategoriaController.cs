using Frutos_del_Terraba.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Frutos_del_Terraba.Helpers.Interfaces;
using Frutos_del_Terraba_Api.DTO;

namespace Frutos_del_Terraba.Controllers
{
    public class CategoriaController : Controller

    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        #region Visualizar todas las Categorías
        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaService.ObtenerTodasCategoriasAsync();
            var viewModel = new CategoriaViewModel
            {
                Categorias = categorias.ToList(),
                NuevaCategoria = new Categoria()
            };
            return View(viewModel);
        }
        #endregion

        #region Crear una nueva Categoría
        [HttpPost]
        public async Task<IActionResult> Crear(CategoriaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categorias = (await _categoriaService.ObtenerTodasCategoriasAsync()).ToList();
                return View("Index", model);
            }

            var nuevaCategoria = await _categoriaService.CrearCategoriaAsync(new Categoria
            {
                Nombre = model.NuevaCategoria.Nombre,
                Descripcion = model.NuevaCategoria.Descripcion
            });

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Eliminar una Categoría
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _categoriaService.EliminarCategoriaAsync(id);
            if (eliminado)
            {
                return RedirectToAction(nameof(Index));
            }

            return View("Error", new { RequestId = $"Error al eliminar la categoría con ID {id}." });
        }
        #endregion

        #region Editar Categoría
        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var categoria = await _categoriaService.ObtenerCategoriaPorIdAsync(id.Value);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            var actualizado = await _categoriaService.ActualizarCategoriaAsync(id, categoria);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}