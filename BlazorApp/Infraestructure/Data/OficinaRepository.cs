using BlazorApp.Models.BaseDeDatos;
using BlazorApp.Models;
using BlazorApp.Models.DTs;
using BlazorApp.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                            .Include(o => o.PuestosDeAtencion)
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
                            .Include(o => o.PuestosDeAtencion)
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
                                            .Include(o => o.PuestosDeAtencion)
                                            .FirstOrDefault(o => o.Id == oficina.Id);

            if (oficinaExistente != null)
            {
                oficinaExistente.Nombre = oficina.Nombre;
                oficinaExistente.Operarios = oficina.Operarios;
                oficinaExistente.Clientes = oficina.Clientes;
                oficinaExistente.PuestosDeAtencion = oficina.PuestosDeAtencion;

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


    }
}
