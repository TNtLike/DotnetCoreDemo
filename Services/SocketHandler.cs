
using System;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Threading;
namespace MyWebApi.Services
{
    public class SocketHandler
    {
        public const int KeepAlive = 120;
        public const int BufferSize = 4096;
        public WebSocket socket;
        public SocketHandler(WebSocket socket)
        {
            this.socket = socket;
        }
        public async Task Echo()
        {
            var buffer = new byte[BufferSize];
            var seg = new ArraySegment<byte>(buffer);
            while (this.socket.State == WebSocketState.Open)
            {
                var incoming = await this.socket.ReceiveAsync(seg, CancellationToken.None);
                var outgoing = new ArraySegment<byte>(buffer, 0, incoming.Count);
                await this.socket.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None);
                // await this.socket.CloseAsync(incoming.CloseStatus.Value, incoming.CloseStatusDescription, CancellationToken.None);
            }
        }

        public static async Task Acceptor(HttpContext context, Func<Task> next)
        {
            if (!context.WebSockets.IsWebSocketRequest)
                return;
            var socket = await context.WebSockets.AcceptWebSocketAsync();
            var h = new SocketHandler(socket);
            await h.Echo();
        }

        /// <summary>
        /// branches the request pipeline for this SocketHandler usage
        /// </summary>
        /// <param name="app"></param>
        public static void Map(IApplicationBuilder app)
        {

            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = 4 * 1024
            };
            webSocketOptions.AllowedOrigins.Add("http://127.0.0.1:5500");
            app.UseWebSockets(webSocketOptions);
            app.Use(SocketHandler.Acceptor);
        }
    }
}
