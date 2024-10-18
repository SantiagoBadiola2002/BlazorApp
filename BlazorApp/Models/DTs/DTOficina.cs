namespace BlazorApp.Models.DTs
{
    public class DTOficina
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } 
        public List<int> OperariosIds { get; set; } 
        public List<int> ClientesIds { get; set; } 
        public List<int> PuestosDeAtencionIds { get; set; } 
    }
}
