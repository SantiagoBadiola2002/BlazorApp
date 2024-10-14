namespace BlazorApp.Models.Interfaces
{
    public interface IOficinaRepository
    {
        Oficina ObtenerOficinaPorId(int id);
        Task<List<Oficina>> ObtenerTodasLasOficinasAsync();
        void AgregarOficina(Oficina oficina);
        void ActualizarOficina(Oficina oficina);
        void EliminarOficina(int id);
        Task<List<Cliente>> ObtenerClientesEnEsperaPorOficinaAsync(int idOficina);
    }
}
