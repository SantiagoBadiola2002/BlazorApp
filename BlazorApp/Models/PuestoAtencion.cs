namespace BlazorApp.Models
{
    public class PuestoAtencion
    {
        public int Id { get; set; } 
        public int Numero { get; set; } 
        public bool EstaLibre { get; set; } // Estado del puesto (libre o ocupado)
        public int IdOperarioAsignado { get; set; }
        public int OficinaId { get; set; }

        public PuestoAtencion() { 
        }
        public PuestoAtencion(int id, int numero, bool estaLibre, int operarioAsignado, int oficinaId)
        {
            Id = id;
            Numero = numero;
            EstaLibre = estaLibre;
            IdOperarioAsignado = operarioAsignado;
            OficinaId = oficinaId;
        }

        public void Liberar()
        {
            EstaLibre = true;
        }

        public void Ocupar()
        {
            EstaLibre = false;
        }

        public void asignarOperario(int IdOperario)
        {
            IdOperarioAsignado = IdOperario;
            Ocupar();

        }
    }

}
