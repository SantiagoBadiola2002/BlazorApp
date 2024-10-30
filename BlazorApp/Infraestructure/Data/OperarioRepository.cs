using BlazorApp.Models;
using BlazorApp.Models.Interfaces;
using BlazorApp.Models.BaseDeDatos;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Models.DTs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Infraestructure.Data
{
    public class OperarioRepository : IOperarioRepository
    {
        private readonly ContextoBD _context;

        public OperarioRepository(ContextoBD context)
        {
            _context = context;
        }

        public async Task<DTOperario> GetOperarioByIdAsync(int id)
        {
            try
            {
                var operario = await _context.Operarios.FirstOrDefaultAsync(o => o.Id == id);
                return operario != null ? DTsMapped.ConvertirAOperarioDTO(operario) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el operario por Id: {ex.Message}");
                return null;
            }
        }

        public async Task<IList<DTOperario>> GetAllOperariosAsync()
        {
            try
            {
                var operarios = await _context.Operarios.ToListAsync();
                return operarios.Select(o => DTsMapped.ConvertirAOperarioDTO(o)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los operarios: {ex.Message}");
                return new List<DTOperario>();
            }
        }

        public async Task AddOperarioAsync(DTOperario operarioDTO)
        {
            try
            {
                var operario = new Operario
                {
                    Id = operarioDTO.Id,
                    Nombre = operarioDTO.Nombre,
                    EstaDisponible = operarioDTO.EstaDisponible,
                    OficinaId = operarioDTO.OficinaId,
                    Contraseña = operarioDTO.Contraseña // Si este campo está incluido en el DTO
                };

                await _context.Operarios.AddAsync(operario);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el operario: {ex.Message}");
            }
        }

        public async Task UpdateOperarioAsync(DTOperario operarioDTO)
        {
            try
            {
                var existingOperario = await _context.Operarios.FirstOrDefaultAsync(o => o.Id == operarioDTO.Id);
                if (existingOperario != null)
                {
                    existingOperario.Nombre = operarioDTO.Nombre;
                    existingOperario.EstaDisponible = operarioDTO.EstaDisponible;
                    existingOperario.OficinaId = operarioDTO.OficinaId;
                    existingOperario.Contraseña = operarioDTO.Contraseña; // Actualizamos la contraseña si está en el DTO

                    _context.Operarios.Update(existingOperario);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el operario: {ex.Message}");
            }
        }

        public async Task DeleteOperarioAsync(int id)
        {
            try
            {
                var operario = await _context.Operarios.FirstOrDefaultAsync(o => o.Id == id);
                if (operario != null)
                {
                    _context.Operarios.Remove(operario);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el operario: {ex.Message}");
            }
        }

        public async Task<DTOperario> VerificarCredencialesAsync(string nombre, string contraseña)
        {
            try
            {
                var operario = await _context.Operarios
                    .FirstOrDefaultAsync(o => o.Nombre == nombre && o.Contraseña == contraseña);

                return operario != null ? DTsMapped.ConvertirAOperarioDTO(operario) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar las credenciales del operario: {ex.Message}");
                return null;
            }
        }

        public async Task<List<DTOperario>> ObtenerOperariosPorOficinaIdAsync(int oficinaId)
        {
            try
            {
                var operarios = await _context.Operarios
                    .Where(o => o.OficinaId == oficinaId)
                    .ToListAsync();

                return operarios.Select(o => DTsMapped.ConvertirAOperarioDTO(o)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener operarios por oficina Id: {ex.Message}");
                return new List<DTOperario>();
            }
        }

        public async Task<DTOperario> VerificarCredencialesAsync(int idOficina, string nombre, string contraseña, Rol rol)
        {
            try
            {
                // Filtra por OficinaId, Nombre, Contraseña y Rol
                var operario = await _context.Operarios
                    .FirstOrDefaultAsync(o => o.OficinaId == idOficina
                                              && o.Nombre == nombre
                                              && o.Contraseña == contraseña
                                              && o.RolOperario == rol);

                // Si se encuentra el operario, conviértelo a DTO y retorna, de lo contrario retorna null
                return operario != null ? DTsMapped.ConvertirAOperarioDTO(operario) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar las credenciales del operario: {ex.Message}");
                return null;
            }
        }



    }
}
