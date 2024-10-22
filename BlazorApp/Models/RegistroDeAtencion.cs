using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Models
{
    public class RegistroDeAtencion
    {
        [Key] // Asumiendo que esta es la clave primaria de la tabla
        public int RegistroDeAtencionId { get; set; }

        [ForeignKey("OperarioId")]
        public int OperarioId { get; set; }
        public Operario Operario { get; set; } // Relación con Operario

        [ForeignKey("ClienteId")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } // Relación con Cliente

        [ForeignKey("OficinaId")]
        public int OficinaId { get; set; }
        public Oficina Oficina { get; set; } // Relación con Oficina

        [Required] // Hace que la fecha sea obligatoria
        public DateTime Fecha { get; set; }

        [Required] // Hace que la duración sea obligatoria
        public TimeSpan DuracionAtencion { get; set; }

        // Constructor vacío
        public RegistroDeAtencion()
        {
        }

        // Constructor con parámetros
        public RegistroDeAtencion(int operarioId, int clienteId, int oficinaId, DateTime fecha, TimeSpan duracionAtencion)
        {
            OperarioId = operarioId;
            ClienteId = clienteId;
            OficinaId = oficinaId;
            Fecha = fecha;
            DuracionAtencion = duracionAtencion;
        }
    }
}
