using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Models
{
    [Table("Administradores")] 
    public class Administrador
    {
        [Key]
        public int Id { get; set; } 

        [Required] 
        [StringLength(30, ErrorMessage = "El nombre no puede exceder los 30 caracteres.")] 
        public string Nombre { get; set; } 

        public void AgregarOficina(Oficina oficina)
        {
            // Lógica para agregar una nueva oficina al sistema
        }

        public void EliminarOficina(int oficinaId)
        {
            // Lógica para eliminar una oficina del sistema
        }

        public void ActualizarOficina(Oficina oficina)
        {
            // Lógica para actualizar los datos de una oficina
        }
    }
}
