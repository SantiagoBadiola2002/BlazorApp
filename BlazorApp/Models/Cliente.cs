namespace BlazorApp.Models
{
    public class Cliente
    {
        public string Cedula { get;  set; }
        public DateTime FechaRegistro { get;  set; }
        public EstadoCliente Estado { get;  set; }

        public Cliente(string cedula)
        {
            Cedula = cedula;
            FechaRegistro = DateTime.Now;
            Estado = EstadoCliente.Esperando;
        }

        public Cliente(string cedula, DateTime fechaRegistro, EstadoCliente estado)
        {
            Cedula = cedula;
            FechaRegistro = fechaRegistro;
            Estado = estado;
        }

        public void ActualizarEstado(EstadoCliente nuevoEstado)
        {
            Estado = nuevoEstado;
        }
    }
}
