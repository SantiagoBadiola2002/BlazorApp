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

        public int Mes => Fecha.Month;        // Número del mes (1-12) para ordenar
        public string NombreMes => $"{Fecha.ToString("MMMM")} {Fecha.Year}"; // Nombre del mes y el año

        public string NombreOperario { get; set; } 
        public string NombreOficina { get; set; }


    }
}

