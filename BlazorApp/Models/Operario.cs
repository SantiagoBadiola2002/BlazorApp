namespace BlazorApp.Models
{
    public class Operario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public PuestoAtencion PuestoAsignado { get; set; }
        public bool EstaDisponible { get; set; } //Esta disponible para antender a un cliente
        public int OficinaId { get; set; } 

        public Operario() { }

        public Operario(int id, string nombre, PuestoAtencion puestoAsignado, bool estaDisponible, int oficinaId)
        {
            Id = id;
            Nombre = nombre;
            PuestoAsignado = puestoAsignado;
            EstaDisponible = estaDisponible;
            OficinaId = oficinaId;
        }

        public void AtenderCliente(Cliente cliente)
        {
            EstaDisponible = false;
            cliente.ActualizarEstado(EstadoCliente.Procesando);
            //Pasa el tiempo
            cliente.ActualizarEstado(EstadoCliente.Atendido);
            EstaDisponible = true;
        }

        public void LiberarPuesto()
        {
            EstaDisponible = true;
            PuestoAsignado.Liberar(); 
        }
    }

}
