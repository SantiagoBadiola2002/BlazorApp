using BlazorApp.Models.DTs;

namespace BlazorApp.Models.Interfaces
{
    public interface IClienteRepository
    {
        Task<DTCliente> ObtenerClientePorIdAsync(int id);
        Task AgregarClienteAsync(DTCliente cliente);
        Task<DTCliente> ObtenerClientePorCedulaAsync(string cedula);
        Task<IList<DTCliente>> ObtenerClientesEnEsperaAsync();
        Task ActualizarEstadoClientePorCedulaAsync(string cedula, EstadoCliente nuevoEstado);
        Task ActualizarEstadoClientePorIdAsync(int id, EstadoCliente nuevoEstado);
    }
}
