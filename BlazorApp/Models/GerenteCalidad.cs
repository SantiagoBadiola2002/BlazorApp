using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Models
{
    [Table("GerentesCalidad")] // Opcional: define el nombre de la tabla en la base de datos
    public class GerenteCalidad
    {
        [Key]
        public int Id { get; set; } // Identificador único del gerente

        [Required] // Indica que el campo es obligatorio
        [StringLength(30, ErrorMessage = "El nombre no puede exceder los 30 caracteres.")] // Longitud máxima del nombre
        public string Nombre { get; set; } // Nombre del gerente

        public void AnalizarDatos()
        {
            // Lógica para analizar datos de atención al cliente
            // Puede incluir estadísticas de tiempos de espera, satisfacción, etc.
        }

        public void GenerarReporte()
        {
            // Lógica para generar un reporte de calidad
        }
    }
}
