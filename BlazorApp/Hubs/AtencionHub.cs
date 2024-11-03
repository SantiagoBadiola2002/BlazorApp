﻿using Microsoft.AspNetCore.SignalR;

namespace BlazorApp.Hubs
{
    public class Atencionhub : Hub
    {
        public async Task NotifyClients()
        {
            await Clients.All.SendAsync("ReceiveMessage", "Los datos han sido actualizados.");
        }
    }
}