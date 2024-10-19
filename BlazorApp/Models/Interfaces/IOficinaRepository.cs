using BlazorApp.Models.DTs;

namespace BlazorApp.Models.Interfaces { 

        public interface IOficinaRepository
        {
            DTOficina ObtenerOficinaPorIdDTO(int id);
            Task<List<DTOficina>> ObtenerTodasLasOficinasDTOAsync();
            void AgregarOficina(Oficina oficina);
            void ActualizarOficina(Oficina oficina);
            void EliminarOficina(int id);
            Task<List<DTCliente>> ObtenerClientesEnEsperaPorOficinaDTOAsync(int oficinaId);
            Task RegistrarClienteEnOficinaAsync(int oficinaId, DTCliente nuevoCliente);
            //DTAdministrador ObtenerAdministradorPorIdDTO(int id);
            //DTGerenteCalidad ObtenerGerenteCalidadPorIdDTO(int id);
        }
    }
