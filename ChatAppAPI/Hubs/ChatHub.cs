using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatAppAPI.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppAPI.Hubs
{
    public class ChatHub: Hub
    {
        public async Task SendMessage(string user, string Message)
        {
            try
            {
                await Clients.All.SendAsync("MessageRecieved", user, Message);
            }catch(Exception ex)
            {
                await Clients.All.SendAsync("JoinChat",  ex.Message);
            }
        }
        public async Task JoinChat(string user)
        {
            await Clients.All.SendAsync("JoinChat", user);
        }

        public async Task LeaveChat(string user)
        {
            await Clients.All.SendAsync("LeaveChat", user);
        }
    }
}
