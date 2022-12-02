using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
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
            string? newMessageText = await GetNewMessageAsync();
            if (newMessageText == null) continue;
            
            MessageDTO? messageDto = JsonConvert.DeserializeObject<MessageDTO>(newMessageText);
            if (messageDto != null) yield return messageDto;
        }
    }

    public async IAsyncEnumerable<JobConfirmationDTO> ListenToJobConfirmation(int chatId)
    {
        await ConnectAsync();
        await JoinChatAsync(chatId);
        
        while (_webSocket.State == WebSocketState.Open)
        {
            string? newMessageText = await GetNewMessageAsync();
            if (newMessageText == null) continue;
            
            JobConfirmationDTO? jobConfirmationDto = JsonConvert.DeserializeObject<JobConfirmationDTO>(newMessageText);
            if (jobConfirmationDto != null) yield return jobConfirmationDto;
        }
    }

    private async Task<string?> GetNewMessageAsync()
    {
        ArraySegment<byte> buffer = new(new byte[8192]);

        WebSocketReceiveResult? result;

        using MemoryStream ms = new();
        
        do
        {
            result = await _webSocket.ReceiveAsync(buffer, CancellationToken.None);
            ms.Write(buffer.Array, buffer.Offset, result.Count);
        }
        while (!result.EndOfMessage);

        ms.Seek(0, SeekOrigin.Begin);

        if (result.MessageType == WebSocketMessageType.Text)
        {
            return Encoding.UTF8.GetString(ms.ToArray());
        }
        
        return null;
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