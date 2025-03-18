using Azure;
using Frutos_del_Terraba.Helpers.Interfaces;
using Frutos_del_Terraba.Models;
using Frutos_del_Terraba_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Frutos_del_Terraba.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;
        private readonly ICategoriaService _categoriaService;
        public ProductoController(IProductoService productoService, ICategoriaService categoriaService)
        {
            _productoService = productoService;
            _categoriaService = categoriaService;
        }

        #region Obtener lista de productos
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var productos = await _productoService.ObtenerTodosProductosAsync();
            var categorias = await _categoriaService.ObtenerTodasCategoriasAsync();

            foreach (var producto in productos)
            {
                var categoria = categorias.FirstOrDefault(c => c.Id_categoria == producto.Id_categoria);
                producto.NombreCategoria = categoria?.Nombre ?? "Sin categoría"; 
            }
            return View(productos);
        }
        #endregion

        #region Crear un nuevo producto
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categorias = await _categoriaService.ObtenerTodasCategoriasAsync();

            if (categorias == null || !categorias.Any())
            {
                ViewBag.Categorias = new List<SelectListItem>(); 
            }
            else
            {
                ViewBag.Categorias = categorias.Select(c => new SelectListItem
                {
                    Value = c.Id_categoria.ToString(),
                    Text = c.Nombre
                }).ToList();
            }

            return View(new ProductoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categorias = await _categoriaService.ObtenerTodasCategoriasAsync();
                ViewBag.Categorias = categorias.Select(c => new SelectListItem
                {
                    Value = c.Id_categoria.ToString(),
                    Text = c.Nombre
                }).ToList();

                return View(model);
            }

            var categoriaSeleccionada = (await _categoriaService.ObtenerTodasCategoriasAsync())
                .FirstOrDefault(c => c.Id_categoria == model.Id_categoria);

            if (categoriaSeleccionada != null)
            {
                model.NombreCategoria = categoriaSeleccionada.Nombre;
            }

            await _productoService.CrearProductoAsync(model);
            TempData["SuccessMessage"] = "Producto creado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Eliminar un producto
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _productoService.EliminarProductoAsync(id);
            if (eliminado)
            {
                return Json(new { success = true });
            }
            
            return Json(new { success = false });
        }
        #endregion

        #region Editar Producto
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var producto = await _productoService.ObtenerProductoPorIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            var categorias = await _categoriaService.ObtenerTodasCategoriasAsync();
            if (categorias == null || !categorias.Any())
            {
                return View("Error", new ErrorViewModel { RequestId = "No se encontraron categorías." });
            }

            ViewBag.Categorias = categorias.Select(c => new SelectListItem
            {
                Value = c.Id_categoria.ToString(),
                Text = c.Nombre,
                Selected = c.Id_categoria == producto.Id_categoria
            }).ToList();

            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categorias = await _categoriaService.ObtenerTodasCategoriasAsync();
                ViewBag.Categorias = categorias.Select(c => new SelectListItem
                {
                    Value = c.Id_categoria.ToString(),
                    Text = c.Nombre
                }).ToList();

                return View(model);
            }

            var productoActualizado = await _productoService.ActualizarProductoAsync(model.IdProducto, model);

            if (productoActualizado == null)
            {
                ModelState.AddModelError("", "Hubo un error al actualizar el producto. Inténtelo de nuevo.");
                TempData["ErrorMessage"] = "Error al actualizar el producto. Inténtalo de nuevo.";

                var categorias = await _categoriaService.ObtenerTodasCategoriasAsync();
                ViewBag.Categorias = categorias.Select(c => new SelectListItem
                {
                    Value = c.Id_categoria.ToString(),
                    Text = c.Nombre
                }).ToList();

                return View(model);
            }
            
            TempData["SuccessMessage"] = "Producto actualizado exitosamente.";
            return RedirectToAction("Index");
        }

        #endregion

    }
}
