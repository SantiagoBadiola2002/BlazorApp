using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class Cliente
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        [StringLength(12)]
        public string Cedula { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        [Required]
        [EnumDataType(typeof(EstadoCliente), ErrorMessage = "Estado inválido.")]
        [Display(Name = "Estado del Cliente")]
        public EstadoCliente Estado { get; set; } //0:Esperando, 1:Procesando, 2:Atendido

        public Cliente() { }

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
