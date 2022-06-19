using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualShopping.API.SignalR;

namespace VirtualShopping.SignalR
{
    public class OrdersHub : Hub
    {
        private readonly GroupsTracker _tracker;

        public OrdersHub(GroupsTracker tracker)
        {
            _tracker = tracker;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var orderId = httpContext.Request.Query["order"].ToString();
            await _tracker.UserConnectedToOrder(orderId, Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            var orderId = httpContext.Request.Query["order"].ToString();
            await _tracker.UserDisconnectedToOrder(orderId, Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
