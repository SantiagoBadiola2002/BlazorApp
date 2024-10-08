using BlazorApp.Models;

namespace BlazorApp.Data
{
    public class OficinaRepository : IOficinaRepository
    {
        private readonly List<Oficina> _oficinas;

        public OficinaRepository()
        {
            _oficinas = new List<Oficina>
        {
            CrearOficina1()
        };

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
            // Crear los puestos de atención. PuestoAtencion -> (Id, Numero, EstaLibre, IdOperarioAsignado, OficinaId)
            List<PuestoAtencion> puestosAtencion = new List<PuestoAtencion>
        {
            new PuestoAtencion(111, 1, true, 1, 1),
            new PuestoAtencion(222, 2, true, 2, 1),
            new PuestoAtencion(333, 3, true, 3, 1)
        };

            // Crear los operarios directamente vinculados a sus puestos. Operario -> ( Id, Nombre, PuestoAsignado, EstaDisponible, OficinaId)
            List<Operario> operarios = new List<Operario>
        {
            new Operario(1, "Operario 1", puestosAtencion[0], true, 1),
            new Operario(2, "Operario 2", puestosAtencion[1], true, 1),
            new Operario(3, "Operario 3", puestosAtencion[2], true, 1)
        };

            // Crear los clientes en espera con la fecha actual si no es necesario pasar una específica. Cliente -> (Cedula, FechaRegistro, Estado)
            List<Cliente> clientesEnEspera = new List<Cliente>
        {
            new Cliente("111111", DateTime.Today, EstadoCliente.Esperando),
            new Cliente("222222", DateTime.Today, EstadoCliente.Esperando),
            new Cliente("333333", DateTime.Today, EstadoCliente.Esperando)
        };

            // Crear la oficina con sus operarios, clientes y puestos
            return new Oficina
            {
                Id = 1,
                Nombre = "Oficina 1",
                Operarios = operarios,
                ClientesEnEspera = clientesEnEspera,
                PuestosDeAtencion = puestosAtencion
            };
        }
    }
}
