using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Frutos_del_Terraba_Api.Controllers
{

    [Route("api/categoria")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound(new { message = "Categoría no encontrada" });
            }
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoriaDTOModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoria = new Categoria
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion
            };

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = categoria.Id_categoria }, categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoriaDTOModel model)
        {
            if (id != model.Id_categoria)
            {
                return BadRequest(new { message = "El ID de la categoría no coincide" });
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound(new { message = "Categoría no encontrada" });
            }
            categoria.Nombre = model.Nombre;
            categoria.Descripcion = model.Descripcion;

            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound(new { message = "Categoría no encontrada" });
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}