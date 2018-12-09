using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace DostDestekSample.Hubs
{
    public class PostHub:Hub
    {
        public static ConcurrentDictionary<string, string> ConnectedUsers = new ConcurrentDictionary<string, string>();
       
        public override Task OnConnected()
        {
            Clients.Caller.Test("Merhaba");
            return base.OnConnected();
        }
        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var userName = string.Empty;
            ConnectedUsers.TryRemove(Context.ConnectionId, out userName);
            return base.OnDisconnected(stopCalled);
        }
        public void Join(string userName)
        {
            ConnectedUsers.AddOrUpdate(Context.ConnectionId, userName, (k,v)=>userName);
            Clients.Caller.Test("Giris Basarili");
        }
        public void ReceiveMessage(string message, string to)
        {
            var ConnectionId = ConnectedUsers.FirstOrDefault(u => u.Value == to);
            if (ConnectionId.Key != null)
            {
                Clients.Client(ConnectionId.Key).Test(message);
            }
            
        }
    }
}