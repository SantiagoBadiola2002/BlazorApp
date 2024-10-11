using BlazorApp.Models;

namespace BlazorApp.Infraestructure.Data
{
    public class OficinaRepository : IOficinaRepository
    {
        private readonly List<Oficina> _oficinas;
        private readonly IOperarioRepository _operarioRepository;
        private readonly IClienteRepository _clienteRepository;

        public OficinaRepository(IOperarioRepository operarioRepository, IClienteRepository clienteRepository)
        {
            _operarioRepository = operarioRepository;
            _clienteRepository = clienteRepository;

            _oficinas = new List<Oficina>
            {
                CrearOficina1()
            };

            MostrarDatosOficina(_oficinas.FirstOrDefault());
        }

        public Oficina ObtenerOficinaPorId(int id)
        {
            return _oficinas.FirstOrDefault(o => o.Id == id);
        }

        public List<Oficina> ObtenerTodasLasOficinas()
        {
            return _oficinas;
        }

        public void AgregarOficina(Oficina oficina)
        {
            // Generar un nuevo Id único para la nueva oficina
            oficina.Id = _oficinas.Any() ? _oficinas.Max(o => o.Id) + 1 : 1;
            _oficinas.Add(oficina);
        }

        public void ActualizarOficina(Oficina oficina)
        {
            var oficinaExistente = ObtenerOficinaPorId(oficina.Id);
            if (oficinaExistente != null)
            {
                oficinaExistente.Nombre = oficina.Nombre;
                oficinaExistente.PuestosDeAtencion = oficina.PuestosDeAtencion;
                oficinaExistente.ClientesEnEspera = oficina.ClientesEnEspera;
                oficinaExistente.Operarios = oficina.Operarios;
            }
        }

        public void EliminarOficina(int id)
        {
            var oficina = ObtenerOficinaPorId(id);
            if (oficina != null)
            {
                _oficinas.Remove(oficina);
            }
        }

        private Oficina CrearOficina1()
        {
            // Obtenemos los operarios y clientes desde los repositorios
            List<Operario> operarios = _operarioRepository.GetAllOperarios().ToList();
            List<Cliente> clientesEnEspera = _clienteRepository.ObtenerClientesEnEspera().ToList();

            // Crear los puestos de atención (pueden estar también en un repositorio si quieres separarlo)
            List<PuestoAtencion> puestosAtencion = new List<PuestoAtencion>
            {
                new PuestoAtencion(111, 1, true, operarios[0].Id, 1),
                new PuestoAtencion(222, 2, true, operarios[1].Id, 1),
                new PuestoAtencion(333, 3, true, operarios[2].Id, 1)
            };

            return new Oficina
            {
                Id = 1,
                Nombre = "Oficina 1",
                Operarios = operarios,
                ClientesEnEspera = clientesEnEspera,
                PuestosDeAtencion = puestosAtencion
            };
        }

        private void MostrarDatosOficina(Oficina oficina)
        {
            if (oficina != null)
            {
                Console.WriteLine($"Oficina ID: {oficina.Id}, Nombre: {oficina.Nombre}");

                Console.WriteLine("Puestos de atención:");
                foreach (var puesto in oficina.PuestosDeAtencion)
                {
                    Console.WriteLine($"  Puesto ID: {puesto.Id}, Número: {puesto.Numero}, EstaLibre: {puesto.EstaLibre}, OperarioAsignado: {puesto.IdOperarioAsignado}");
                }

                Console.WriteLine("Operarios:");
                foreach (var operario in oficina.Operarios)
                {
                    Console.WriteLine($"  Operario ID: {operario.Id}, Nombre: {operario.Nombre}, EstaDisponible: {operario.EstaDisponible}");
                }

                Console.WriteLine("Clientes en espera:");
                foreach (var cliente in oficina.ClientesEnEspera)
                {
                    Console.WriteLine($"  Cliente Cédula: {cliente.Cedula}, FechaRegistro: {cliente.FechaRegistro.ToShortDateString()}, Estado: {cliente.Estado}");
                }
            }
            else
            {
                Console.WriteLine("No se encontró ninguna oficina.");
            }
        }

    }
}
