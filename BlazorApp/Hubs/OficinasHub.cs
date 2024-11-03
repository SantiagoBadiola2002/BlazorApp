using BlazorApp.Models.DTs;
using Microsoft.AspNetCore.SignalR;

namespace BlazorApp.Hubs
{
    public class OficinasHub : Hub
    {
        public void EnviarOficinasActualizadas()
        {
            Console.WriteLine("### NotificacionHub. Refrescando Flujo Cliente");
            Clients.All.SendAsync("ReceiveOficinas").GetAwaiter().GetResult();
        }
    }

}
