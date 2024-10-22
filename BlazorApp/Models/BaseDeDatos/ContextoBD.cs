using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Models.BaseDeDatos
{
    public class ContextoBD : DbContext
    {
        public ContextoBD(DbContextOptions<ContextoBD> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Operario> Operarios { get; set; }
        public DbSet<Oficina> Oficinas { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<GerenteCalidad> GerentesCalidad { get; set; }
        public DbSet<RegistroDeAtencion> RegistrosDeAtencion { get; set; }
    }
}
