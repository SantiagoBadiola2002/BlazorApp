namespace BlazorApp.Models
{
    public class Cliente
    {
        public string Cedula { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; } //  (Esperando, Atendido, etc.)

        public void Registrar()
        {
            // Lógica para registrar al cliente en la cola
        }

        public void ActualizarEstado(string nuevoEstado)
        {
            Estado = nuevoEstado;
            // Lógica para actualizar el estado en el sistema
        }

    }
}
