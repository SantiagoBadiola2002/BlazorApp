namespace BlazorApp.Models
{
    public class PuestoAtencion
    {
        public int Id { get; set; } 
        public int Numero { get; set; } 
        public bool EstaLibre { get; set; } // Estado del puesto (libre o ocupado)
        public Operario OperarioAsignado { get; set; } 

        public void Liberar()
        {
            EstaLibre = true;
            // Lógica adicional para liberar el puesto
        }

        public void Ocupar(Operario operario)
        {
            OperarioAsignado = operario;
            EstaLibre = false;
            // Lógica adicional para ocupar el puesto
        }
    }

}
