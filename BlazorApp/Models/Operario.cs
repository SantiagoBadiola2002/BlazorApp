namespace BlazorApp.Models
{
    public class Operario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public PuestoAtencion PuestoAsignado { get; set; }
        public bool EstaDisponible { get; set; } 
        public int OficinaId { get; set; } 

        public Operario() { }


        public void AtenderCliente(Cliente cliente)
        {
            cliente.ActualizarEstado(EstadoCliente.Atendido);
            EstaDisponible = false;
            PuestoAsignado.Ocupar(this); // Asegurar que el puesto también esté marcado como ocupado
        }

        public void LiberarPuesto()
        {
            EstaDisponible = true;
            PuestoAsignado.Liberar(); // Asegurar que el puesto se libera
        }
    }

}
