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
            _context.Clientes.Add(cliente);
            _context.SaveChanges(); // Guarda los cambios en la base de datos
        }

        public Cliente ObtenerClientePorCedula(string cedula)
        {
            return _context.Clientes.FirstOrDefault(c => c.Cedula == cedula);
        }

        public IList<Cliente> ObtenerClientesEnEspera()
        {
            return _context.Clientes
                .Where(c => c.Estado == EstadoCliente.Esperando)
                .ToList();
        }

        public void ActualizarEstadoCliente(string cedula, EstadoCliente nuevoEstado)
        {
            var cliente = ObtenerClientePorCedula(cedula);
            if (cliente != null)
            {
                cliente.ActualizarEstado(nuevoEstado);
                _context.Clientes.Update(cliente); // Marca el cliente como modificado
                _context.SaveChanges(); // Guarda los cambios en la base de datos
            }
        }
    }
}
