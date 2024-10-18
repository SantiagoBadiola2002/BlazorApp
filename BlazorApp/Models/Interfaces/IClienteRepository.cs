namespace BlazorApp.Models.Interfaces
{
    public interface IClienteRepository
    {
        void AgregarCliente(Cliente cliente);
        Cliente ObtenerClientePorCedula(string cedula);
        Cliente ObtenerClientePorId(int id);
        IList<Cliente> ObtenerClientesEnEspera();
        void ActualizarEstadoClientePorCedula(string cedula, EstadoCliente nuevoEstado);
        void ActualizarEstadoClientePorId(int id, EstadoCliente nuevoEstado);
    }
}
