using BlazorApp.Models;

namespace BlazorApp.Data
{
    public class OperarioRepository : IOperarioRepository
    {
        private readonly List<Operario> _operarios;

        public OperarioRepository()
        {
            // Inicialización de la lista de operarios (ejemplo)
            _operarios = new List<Operario>
            {
                //new Operario { Id = 1, Nombre = "Juan Pérez", EstaDisponible = true, OficinaId = 1 },
                //new Operario { Id = 2, Nombre = "Ana Gómez", EstaDisponible = true, OficinaId = 1 }
            };
        }

        public Operario GetOperarioById(int id)
        {
            return _operarios.FirstOrDefault(o => o.Id == id);
        }

        public IList<Operario> GetAllOperarios()
        {
            return _operarios;
        }

        public void AddOperario(Operario operario)
        {
            _operarios.Add(operario);
        }

        public void UpdateOperario(Operario operario)
        {
            var existingOperario = GetOperarioById(operario.Id);
            if (existingOperario != null)
            {
                existingOperario.Nombre = operario.Nombre;
                existingOperario.EstaDisponible = operario.EstaDisponible;
                existingOperario.PuestoAsignado = operario.PuestoAsignado;
                existingOperario.OficinaId = operario.OficinaId;
            }
        }

        public void DeleteOperario(int id)
        {
            var operario = GetOperarioById(id);
            if (operario != null)
            {
                _operarios.Remove(operario);
            }
        }
    }
}
