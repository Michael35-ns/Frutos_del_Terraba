using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Services;

[Route("api/[controller]")]
[ApiController]
public class EmpleadosController : ControllerBase
{
    private readonly EmpleadosService _empleadosService;

    public EmpleadosController(EmpleadosService empleadosService)
    {
        _empleadosService = empleadosService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmpleadoDTO>>> GetEmpleados()
    {
        return Ok(await _empleadosService.GetEmpleadosAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmpleadoDTO>> GetEmpleado(int id)
    {
        var empleado = await _empleadosService.GetEmpleadoByIdAsync(id);
        if (empleado == null) return NotFound();
        return Ok(empleado);
    }

    [HttpPost]
    public async Task<ActionResult> CreateEmpleado([FromBody] EmpleadoDTO empleado)
    {
        await _empleadosService.CreateEmpleadoAsync(empleado);
        return CreatedAtAction(nameof(GetEmpleado), new { id = empleado.Id }, empleado);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateEmpleado(int id, [FromBody] EmpleadoDTO empleado)
    {
        if (id != empleado.Id) return BadRequest();
        await _empleadosService.UpdateEmpleadoAsync(empleado);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEmpleado(int id)
    {
        await _empleadosService.DeleteEmpleadoAsync(id);
        return NoContent();
    }
}
