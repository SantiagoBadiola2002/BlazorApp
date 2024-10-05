namespace BlazorApp.Models
{
    public class Oficina
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } 
        public List<Operario> Operarios { get; set; } 
        public List<Cliente> ClientesEnEspera { get; set; } 

        public Oficina()
        {
            Operarios = new List<Operario>();
            ClientesEnEspera = new List<Cliente>();
        }

        public void AgregarCliente(Cliente cliente)
        {
            ClientesEnEspera.Add(cliente);
            // Lógica para notificar a los monitores sobre el nuevo cliente
        }

        public void ActualizarMonitores()
        {
            // Lógica para actualizar la información en los monitores
        }
    }

}
