using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using HttpClients;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebSockets;

public class ChatSocket
{
    private const string BaseAddress = "ws://localhost:8080";
    private readonly ClientWebSocket _webSocket;
    private readonly Uri _webSocketUrl;

    public ChatSocket()
    {
        _webSocket = new ClientWebSocket();
        _webSocketUrl = new Uri($"{BaseAddress}/ws");
    }

    public async IAsyncEnumerable<MessageDTO> ConnectAsync(int chatId, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await _webSocket.ConnectAsync(_webSocketUrl, CancellationToken.None);
        await JoinChatAsync(chatId);

        ArraySegment<byte> buffer = new(new byte[2048]);
        
        while (!cancellationToken.IsCancellationRequested)
        {
            WebSocketReceiveResult result;
            MemoryStream ms = new();
            
            do
            {
                result = await _webSocket.ReceiveAsync(buffer, cancellationToken);
                ms.Write(buffer.Array, buffer.Offset, result.Count);
            } while (!result.EndOfMessage);

            ms.Seek(0, SeekOrigin.Begin);

            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            
            MessageDTO? message = JsonConvert.DeserializeObject<MessageDTO>(jsonString);
            
            if (message != null)
            {
                yield return message;
            }

            if (result.MessageType == WebSocketMessageType.Close)
                break;
        }
    }

    private async Task JoinChatAsync(int chatId)
    {
        JoinChatMessage joinChatMessage = new(chatId);
        
        string jsonString = JsonSerializer.Serialize(joinChatMessage);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonString);
        await _webSocket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
    }
}