using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class Oficina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        public List<Operario> Operarios { get; set; }
        public List<Cliente> Clientes { get; set; }
        public List<PuestoAtencion> PuestosDeAtencion { get; set; }


        public Oficina()
        {
            Operarios = new List<Operario>();
            Clientes = new List<Cliente>();
            PuestosDeAtencion = new List<PuestoAtencion>();
        }

        public void AgregarCliente(Cliente cliente)
        {
            Clientes.Add(cliente);
        }

        public void ActualizarMonitores()
        {
        }

        public void AsignarPuestoAOperario(Operario operario, PuestoAtencion puesto)
        {
            if (puesto.EstaLibre && operario.EstaDisponible)
            {
                puesto.Ocupar();
            }
        }
    }
}