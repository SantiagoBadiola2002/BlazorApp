using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class Operario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        public bool EstaDisponible { get; set; }

        [ForeignKey("Oficina")]
        public int OficinaId { get; set; }

        // Nueva propiedad para la contraseña
        [Required]
        [StringLength(100, ErrorMessage = "La contraseña debe tener al menos 8 caracteres y un máximo de 100.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        [Required]
        [EnumDataType(typeof(Rol), ErrorMessage = "Estado inválido.")]
        [Display(Name = "Rol del usuario")]
        public Rol RolOperario { get; set; } //0:Operario, 1:GerenteDeCalidad, 2:Administrador

        public Operario() { }

        public Operario(int id, string nombre, bool estaDisponible, int oficinaId, string contraseña, Rol rolOperario)
        {
            Id = id;
            Nombre = nombre;
            EstaDisponible = estaDisponible;
            OficinaId = oficinaId;
            Contraseña = contraseña;
            RolOperario = rolOperario;
        }

        public void AtenderCliente(Cliente cliente)
        {
            EstaDisponible = false;
            cliente.ActualizarEstado(EstadoCliente.Procesando);
            // Pasa el tiempo
            cliente.ActualizarEstado(EstadoCliente.Atendido);
            EstaDisponible = true;
        }

    }
}
