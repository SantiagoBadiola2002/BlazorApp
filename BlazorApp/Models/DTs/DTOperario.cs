namespace BlazorApp.Models.DTs
{
    public class DTOperario
    {
        public int Id { get; set; } 
        public string Nombre { get; set; }
        public int PuestoAsignadoId { get; set; } 
        public bool EstaDisponible { get; set; } 
        public int OficinaId { get; set; } 
    }
}
