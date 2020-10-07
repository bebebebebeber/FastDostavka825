using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastDostavka.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string userId, string message)
        {           
            await Clients.User("2").SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
        }
    }
}
