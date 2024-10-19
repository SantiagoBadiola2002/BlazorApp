using BlazorApp.Models.DTs;

namespace BlazorApp.Models
{
    public class DTsMapped
    {
        // Mapeo de Oficina a DTOficina
        public static DTOficina ConvertirAOficinaDTO(Oficina oficina)
        {
            return new DTOficina
            {
                Id = oficina.Id,
                Nombre = oficina.Nombre,
                OperariosIds = oficina.Operarios.Select(o => o.Id).ToList(),
                ClientesIds = oficina.Clientes.Select(c => c.Id).ToList(),
                PuestosDeAtencionIds = oficina.PuestosDeAtencion.Select(p => p.Id).ToList()
            };
        }

        // Mapeo de Operario a DTOperario
        public static DTOperario ConvertirAOperarioDTO(Operario operario)
        {
            return new DTOperario
            {
                Id = operario.Id,
                Nombre = operario.Nombre,
                PuestoAsignadoId = operario.PuestoAsignado.Id,
                EstaDisponible = operario.EstaDisponible,
                OficinaId = operario.OficinaId
            };
        }

        // Mapeo de Cliente a DTCliente
        public static DTCliente ConvertirAClienteDTO(Cliente cliente)
        {
            return new DTCliente
            {
                Id = cliente.Id,
                Cedula = cliente.Cedula,
                FechaRegistro = cliente.FechaRegistro,
                Estado = cliente.Estado
            };
        }

        // Mapeo de Administrador a DTAdministrador
        public static DTAdministrador ConvertirAAdministradorDTO(Administrador administrador)
        {
            return new DTAdministrador
            {
                Id = administrador.Id,
                Nombre = administrador.Nombre
            };
        }

        // Mapeo de Gerente de Calidad a DTGerenteCalidad
        public static DTGerenteCalidad ConvertirAGerenteCalidadDTO(GerenteCalidad gerenteCalidad)
        {
            return new DTGerenteCalidad
            {
                Id = gerenteCalidad.Id,
                Nombre = gerenteCalidad.Nombre
            };
        }
    }
}
