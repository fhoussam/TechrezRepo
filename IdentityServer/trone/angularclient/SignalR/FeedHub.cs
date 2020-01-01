using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angularclient.SignalR
{
    public class FeedHub : Hub
    {
        public async Task BroadcastFeedData(string data) 
        {
            await Clients.All.SendAsync("broadcastfeeddata", data);
        }
    }
}
