using Frutos_del_Terraba_Api.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frutos_del_Terraba_Api.Services
{
    public class EmpleadosService
    {
        private static List<EmpleadoDTO> empleados = new List<EmpleadoDTO>();

        public async Task<IEnumerable<EmpleadoDTO>> GetEmpleadosAsync()
        {
            return await Task.FromResult(empleados);
        }

        public async Task<EmpleadoDTO> GetEmpleadoByIdAsync(int id)
        {
            return await Task.FromResult(empleados.FirstOrDefault(e => e.Id == id));
        }

        public async Task CreateEmpleadoAsync(EmpleadoDTO empleado)
        {
            empleado.Id = empleados.Count + 1;
            empleados.Add(empleado);
            await Task.CompletedTask;
        }

        public async Task UpdateEmpleadoAsync(EmpleadoDTO empleado)
        {
            var existing = empleados.FirstOrDefault(e => e.Id == empleado.Id);
            if (existing != null)
            {
                existing.Email = empleado.Email;
                existing.UserName = empleado.UserName;
                existing.Role = empleado.Role;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteEmpleadoAsync(int id)
        {
            var empleado = empleados.FirstOrDefault(e => e.Id == id);
            if (empleado != null) empleados.Remove(empleado);
            await Task.CompletedTask;
        }
    }
}
