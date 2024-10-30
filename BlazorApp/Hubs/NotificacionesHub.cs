using Microsoft.AspNetCore.SignalR;

namespace BlazorApp.Hubs
{
    public class NotificacionesHub : Hub
    {
        public void NotificarLlamadoCliente(int clienteId, string operarioNombre)
        {
            Console.WriteLine("### NotificacionHub. clienteId: " + clienteId + ", operarioNombre: " + operarioNombre);
            Clients.All.SendAsync("LlamandoCliente", clienteId, operarioNombre).GetAwaiter().GetResult();
        }

        public void RefrescarPaginas(int clienteId)
        {
            Console.WriteLine("### NotificacionHub. Refrescando todas las páginas");
            Clients.All.SendAsync("RefrescarPagina").GetAwaiter().GetResult();
        }

        public void RefrescarPaginaAtencionCliente()
        {
            Console.WriteLine("### NotificacionHub. Refrescando pagina AtencionCliente");
            Clients.All.SendAsync("RefrescarPaginaAtencionCliente").GetAwaiter().GetResult();
        }

        public void ActualizarClientesEnEspera()
        {
            Console.WriteLine("### NotificacionHub. Actualizando lista de clientes en espera");
            Clients.All.SendAsync("ActualizarListaClientesEnEspera").GetAwaiter().GetResult();
        }
    }
}
