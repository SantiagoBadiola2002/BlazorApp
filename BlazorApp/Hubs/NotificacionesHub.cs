using Microsoft.AspNetCore.SignalR;

namespace BlazorApp.Hubs
{
    public class NotificacionesHub : Hub
    {

        public async Task NotificarClienteAtendido(int clienteId)
        {
            // Enviar la notificación a todos los clientes conectados
            await Clients.All.SendAsync("ClienteAtendido", clienteId);
        }
    }
}
