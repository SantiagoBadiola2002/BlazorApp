using BlazorApp.Models;

namespace BlazorApp.Data
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly List<Cliente> _clientes;

        public ClienteRepository()
        {
            _clientes = new List<Cliente>
            {
                new Cliente("111111", DateTime.Today, EstadoCliente.Esperando),
                new Cliente("222222", DateTime.Today, EstadoCliente.Esperando),
                new Cliente("333333", DateTime.Today, EstadoCliente.Esperando)
            };
        }

        public void AgregarCliente(Cliente cliente)
        {
            _clientes.Add(cliente);
        }

        public Cliente ObtenerClientePorCedula(string cedula)
        {
            return _clientes.FirstOrDefault(c => c.Cedula == cedula);
        }

        public IList<Cliente> ObtenerClientesEnEspera()
        {
            return _clientes.Where(c => c.Estado == EstadoCliente.Esperando).ToList();
        }

        public void ActualizarEstadoCliente(string cedula, EstadoCliente nuevoEstado)
        {
            var cliente = ObtenerClientePorCedula(cedula);
            if (cliente != null)
            {
                cliente.ActualizarEstado(nuevoEstado);
            }
        }
    }
}
