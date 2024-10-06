using BlazorApp.Models;

namespace BlazorApp.Data
{
    public interface IOperarioRepository
    {
        Operario GetOperarioById(int id);
        IList<Operario> GetAllOperarios();
        void AddOperario(Operario operario);
        void UpdateOperario(Operario operario);
        void DeleteOperario(int id);
    }
}
