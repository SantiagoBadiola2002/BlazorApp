using BlazorApp.Models.BaseDeDatos;
using BlazorApp.Models;
using BlazorApp.Models.DTs;
using BlazorApp.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Infraestructure.Data
{
    public class OficinaRepository : IOficinaRepository
    {
        private readonly ContextoBD _contexto;

        public OficinaRepository(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        public DTOficina ObtenerOficinaPorIdDTO(int id)
        {
            var oficina = _contexto.Oficinas
                            .Include(o => o.Operarios)
                            .Include(o => o.Clientes)
                            .FirstOrDefault(o => o.Id == id);

            return DTsMapped.ConvertirAOficinaDTO(oficina);
        }

        public List<DTOficina> ObtenerTodasLasOficinasDTO()
        {
            var oficinas = _contexto.Oficinas
                            .Include(o => o.Operarios)
                            .Include(o => o.Clientes)
                            .ToList();

            return oficinas.Select(DTsMapped.ConvertirAOficinaDTO).ToList();
        }

        public List<DTOficina> ListarClientesOficinasDTO()
        {
            return _contexto.Oficinas
                .Include(o => o.Clientes)
                .Select(o => new DTOficina
                {
                    Id = o.Id,
                    Nombre = o.Nombre,
                    ClientesIds = o.Clientes
                        .Where(c => c.Estado == EstadoCliente.Esperando)
                        .Select(c => c.Id)
                        .ToList(),
                })
                .ToList();
        }

        public List<DTAtencionCliente> ObtenerTodosLosRegistros(int dia, int mes, int anio)
        {
            var registros = _contexto.RegistrosDeAtencion
                .Where(r => r.Fecha.Day == dia && r.Fecha.Month == mes && r.Fecha.Year == anio)
                .ToList();

            return registros.Select(r => new DTAtencionCliente
            {
                RegistroDeAtencionId = r.RegistroDeAtencionId,
                OperarioId = r.OperarioId,
                ClienteId = r.ClienteId,
                OficinaId = r.OficinaId,
                Fecha = r.Fecha,
                Duracion = r.DuracionAtencion
            }).ToList();
        }

        public List<DTAtencionCliente> ObtenerClientesPorMes(int año)
        {
            var registros = _contexto.RegistrosDeAtencion
                .Where(r => r.Fecha.Year == año)
                .ToList();

            var clientesPorMes = registros
                .GroupBy(r => r.Fecha.Month)
                .Select(g => new DTAtencionCliente
                {
                    Fecha = new DateTime(año, g.Key, 1),
                    Duracion = g.Count()
                })
                .OrderBy(r => r.Fecha)
                .ToList();

            return clientesPorMes;
        }

        public List<DTCliente> ObtenerClientesEnEsperaPorOficinaDTO(int oficinaId)
        {
            var oficina = _contexto.Oficinas
                .Include(o => o.Clientes)
                .AsNoTracking() // Desactiva el seguimiento para evitar caché
                .FirstOrDefault(o => o.Id == oficinaId);

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

        public List<DTCliente> ObtenerClientesProcesandoPorOficinaDTO(int oficinaId)
        {
            var oficina = _contexto.Oficinas
                .Include(o => o.Clientes)
                .AsNoTracking() // Desactiva el seguimiento para evitar caché
                .FirstOrDefault(o => o.Id == oficinaId);

            if (oficina == null)
            {
                return new List<DTCliente>();
            }

            return oficina.Clientes
                .Where(c => c.Estado == EstadoCliente.Procesando)
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

        public void RegistrarClienteEnOficina(int oficinaId, DTCliente nuevoCliente)
        {
            try
            {
                var oficina = _contexto.Oficinas
                    .Include(o => o.Clientes)
                    .FirstOrDefault(o => o.Id == oficinaId);

                if (oficina == null)
                {
                    throw new Exception($"La oficina con ID {oficinaId} no existe.");
                }

                // Verificar si el cliente ya está en estado "Esperando"
                var clienteExistente = oficina.Clientes.FirstOrDefault(c => c.Cedula == nuevoCliente.Cedula && c.Estado == EstadoCliente.Esperando);

                if (clienteExistente != null)
                {
                    throw new InvalidOperationException($"El cliente con cédula {nuevoCliente.Cedula} ya está en espera en la oficina.");
                }

                // Crear y agregar nuevo cliente si no existe en espera
                var cliente = new Cliente
                {
                    Cedula = nuevoCliente.Cedula,
                    FechaRegistro = nuevoCliente.FechaRegistro,
                    Estado = nuevoCliente.Estado
                };

                oficina.Clientes.Add(cliente);
                _contexto.SaveChanges();
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otra excepción
                throw new Exception("Ocurrió un error al registrar el cliente en la oficina: " + ex.Message);
            }
        }


        public void AtenderCliente(int clienteId, int operarioId, int oficinaId, DateTime fecha, int duracion)
        {
            var oficina = _contexto.Oficinas
                .Include(o => o.Clientes)
                .Include(o => o.Operarios)
                .FirstOrDefault(o => o.Id == oficinaId);

            if (oficina == null)
            {
                throw new Exception($"Oficina con ID {oficinaId} no encontrada.");
            }

            var cliente = oficina.Clientes.FirstOrDefault(c => c.Id == clienteId);
            if (cliente == null)
            {
                throw new Exception($"Cliente con ID {clienteId} no encontrado en la oficina ID {oficinaId}.");
            }

            var operario = oficina.Operarios.FirstOrDefault(o => o.Id == operarioId);
            if (operario == null)
            {
                throw new Exception($"Operario con ID {operarioId} no encontrado en la oficina ID {oficinaId}.");
            }

            var atencion = new RegistroDeAtencion
            {
                ClienteId = cliente.Id,
                OperarioId = operario.Id,
                OficinaId = oficina.Id,
                Fecha = fecha,
                DuracionAtencion = duracion
            };

            cliente.Estado = EstadoCliente.Atendido;

            _contexto.RegistrosDeAtencion.Add(atencion);
            _contexto.SaveChanges();
        }
    }
}
