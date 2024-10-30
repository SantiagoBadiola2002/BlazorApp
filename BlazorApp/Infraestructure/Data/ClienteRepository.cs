using BlazorApp.Models;
using BlazorApp.Models.Interfaces;
using BlazorApp.Models.BaseDeDatos;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Models.DTs;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Infraestructure.Data
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ContextoBD _context;

        public ClienteRepository(ContextoBD context)
        {
            _context = context;
        }

        public DTCliente ObtenerClientePorId(int id)
        {
            try
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);
                return cliente != null ? DTsMapped.ConvertirAClienteDTO(cliente) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el cliente por Id: {ex.Message}");
                return null;
            }
        }

        public void AgregarCliente(DTCliente clienteDTO)
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

                _context.Clientes.Add(cliente);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el cliente: {ex.Message}");
            }
        }

        public DTCliente ObtenerClientePorCedula(string cedula)
        {
            try
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.Cedula == cedula);
                return cliente != null ? DTsMapped.ConvertirAClienteDTO(cliente) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el cliente por cédula: {ex.Message}");
                return null;
            }
        }

        public IList<DTCliente> ObtenerClientesEnEspera()
        {
            try
            {
                var clientes = _context.Clientes
                    .Where(c => c.Estado == EstadoCliente.Esperando)
                    .ToList();

                return clientes.Select(c => DTsMapped.ConvertirAClienteDTO(c)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener clientes en espera: {ex.Message}");
                return new List<DTCliente>();
            }
        }

        public void ActualizarEstadoClientePorCedula(string cedula, EstadoCliente nuevoEstado)
        {
            try
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.Cedula == cedula);
                if (cliente != null)
                {
                    cliente.ActualizarEstado(nuevoEstado);
                    _context.Clientes.Update(cliente);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el estado del cliente por cédula: {ex.Message}");
            }
        }

        public void ActualizarEstadoClientePorId(int id, EstadoCliente nuevoEstado)
        {
            try
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);
                if (cliente != null)
                {
                    cliente.ActualizarEstado(nuevoEstado);
                    _context.Clientes.Update(cliente);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el estado del cliente por Id: {ex.Message}");
            }
        }

        public void ActualizarCliente(DTCliente clienteDTO)
        {
            try
            {
                var clienteExistente = _context.Clientes.FirstOrDefault(c => c.Id == clienteDTO.Id);

                if (clienteExistente != null)
                {
                    clienteExistente.Cedula = clienteDTO.Cedula;
                    clienteExistente.FechaRegistro = clienteDTO.FechaRegistro;
                    clienteExistente.Estado = clienteDTO.Estado;

                    _context.Clientes.Update(clienteExistente);
                    _context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"Cliente con ID {clienteDTO.Id} no encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el cliente: {ex.Message}");
            }
        }
    }
}
