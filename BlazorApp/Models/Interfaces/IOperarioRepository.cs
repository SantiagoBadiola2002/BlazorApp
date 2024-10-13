namespace BlazorApp.Models.Interfaces
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
