using BlazorApp.Models;
using BlazorApp.Models.Interfaces;

namespace BlazorApp.Infraestructure.Data
{
    public class OperarioRepository : IOperarioRepository
    {
        private readonly List<Operario> _operarios;

        public OperarioRepository()
        {
            // Inicialización de la lista de operarios con datos pre-cargados
            _operarios = new List<Operario>
            {
                new Operario(1, "Operario 1", new PuestoAtencion(111, 1, true, 1, 1), true, 1),
                new Operario(2, "Operario 2", new PuestoAtencion(222, 2, true, 2, 1), true, 1),
                new Operario(3, "Operario 3", new PuestoAtencion(333, 3, true, 3, 1), true, 1)
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
