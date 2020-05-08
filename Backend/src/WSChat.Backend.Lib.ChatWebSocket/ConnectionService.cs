using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace WSChat.Backend.Lib.ChatWebSocket
{
    public class ConnectionService
    {
        private ConcurrentDictionary<string, WebSocket> _connections = new ConcurrentDictionary<string, WebSocket>();

        public ConcurrentDictionary<string, WebSocket> GetAllConnections()
        {
            return _connections;
        }

        private string GenerateConnectionId()
        {
            return Guid.NewGuid().ToString("N");
        }

        public void AddSocket(WebSocket socket)
        {
            _connections.TryAdd(GenerateConnectionId(), socket);
        }

        public async Task RemoveSocketAsync(string id)
        {
            _connections.TryRemove(id, out var socket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "socket connection closed", CancellationToken.None);
        }

        public WebSocket GetSocketById(string id)
        {
            return _connections.FirstOrDefault(x => x.Key == id).Value;
        }

        public string GetId(IDisposable socket)
        {
            return _connections.FirstOrDefault(x => x.Value == (WebSocket)socket).Key;
        }
    }
}
