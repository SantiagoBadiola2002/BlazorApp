using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class PuestoAtencion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Numero { get; set; }

        public bool EstaLibre { get; set; } // Estado del puesto (libre o ocupado)

        [ForeignKey("Operario")]
        public int IdOperarioAsignado { get; set; }

        [ForeignKey("Oficina")]
        public int OficinaId { get; set; }

        public PuestoAtencion() { }

        public PuestoAtencion(int id, int numero, bool estaLibre, int operarioAsignado, int oficinaId)
        {
            Id = id;
            Numero = numero;
            EstaLibre = estaLibre;
            IdOperarioAsignado = operarioAsignado;
            OficinaId = oficinaId;
        }

        public void Liberar()
        {
            EstaLibre = true;
        }

        public void Ocupar()
        {
            EstaLibre = false;
        }

        public void AsignarOperario(int IdOperario)
        {
            IdOperarioAsignado = IdOperario;
            Ocupar();
        }
    }
}