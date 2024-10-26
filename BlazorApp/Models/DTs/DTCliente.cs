namespace BlazorApp.Models.DTs
{
    public class DTCliente
    {
        public int Id { get; set; } 
        public string Cedula { get; set; } 
        public DateTime FechaRegistro { get; set; } 
        public EstadoCliente Estado { get; set; } // Estado del cliente: Esperando, Procesando, Atendido
        public string Operario { get; set; }
    }
}
