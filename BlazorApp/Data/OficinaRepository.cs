using BlazorApp.Models;

namespace BlazorApp.Data
{
    public class OficinaRepository : IOficinaRepository
    {
        private List<Oficina> _oficinas;

        public OficinaRepository()
        {
            _oficinas = new List<Oficina>
            {
                //new Oficina (1, "Oficina Central"),
                //new Oficina (2, "Oficina Norte")
            };
        }

        public Oficina ObtenerOficinaPorId(int id)
        {
            return _oficinas.FirstOrDefault(o => o.Id == id);
        }

        public List<Oficina> ObtenerTodasLasOficinas()
        {
            return _oficinas;
        }

        public void AgregarOficina(Oficina oficina)
        {
            // Generar un nuevo Id único para la nueva oficina
            oficina.Id = _oficinas.Any() ? _oficinas.Max(o => o.Id) + 1 : 1;
            _oficinas.Add(oficina);
        }

        public void ActualizarOficina(Oficina oficina)
        {
            var oficinaExistente = ObtenerOficinaPorId(oficina.Id);
            if (oficinaExistente != null)
            {
                oficinaExistente.Nombre = oficina.Nombre;
                oficinaExistente.PuestosDeAtencion = oficina.PuestosDeAtencion;
                oficinaExistente.ClientesEnEspera = oficina.ClientesEnEspera;
                oficinaExistente.Operarios = oficina.Operarios;
            }
        }

        public void EliminarOficina(int id)
        {
            var oficina = ObtenerOficinaPorId(id);
            if (oficina != null)
            {
                _oficinas.Remove(oficina);
            }
        }
    }
}
