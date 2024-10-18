namespace BlazorApp.Models.DTs
{
    public class DTPuestoAtencion
    {
        public int Id { get; set; } 
        public int Numero { get; set; } 
        public bool EstaLibre { get; set; } // Estado del puesto (libre o ocupado)
        public int IdOperarioAsignado { get; set; } 
        public int OficinaId { get; set; } 
    }
}
