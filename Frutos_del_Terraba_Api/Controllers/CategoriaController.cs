using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Models;
using Frutos_del_Terraba_Api.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Frutos_del_Terraba_Api.Controllers
{

    [Route("api/categoria")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        #region Obtener todas las Categorias
        [HttpGet]
        public async Task<IActionResult> ObtenerTodasCategorias()
        {
            var categorias = await _categoriaService.ObtenerTodasCategorias();
            return Ok(categorias);
        }

        #endregion

        #region Obtener Categoria por Id
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCategoriaId(int id)
        {
            var categoria = await _categoriaService.ObtenerCategoriaPorId(id);
            if (categoria == null)
                return NotFound(new { message = "Categoría no encontrada" });

            return Ok(categoria);
        }

        #endregion


        #region Crear una nueva Categoria
        [HttpPost]
        public async Task<IActionResult> CrearCategoria([FromBody] CategoriaDTOModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoria = await _categoriaService.CrearCategoria(model);
            return CreatedAtAction(nameof(ObtenerCategoriaId), new { id = categoria.Id_categoria }, categoria);
        }
        #endregion

        #region Actualizar Categoria
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCategoria(int id, [FromBody] CategoriaDTOModel model)
        {
            if (id != model.Id_categoria)
                return BadRequest(new { message = "El ID de la categoría no coincide" });

            var categoria = await _categoriaService.ObtenerCategoriaPorId(id);
            if (categoria == null)
                return NotFound(new { message = "Categoría no encontrada" });

            var actualizado = await _categoriaService.ActualizarCategoria(id, model);
            if (!actualizado)
                return StatusCode(500, new { message = "Error al actualizar la categoría" });
            return Ok(categoria);
        }

        #endregion

        #region Eliminar Categoria
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            var eliminado = await _categoriaService.EliminarCategoria(id);
            if (!eliminado)
                return NotFound(new { message = "Categoría no encontrada" });

            return NoContent();
        }

        #endregion
    }
}
