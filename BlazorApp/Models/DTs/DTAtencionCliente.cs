namespace BlazorApp.Models.DTs
{
    public class DTAtencionCliente
    {
        public int RegistroDeAtencionId { get; set; }
        public int OperarioId { get; set; }
        public int ClienteId { get; set; }
        public int OficinaId { get; set; }
        public DateTime Fecha { get; set; }
        public int Duracion { get; set; } // Duración en minutos
    }
}

