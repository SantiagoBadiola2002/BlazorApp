using BlazorApp.Models;
using BlazorApp.Models.BaseDeDatos;
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

        // Obtiene una oficina por su ID
        public Oficina ObtenerOficinaPorId(int id)
        {
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return _contexto.Oficinas
                            .Include(o => o.Operarios)
                            .Include(o => o.Clientes)
                            .Include(o => o.PuestosDeAtencion)
                            .FirstOrDefault(o => o.Id == id);
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        // Obtiene todas las oficinas
        public async Task<List<Oficina>> ObtenerTodasLasOficinasAsync()
        {
            Console.WriteLine("#### Devolviendo todas las oficinas ####");
            return await _contexto.Oficinas
                            .Include(o => o.Operarios)
                            .Include(o => o.Clientes)
                            .Include(o => o.PuestosDeAtencion)
                            .ToListAsync();
        }


        // Agrega una nueva oficina
        public void AgregarOficina(Oficina oficina)
        {
            _contexto.Oficinas.Add(oficina);
            _contexto.SaveChanges();
        }

        // Actualiza una oficina existente
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

        // Elimina una oficina por su ID
        public void EliminarOficina(int id)
        {
            var oficina = _contexto.Oficinas.FirstOrDefault(o => o.Id == id);
            if (oficina != null)
            {
                _contexto.Oficinas.Remove(oficina);
                _contexto.SaveChanges();
            }
        }

        public async Task<List<Cliente>> ObtenerClientesEnEsperaPorOficinaAsync(int oficinaId)
        {
            try
            {
                // Buscar la oficina por su ID
                var oficina = await _contexto.Oficinas
                    .Include(o => o.Clientes) // Incluye clientes en la consulta
                    .FirstOrDefaultAsync(o => o.Id == oficinaId);

                // Si no se encuentra la oficina, retornamos una lista vacía
                if (oficina == null)
                {
                    return new List<Cliente>();
                }

                // Filtrar los clientes de la oficina que están en estado de espera y ordenarlos por fecha de registro
                return oficina.Clientes
                    .Where(c => c.Estado == EstadoCliente.Esperando)
                    .OrderBy(c => c.FechaRegistro) // Ordenar por fecha de menor a mayor
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener clientes en espera para la oficina {oficinaId}: {ex.Message}");
                return new List<Cliente>();
            }
        }


    }
}
