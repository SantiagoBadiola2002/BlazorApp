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
            };
        }

        // Mapeo de Operario a DTOperario
        public static DTOperario ConvertirAOperarioDTO(Operario operario)
        {
            return new DTOperario
            {
                Id = operario.Id,
                Nombre = operario.Nombre,
                EstaDisponible = operario.EstaDisponible,
                OficinaId = operario.OficinaId,
                RolOperario = operario.RolOperario,
                Contraseña = operario.Contraseña,
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

    }
}
