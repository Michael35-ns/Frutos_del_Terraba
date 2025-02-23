using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Frutos_del_Terraba_Api.Controllers
{
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;


        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        #region Obtener todos los productoos

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosProductos()
        {
            var productos = await _productoService.ObtenerTodosProductoAsync();
            return Ok(productos);
        }

        #endregion



        #region Obtener Producto por Id
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerProductoPorId(int id)
        {
            try
            {
                var producto = await _productoService.ObtenerProductoPorIdAsync(id);
                return Ok(producto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        #endregion


        #region Crear producto
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] ProductoDTOModel productoDto)
        {
            if (productoDto == null)
                return BadRequest(new { mensaje = "Los datos del producto no pueden estar vacios." });

            var productoCreado = await _productoService.CrearProductoAsync(productoDto);
            return CreatedAtAction(nameof(ObtenerProductoPorId), new { id = productoCreado.IdProducto }, productoCreado);
        }

        #endregion


        #region Actualizar un producto
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] ProductoDTOModel productoDto)
        {
            if (productoDto == null)
                return BadRequest(new { mensaje = "Los datos del producto no pueden estar vacios." });

            var resultado = await _productoService.ActualizarProductoAsync(id, productoDto);
            if (!resultado)
                return NotFound(new { mensaje = $"No se encontró el producto con ID {id} para actualizar." });

            return NoContent();
        }

        #endregion


        #region Eliminar un producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {

            if (id <= 0)
            {
                return BadRequest(new { mensaje = "ID de producto no válido." });
            }

            try
            {
                var resultado = await _productoService.EliminarProductoAsync(id);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se encontró el producto con ID {id} para eliminar." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al eliminar el producto.", error = ex.Message });
            }
        }

        #endregion
    }
}
