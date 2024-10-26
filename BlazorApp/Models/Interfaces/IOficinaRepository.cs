using BlazorApp.Models.DTs;
using Google.Protobuf.WellKnownTypes;

namespace BlazorApp.Models.Interfaces { 

        public interface IOficinaRepository
        {
            DTOficina ObtenerOficinaPorIdDTO(int id);
            Task<List<DTOficina>> ObtenerTodasLasOficinasDTOAsync();
            void AgregarOficina(Oficina oficina);
            void ActualizarOficina(Oficina oficina);
            void EliminarOficina(int id);
            Task<List<DTCliente>> ObtenerClientesEnEsperaPorOficinaDTOAsync(int oficinaId);
            Task<List<DTCliente>> ObtenerClientesProcesandoPorOficinaDTOAsync(int oficinaId);
        Task RegistrarClienteEnOficinaAsync(int oficinaId, DTCliente nuevoCliente);
            Task AtenderCliente(int clienteId, int operarioId, int oficinaId, DateTime fecha, int Duracion);
            //DTAdministrador ObtenerAdministradorPorIdDTO(int id);
            //DTGerenteCalidad ObtenerGerenteCalidadPorIdDTO(int id);
        }
    }
