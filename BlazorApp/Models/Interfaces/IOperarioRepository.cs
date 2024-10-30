using BlazorApp.Models.DTs;

namespace BlazorApp.Models.Interfaces
{
    public interface IOperarioRepository
    {
        DTOperario GetOperarioById(int id);
        IList<DTOperario> GetAllOperarios();
        void AddOperario(DTOperario operario);
        void UpdateOperario(DTOperario operario);
        void DeleteOperario(int id);
        DTOperario VerificarCredenciales(string nombre, string contraseña);
        List<DTOperario> ObtenerOperariosPorOficinaId(int oficinaId);
        DTOperario VerificarCredenciales(int idOficina, string nombre, string contraseña, Rol rol);
    }
}
