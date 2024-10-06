using BlazorApp.Models;
using BlazorApp.Models;

namespace BlazorApp.Data
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly List<Cliente> _clientes = new List<Cliente>();

        public ClienteRepository()
        {
            this._clientes = new List<Cliente>
            {
                //new Cliente("11111111", new DateTime(2024, 10, 05), EstadoCliente.Esperando),
                //new Cliente("22222222", new DateTime(2024, 10, 05), EstadoCliente.Atendido),
                //new Cliente("33333333", new DateTime(2024, 10, 04), EstadoCliente.NoSePresento),
                //new Cliente("44444444", new DateTime(2024, 10, 03), EstadoCliente.Esperando)
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
