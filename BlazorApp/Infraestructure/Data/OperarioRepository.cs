using BlazorApp.Models;
using BlazorApp.Models.Interfaces;
using BlazorApp.Models.BaseDeDatos;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Infraestructure.Data
{
    public class OperarioRepository : IOperarioRepository
    {
        private readonly ContextoBD _context;

        // Constructor que inyecta el contexto de la base de datos
        public OperarioRepository(ContextoBD context)
        {
            _context = context;
        }

        // Obtener Operario por Id
        public Operario GetOperarioById(int id)
        {
            try
            {
                return _context.Operarios.FirstOrDefault(o => o.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el operario por Id: {ex.Message}");
                return null;
            }
        }

        // Obtener todos los operarios
        public IList<Operario> GetAllOperarios()
        {
            try
            {
                return _context.Operarios.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los operarios: {ex.Message}");
                return new List<Operario>();
            }
        }

        // Agregar nuevo operario
        public void AddOperario(Operario operario)
        {
            try
            {
                _context.Operarios.Add(operario);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el operario: {ex.Message}");
            }
        }

        // Actualizar un operario existente
        public void UpdateOperario(Operario operario)
        {
            try
            {
                var existingOperario = GetOperarioById(operario.Id);
                if (existingOperario != null)
                {
                    existingOperario.Nombre = operario.Nombre;
                    existingOperario.EstaDisponible = operario.EstaDisponible;
                    existingOperario.PuestoAsignado = operario.PuestoAsignado;
                    existingOperario.OficinaId = operario.OficinaId;
                    existingOperario.Contraseña = operario.Contraseña; // Actualiza también la contraseña
                    _context.Operarios.Update(existingOperario);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el operario: {ex.Message}");
            }
        }

        // Eliminar un operario por Id
        public void DeleteOperario(int id)
        {
            try
            {
                var operario = GetOperarioById(id);
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

        // Método adicional: Verificar la contraseña de un operario
        public Operario VerificarCredenciales(string nombre, string contraseña)
        {
            try
            {
                return _context.Operarios
                    .FirstOrDefault(o => o.Nombre == nombre && o.Contraseña == contraseña);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar las credenciales del operario: {ex.Message}");
                return null;
            }
        }
    }
}
