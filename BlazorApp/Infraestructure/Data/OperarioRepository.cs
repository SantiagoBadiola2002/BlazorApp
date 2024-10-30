using BlazorApp.Models;
using BlazorApp.Models.Interfaces;
using BlazorApp.Models.BaseDeDatos;
using BlazorApp.Models.DTs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Infraestructure.Data
{
    public class OperarioRepository : IOperarioRepository
    {
        private readonly ContextoBD _context;

        public OperarioRepository(ContextoBD context)
        {
            _context = context;
        }

        public DTOperario GetOperarioById(int id)
        {
            try
            {
                var operario = _context.Operarios.FirstOrDefault(o => o.Id == id);
                return operario != null ? DTsMapped.ConvertirAOperarioDTO(operario) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el operario por Id: {ex.Message}");
                return null;
            }
        }

        public IList<DTOperario> GetAllOperarios()
        {
            try
            {
                var operarios = _context.Operarios.ToList();
                return operarios.Select(o => DTsMapped.ConvertirAOperarioDTO(o)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los operarios: {ex.Message}");
                return new List<DTOperario>();
            }
        }

        public void AddOperario(DTOperario operarioDTO)
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

                _context.Operarios.Add(operario);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el operario: {ex.Message}");
            }
        }

        public void UpdateOperario(DTOperario operarioDTO)
        {
            try
            {
                var existingOperario = _context.Operarios.FirstOrDefault(o => o.Id == operarioDTO.Id);
                if (existingOperario != null)
                {
                    existingOperario.Nombre = operarioDTO.Nombre;
                    existingOperario.EstaDisponible = operarioDTO.EstaDisponible;
                    existingOperario.OficinaId = operarioDTO.OficinaId;
                    existingOperario.Contraseña = operarioDTO.Contraseña; // Actualizamos la contraseña si está en el DTO

                    _context.Operarios.Update(existingOperario);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el operario: {ex.Message}");
            }
        }

        public void DeleteOperario(int id)
        {
            try
            {
                var operario = _context.Operarios.FirstOrDefault(o => o.Id == id);
                if (operario != null)
                {
                    _context.Operarios.Remove(operario);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el operario: {ex.Message}");
            }
        }

        public DTOperario VerificarCredenciales(string nombre, string contraseña)
        {
            try
            {
                var operario = _context.Operarios
                    .FirstOrDefault(o => o.Nombre == nombre && o.Contraseña == contraseña);

                return operario != null ? DTsMapped.ConvertirAOperarioDTO(operario) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar las credenciales del operario: {ex.Message}");
                return null;
            }
        }

        public List<DTOperario> ObtenerOperariosPorOficinaId(int oficinaId)
        {
            try
            {
                var operarios = _context.Operarios
                    .Where(o => o.OficinaId == oficinaId)
                    .ToList();

                return operarios.Select(o => DTsMapped.ConvertirAOperarioDTO(o)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener operarios por oficina Id: {ex.Message}");
                return new List<DTOperario>();
            }
        }

        public DTOperario VerificarCredenciales(int idOficina, string nombre, string contraseña, Rol rol)
        {
            try
            {
                var operario = _context.Operarios
                    .FirstOrDefault(o => o.OficinaId == idOficina
                                          && o.Nombre == nombre
                                          && o.Contraseña == contraseña
                                          && o.RolOperario == rol);

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
