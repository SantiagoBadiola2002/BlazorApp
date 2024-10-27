using Microsoft.AspNetCore.SignalR;

namespace BlazorApp.Hubs
{
    public class NotificacionesHub : Hub
    {

        public async Task NotificarLlamadoCliente(int clienteId, String operarioNombre)
        {
            // Enviar la notificación a todos los clientes conectados
            Console.WriteLine("### NotificacionHub. clienteId: " + clienteId + ", operarioNombre: " + operarioNombre);
            await Clients.All.SendAsync("LlamandoCliente", clienteId, operarioNombre);
        }

        public async Task RefrescarPaginas()
        {
            // Enviar una notificación de refresco a todos los clientes conectados
            Console.WriteLine("### NotificacionHub. Refrescando todas las páginas");
            await Clients.All.SendAsync("RefrescarPagina");
        }

        public async Task RefrescarPaginaAtencionCliente()
        {
            // Enviar una notificación de refresco a todos los clientes conectados
            Console.WriteLine("### NotificacionHub. Refrescando pagina AtencionCliente");
            await Clients.All.SendAsync("RefrescarPaginaAtencionCliente");
        }

        public async Task ActualizarClientesEnEspera()
        {
            Console.WriteLine("### NotificacionHub. Actualizando lista de clientes en espera");
            await Clients.All.SendAsync("ActualizarListaClientesEnEspera");
        }

    }
}
