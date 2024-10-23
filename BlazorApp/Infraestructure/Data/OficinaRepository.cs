using BlazorApp.Models.BaseDeDatos;
using BlazorApp.Models;
using BlazorApp.Models.DTs;
using BlazorApp.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Components.Pages.Operarios;

namespace BlazorApp.Infraestructure.Data
{
    public class OficinaRepository : IOficinaRepository
    {
        private readonly ContextoBD _contexto;

        public OficinaRepository(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        // Obtener una oficina por su ID y devolver como DTO
        public DTOficina ObtenerOficinaPorIdDTO(int id)
        {
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            var oficina = _contexto.Oficinas
                            .Include(o => o.Operarios)
                            .Include(o => o.Clientes)
                            .FirstOrDefault(o => o.Id == id);
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo

            return DTsMapped.ConvertirAOficinaDTO(oficina);
        }

        // Obtener todas las oficinas y devolver como DTOs
        public async Task<List<DTOficina>> ObtenerTodasLasOficinasDTOAsync()
        {
            var oficinas = await _contexto.Oficinas
                            .Include(o => o.Operarios)
                            .Include(o => o.Clientes)
                            .ToListAsync();

            return oficinas.Select(DTsMapped.ConvertirAOficinaDTO).ToList();
        }

        // Obtener clientes en espera por oficina y devolver como DTOs
        public async Task<List<DTCliente>> ObtenerClientesEnEsperaPorOficinaDTOAsync(int oficinaId)
        {
            var oficina = await _contexto.Oficinas
                .Include(o => o.Clientes)
                .FirstOrDefaultAsync(o => o.Id == oficinaId);

            if (oficina == null)
            {
                return new List<DTCliente>();
            }

            return oficina.Clientes
                .Where(c => c.Estado == EstadoCliente.Esperando)
                .OrderBy(c => c.FechaRegistro)
                .Select(DTsMapped.ConvertirAClienteDTO)
                .ToList();
        }

        public void AgregarOficina(Oficina oficina)
        {
            _contexto.Oficinas.Add(oficina);
            _contexto.SaveChanges();
        }

        public void EliminarOficina(int id)
        {
            var oficina = _contexto.Oficinas.FirstOrDefault(o => o.Id == id);
            if (oficina != null)
            {
                _contexto.Oficinas.Remove(oficina);
                _contexto.SaveChanges();
            }
        }

        public void ActualizarOficina(Oficina oficina)
        {
            var oficinaExistente = _contexto.Oficinas
                                            .Include(o => o.Operarios)
                                            .Include(o => o.Clientes)
                                            .FirstOrDefault(o => o.Id == oficina.Id);

            if (oficinaExistente != null)
            {
                oficinaExistente.Nombre = oficina.Nombre;
                oficinaExistente.Operarios = oficina.Operarios;
                oficinaExistente.Clientes = oficina.Clientes;

                _contexto.SaveChanges();
            }
        }

        public async Task RegistrarClienteEnOficinaAsync(int oficinaId, DTCliente nuevoCliente)
        {
            // Obtener la oficina y verificar si existe
            var oficina = await _contexto.Oficinas
                .Include(o => o.Clientes) // Incluir los clientes existentes
                .FirstOrDefaultAsync(o => o.Id == oficinaId);

            if (oficina != null)
            {
                // Verificar si hay algún cliente con la misma cédula y en estado 'Esperando'
                var clienteExistente = oficina.Clientes.FirstOrDefault(c => c.Cedula == nuevoCliente.Cedula && c.Estado == EstadoCliente.Esperando);

                if (clienteExistente == null)
                {
                    // No hay clientes con la misma cédula en estado 'Esperando', se puede registrar el nuevo cliente
                    var cliente = new Cliente
                    {
                        Cedula = nuevoCliente.Cedula,
                        FechaRegistro = nuevoCliente.FechaRegistro,
                        Estado = nuevoCliente.Estado
                    };

                    // Agregar el cliente a la lista de clientes de la oficina
                    oficina.Clientes.Add(cliente);

                    // Guardar los cambios en la base de datos
                    await _contexto.SaveChangesAsync();

                    Console.WriteLine("### OficinaRepository ###");
                    Console.WriteLine("Cliente Cedula: " + nuevoCliente.Cedula + " registrado en la oficina ID: " + oficina.Id);
                    Console.WriteLine("#########################");
                }
                else
                {
                    // Existe un cliente con la misma cédula en estado 'Esperando'
                    Console.WriteLine("### OficinaRepository ###");
                    Console.WriteLine("No se puede registrar el cliente con cédula: " + nuevoCliente.Cedula + " porque ya existe uno con estado 'Esperando' en la oficina ID: " + oficina.Id);
                    Console.WriteLine("#########################");
                }
            }
            else
            {
                // La oficina no existe
                Console.WriteLine("### OficinaRepository ###");
                Console.WriteLine("Oficina no encontrada para la ID: " + oficinaId);
                Console.WriteLine("#########################");
            }
        }

        public async Task AtenderCliente(int clienteId, int operarioId, int oficinaId, DateTime fecha, int duracion)
        {
            // Buscar la oficina, el cliente, y el operario para asegurarse de que existen
            var oficina = await _contexto.Oficinas
                .Include(o => o.Clientes) // Incluimos los clientes
                .Include(o => o.Operarios) // Incluimos los operarios
                .FirstOrDefaultAsync(o => o.Id == oficinaId);

            if (oficina == null)
            {
                // Si la oficina no existe, lanzar una excepción o manejar el error de alguna otra manera
                throw new Exception($"Oficina con ID {oficinaId} no encontrada.");
            }

            var cliente = oficina.Clientes.FirstOrDefault(c => c.Id == clienteId && c.Estado == EstadoCliente.Esperando);
            if (cliente == null)
            {
                // Si no se encuentra un cliente en estado "Esperando", manejar el error
                throw new Exception($"Cliente con ID {clienteId} no encontrado o no está en espera.");
            }

            var operario = oficina.Operarios.FirstOrDefault(o => o.Id == operarioId);
            if (operario == null)
            {
                // Si el operario no existe en la oficina, lanzar una excepción o manejar el error
                throw new Exception($"Operario con ID {operarioId} no encontrado en la oficina ID {oficinaId}.");
            }

            // Crear un nuevo registro de atención
            var atencion = new RegistroDeAtencion
            {
                ClienteId = cliente.Id,
                OperarioId = operario.Id,
                OficinaId = oficina.Id,
                Fecha = fecha,
                DuracionAtencion = duracion
            };

            // Cambiar el estado del cliente a "Atendido"
            cliente.Estado = EstadoCliente.Atendido;

            // Agregar el registro de atención al contexto de la base de datos
            _contexto.RegistrosDeAtencion.Add(atencion);

            // Guardar los cambios en la base de datos
            await _contexto.SaveChangesAsync();

            Console.WriteLine("### OficinaRepository ###");
            Console.WriteLine($"Cliente ID: {cliente.Id} atendido por Operario ID: {operario.Id} en la Oficina ID: {oficina.Id}.");
            Console.WriteLine("#########################");
        }



    }
}
