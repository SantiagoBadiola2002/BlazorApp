namespace BlazorApp.Models
{
    public class RegistroDeAtencion
    {
        public int idOperario { get; set; }
        public int idCliente { get; set; }
        public int idOficina { get; set; }
        public string fecha { get; set; }
        public TimeSpan duracionAtencion { get; set; }
    }
}
