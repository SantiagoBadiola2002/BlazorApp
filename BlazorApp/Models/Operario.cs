namespace BlazorApp.Models
{
    public class Operario
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } 
        public int Puesto { get; set; } 

        public void AtenderCliente(Cliente cliente)
        {
            // Lógica para atender al cliente
            cliente.ActualizarEstado("Atendido");
            // Otras acciones necesarias para la atención del cliente
        }

        public void LiberarPuesto()
        {
            // Lógica para liberar el puesto cuando no hay clientes en espera
        }
    }

}
