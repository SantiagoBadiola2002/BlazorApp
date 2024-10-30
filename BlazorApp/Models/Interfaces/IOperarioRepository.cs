using BlazorApp.Models.DTs;

namespace BlazorApp.Models.Interfaces
{
    public interface IOperarioRepository
    {
        Task<DTOperario> GetOperarioByIdAsync(int id);
        Task<IList<DTOperario>> GetAllOperariosAsync();
        Task AddOperarioAsync(DTOperario operario);
        Task UpdateOperarioAsync(DTOperario operario);
        Task DeleteOperarioAsync(int id);
        Task<DTOperario> VerificarCredencialesAsync(string nombre, string contraseña);
        Task<List<DTOperario>> ObtenerOperariosPorOficinaIdAsync(int oficinaId);
        Task<DTOperario> VerificarCredencialesAsync(int idOficina, string nombre, string contraseña, Rol rol);
    }
}
