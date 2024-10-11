namespace BlazorApp.Models
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
