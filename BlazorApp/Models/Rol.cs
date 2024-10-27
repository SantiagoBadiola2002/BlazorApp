using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public enum Rol
    {
        [Display(Name = "Operario")] //0
        Operario,

        [Display(Name = "GerenteDeCalidad")]  //1
        GerenteDeCalidad,

        [Display(Name = "Administrador")] //2
        Administrador,
    }
}
