using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualShopping.API.SignalR;

namespace VirtualShopping.SignalR
{
    public class ShopsHub : Hub
    {
        private readonly GroupsTracker _tracker;

        public ShopsHub(GroupsTracker tracker)
        {
            _tracker = tracker;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var shopId = httpContext.Request.Query["shop"].ToString();
            await _tracker.UserConnectedToShop(shopId, Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            var shopId = httpContext.Request.Query["shop"].ToString();
            await _tracker.UserDisconnectedToShop(shopId, Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
