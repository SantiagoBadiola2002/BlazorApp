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
    }
}
