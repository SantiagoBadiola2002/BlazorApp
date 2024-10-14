using BlazorApp.Models;
using BlazorApp.Models.Interfaces;
using BlazorApp.Models.BaseDeDatos;

namespace BlazorApp.Infraestructure.Data
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ContextoBD _context;

        public ClienteRepository(ContextoBD context)
        {
            _context = context;
        }

        public void AgregarCliente(Cliente cliente)
        {
            try
            {
                _context.Clientes.Add(cliente);
                _context.SaveChanges(); // Guarda los cambios en la base de datos
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el cliente: {ex.Message}");
            }
        }

        public Cliente ObtenerClientePorCedula(string cedula)
        {
            try
            {
                return _context.Clientes.FirstOrDefault(c => c.Cedula == cedula);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el cliente por cédula: {ex.Message}");
                return null;
            }
        }

        public IList<Cliente> ObtenerClientesEnEspera()
        {
            try
            {
                return _context.Clientes
                    .Where(c => c.Estado == EstadoCliente.Esperando)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener clientes en espera: {ex.Message}");
                return new List<Cliente>();
            }
        }

        public void ActualizarEstadoCliente(string cedula, EstadoCliente nuevoEstado)
        {
            try
            {
                var cliente = ObtenerClientePorCedula(cedula);
                if (cliente != null)
                {
                    cliente.ActualizarEstado(nuevoEstado);
                    _context.Clientes.Update(cliente); // Marca el cliente como modificado
                    _context.SaveChanges(); // Guarda los cambios en la base de datos
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el estado del cliente: {ex.Message}");
            }
        }
    }
}
