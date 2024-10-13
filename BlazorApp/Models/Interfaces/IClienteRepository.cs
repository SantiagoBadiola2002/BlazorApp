namespace BlazorApp.Models.Interfaces
{
    public interface IClienteRepository
    {
        void AgregarCliente(Cliente cliente);
        Cliente ObtenerClientePorCedula(string cedula);
        IList<Cliente> ObtenerClientesEnEspera();
        void ActualizarEstadoCliente(string cedula, EstadoCliente nuevoEstado);
    }
}
