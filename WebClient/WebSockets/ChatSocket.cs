using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using HttpClients;
using Newtonsoft.Json;

namespace WebSockets;

public class ChatSocket
{
    private const string BaseAddress = "ws://localhost:8080";
    private readonly ClientWebSocket _webSocket;
    private readonly Uri _webSocketUrl;
    private bool _isConnected;
    private bool _hasJoinedChat;
    
    public ChatSocket()
    {
        _webSocket = new ClientWebSocket();
        _webSocketUrl = new Uri($"{BaseAddress}/ws");
    }

    public async IAsyncEnumerable<MessageDTO> ListenToChatMessages(int chatId)
    {
        await ConnectAsync();
        await JoinChatAsync(chatId);
        
        while (_webSocket.State == WebSocketState.Open)
        {
            var buffer = new byte[1024 * 4];
            var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            yield return JsonConvert.DeserializeObject<MessageDTO>(message);
        }
    }

    public async IAsyncEnumerable<Object> ListenToJobConfirmation(int chatId, int jobConfirmationId)
    {
        
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
    
    public async IAsyncEnumerable<Object> ListenToJobConfirmation(int chatId, int jobConfirmationId)
    {
        await _webSocket.ConnectAsync(_webSocketUrl, CancellationToken.None);
        await JoinChatAsync(jobConfirmationId);

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
            
            Object? message = JsonConvert.DeserializeObject<Object>(jsonString);
            
            if (message != null)
            {
                yield return message;
            }

            if (result.MessageType == WebSocketMessageType.Close)
                break;
        }
    }

    private async Task ConnectAsync()
    {
        if (_isConnected) return;
        
        await _webSocket.ConnectAsync(_webSocketUrl, CancellationToken.None);
        _isConnected = true;
    }
    
    private async Task JoinChatAsync(int chatId)
    {
        if (_hasJoinedChat) return;
        if (!_isConnected) throw new Exception("You must connect to the socket before joining a chat");
        
        JoinChatMessage joinChatMessage = new(chatId);
        
        string jsonString = JsonConvert.SerializeObject(joinChatMessage);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonString);
        await _webSocket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
        _hasJoinedChat = true;
    }
}