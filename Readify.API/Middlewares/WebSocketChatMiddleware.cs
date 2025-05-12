using Readify.BLL.Features.Chat.Services;
using Readify.BLL.Features.JWTToken;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text;

public class WebSocketChatMiddleware
{
    private class WebSocketConnection
    {
        public string UserId { get; set; }
        public UserType UserType { get; set; }
        public WebSocket Socket { get; set; }
    }

    private static readonly ConcurrentDictionary<string, WebSocketConnection> _userSockets = new();

    private readonly RequestDelegate _next;
    //private static readonly ConcurrentDictionary<string, WebSocket> _userSockets = new();
    public WebSocketChatMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var tokenService = context.RequestServices.GetService(typeof(ITokenService)) as ITokenService;

        if (context.Request.Path.StartsWithSegments("/ws/chat") && context.WebSockets.IsWebSocketRequest)
        {
            var userId = context.Request.Query["userId"];
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            var userType = Enum.Parse<UserType>(context.Request.Query["userType"]);

            _userSockets[userId] = new WebSocketConnection
            {
                UserId = userId,
                UserType = userType,
                Socket = webSocket
            };


            //_userSockets[userId] = webSocket;

            await ReceiveMessagesAsync(userId, webSocket, context);
        }
        else
        {
            await _next(context);
        }
    }

    private async Task ReceiveMessagesAsync(string userId, WebSocket socket, HttpContext context)
    {
        var chatService = context.RequestServices.GetService(typeof(IChatService)) as IChatService;
        var buffer = new byte[1024 * 4];

        try
        {
            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                // Check for end-of-message signal
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
                    break;
                }

                var messageJson = Encoding.UTF8.GetString(buffer, 0, result.Count);
                var chatMessage = JsonSerializer.Deserialize<IncomingMessageDto>(messageJson);

                await chatService.AddMessageAsync(chatMessage);
                var responseJson = JsonSerializer.Serialize(chatMessage);
                var bytes = Encoding.UTF8.GetBytes(responseJson);

                if (chatMessage.SenderType == UserType.User)
                {
                    // Broadcast to all connected librarians
                    foreach (var connection in _userSockets.Values)
                    {
                        if (connection.UserType == UserType.Librarian && connection.Socket.State == WebSocketState.Open)
                        {
                            await connection.Socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                    }
                }
                else
                {
                    // Librarian sends to specific user
                    var recipientId = chatMessage.UserId;
                    if (_userSockets.TryGetValue(recipientId, out var recipientConnection) &&
                        recipientConnection.Socket.State == WebSocketState.Open)
                    {
                        await recipientConnection.Socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error handling WebSocket message: {ex.Message}");
        }
        finally
        {
            _userSockets.TryRemove(userId, out _);
            await socket.CloseAsync(WebSocketCloseStatus.InternalServerError, "Error occurred", CancellationToken.None);
        }
    }
}
