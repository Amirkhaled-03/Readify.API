using Readify.BLL.Features.Chat.Services;
using Readify.BLL.Features.JWTToken;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text;

public class WebSocketChatMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly ConcurrentDictionary<string, WebSocket> _userSockets = new();
    public WebSocketChatMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var tokenService = context.RequestServices.GetService(typeof(ITokenService)) as ITokenService;

        if (context.Request.Path.StartsWithSegments("/ws/chat") && context.WebSockets.IsWebSocketRequest)
        {
            var userId = tokenService.GetUserIdFromToken(); // Extract from token or query
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();

            _userSockets[userId] = webSocket;

            await ReceiveMessagesAsync(userId, webSocket, context);
        }
        else
        {
            await _next(context); // If not WebSocket, pass request to the next middleware
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
                var recipientId = chatMessage.SenderType == UserType.User ? chatMessage.LibrarianId : chatMessage.UserId;

                // Forward message to recipient if connected
                if (_userSockets.TryGetValue(recipientId, out var recipientSocket))
                {
                    var responseJson = JsonSerializer.Serialize(chatMessage);
                    var bytes = Encoding.UTF8.GetBytes(responseJson);

                    await recipientSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine($"Error handling WebSocket message: {ex.Message}");
        }
        finally
        {
            _userSockets.TryRemove(userId, out _);
            await socket.CloseAsync(WebSocketCloseStatus.InternalServerError, "Error occurred", CancellationToken.None);
        }
    }
}
