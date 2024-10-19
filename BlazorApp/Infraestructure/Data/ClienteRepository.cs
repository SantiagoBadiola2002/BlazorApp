using BlazorApp.Models;
using BlazorApp.Models.Interfaces;
using BlazorApp.Models.BaseDeDatos;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Models.DTs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Infraestructure.Data
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ContextoBD _context;

        public ClienteRepository(ContextoBD context)
        {
            _context = context;
        }

        public async Task<DTCliente> ObtenerClientePorIdAsync(int id)
        {
            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
                return cliente != null ? DTsMapped.ConvertirAClienteDTO(cliente) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el cliente por Id: {ex.Message}");
                return null;
            }
        }

        public async Task AgregarClienteAsync(DTCliente clienteDTO)
        {
            try
            {
                var cliente = new Cliente
                {
                    Id = clienteDTO.Id,
                    Cedula = clienteDTO.Cedula,
                    FechaRegistro = clienteDTO.FechaRegistro,
                    Estado = clienteDTO.Estado
                };

                await _context.Clientes.AddAsync(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el cliente: {ex.Message}");
            }
        }

        public async Task<DTCliente> ObtenerClientePorCedulaAsync(string cedula)
        {
            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Cedula == cedula);
                return cliente != null ? DTsMapped.ConvertirAClienteDTO(cliente) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el cliente por cédula: {ex.Message}");
                return null;
            }
        }

        public async Task<IList<DTCliente>> ObtenerClientesEnEsperaAsync()
        {
            try
            {
                var clientes = await _context.Clientes
                    .Where(c => c.Estado == EstadoCliente.Esperando)
                    .ToListAsync();

                return clientes.Select(c => DTsMapped.ConvertirAClienteDTO(c)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener clientes en espera: {ex.Message}");
                return new List<DTCliente>();
            }
        }

        public async Task ActualizarEstadoClientePorCedulaAsync(string cedula, EstadoCliente nuevoEstado)
        {
            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Cedula == cedula);
                if (cliente != null)
                {
                    cliente.ActualizarEstado(nuevoEstado);
                    _context.Clientes.Update(cliente);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el estado del cliente por cédula: {ex.Message}");
            }
        }

        public async Task ActualizarEstadoClientePorIdAsync(int id, EstadoCliente nuevoEstado)
        {
            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
                if (cliente != null)
                {
                    cliente.ActualizarEstado(nuevoEstado);
                    _context.Clientes.Update(cliente);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el estado del cliente por Id: {ex.Message}");
            }
        }
    }
}
