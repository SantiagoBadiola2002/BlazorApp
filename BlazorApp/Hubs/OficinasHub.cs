using BlazorApp.Models.DTs;
using Microsoft.AspNetCore.SignalR;

public class OficinasHub : Hub
{
    public async Task EnviarOficinasActualizadas(List<DTOficina> oficinas)
    {
        await Clients.All.SendAsync("ReceiveOficinas", oficinas);
    }
}