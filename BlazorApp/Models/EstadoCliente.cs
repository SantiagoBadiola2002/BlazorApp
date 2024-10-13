using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public enum EstadoCliente
    {
        [Display(Name = "Esperando")] //0
        Esperando,

        [Display(Name = "Procesando")]  //1
        Procesando,

        [Display(Name = "Atendido")] //2
        Atendido,
    }
}
