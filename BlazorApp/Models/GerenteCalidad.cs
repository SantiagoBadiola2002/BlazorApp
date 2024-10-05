namespace BlazorApp.Models
{
    public class GerenteCalidad
    {
        public int Id { get; set; } // Identificador único del gerente
        public string Nombre { get; set; } // Nombre del gerente
        public int ClientesAtendidos { get; set; }
        public TimeSpan TiempoPromedioAtencion { get; set; }
        public void AnalizarDatos()
        {
            // Lógica para analizar datos de atención al cliente
            // Puede incluir estadísticas de tiempos de espera, satisfacción, etc.
        }

        public void GenerarReporte()
        {
            // Lógica para generar un reporte de calidad
        }
    }

}
