using BlazorApp.Models.DTs;

namespace BlazorApp.Models.Interfaces
{
    public interface IOficinaRepository
    {
        DTOficina ObtenerOficinaPorIdDTO(int id);
        List<DTOficina> ObtenerTodasLasOficinasDTO();
        List<DTOficina> ListarClientesOficinasDTO();
        List<DTAtencionCliente> ObtenerTodosLosRegistros(int dia, int mes, int anio);
        List<DTAtencionCliente> ObtenerClientesPorMes(int año);
        public List<int> ObtenerAñosDisponibles();
        void AgregarOficina(Oficina oficina);
        void ActualizarOficina(Oficina oficina);
        void EliminarOficina(int id);
        List<DTCliente> ObtenerClientesEnEsperaPorOficinaDTO(int oficinaId);
        List<DTCliente> ObtenerClientesProcesandoPorOficinaDTO(int oficinaId);
        void RegistrarClienteEnOficina(int oficinaId, DTCliente nuevoCliente);
        void AtenderCliente(int clienteId, int operarioId, int oficinaId, DateTime fecha, int Duracion);
        // DTAdministrador ObtenerAdministradorPorIdDTO(int id);
        // DTGerenteCalidad ObtenerGerenteCalidadPorIdDTO(int id);
    }
}
