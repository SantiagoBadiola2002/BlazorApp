using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class Cliente
    {
        [Key]
        [Required]
        [StringLength(12)] 
        public string Cedula { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        [Required]
        public EstadoCliente Estado { get; set; }

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