using BlazorApp.Models;

namespace BlazorApp.Data
{
    public interface IOficinaRepository
    {
        Oficina ObtenerOficinaPorId(int id);
        List<Oficina> ObtenerTodasLasOficinas();
        void AgregarOficina(Oficina oficina);
        void ActualizarOficina(Oficina oficina);
        void EliminarOficina(int id);
    }
}
