using BlazorApp.Models.DTs;

namespace BlazorApp.Models.Interfaces
{
    public interface IClienteRepository
    {
        DTCliente ObtenerClientePorId(int id);
        void AgregarCliente(DTCliente cliente);
        void ActualizarCliente(DTCliente clienteDTO);
        DTCliente ObtenerClientePorCedula(string cedula);
        IList<DTCliente> ObtenerClientesEnEspera();
        void ActualizarEstadoClientePorCedula(string cedula, EstadoCliente nuevoEstado);
        void ActualizarEstadoClientePorId(int id, EstadoCliente nuevoEstado);
    }
}
