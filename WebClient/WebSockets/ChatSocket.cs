using System.Net.WebSockets;
using System.Text;
using HttpClients;
using Newtonsoft.Json;

namespace WebSockets;

public class ChatSocket
{
    private const string BaseAddress = "ws://localhost:8080";
    private readonly ClientWebSocket _webSocket;
    private readonly Uri _webSocketUrl;
    
    private int? _currentChatId;

    private readonly Queue<MessageDTO> _messages;
    private readonly Queue<JobConfirmationDTO> _jobConfirmations;
 
    public ChatSocket()
    {
        _webSocket = new ClientWebSocket();
        _webSocketUrl = new Uri($"{BaseAddress}/ws");
        _messages = new Queue<MessageDTO>();
        _jobConfirmations = new Queue<JobConfirmationDTO>();
    }
    
    public async IAsyncEnumerable<MessageDTO> ListenToChatMessages()
    {
        while (true)
        {
            if (_messages.Count > 0)
            {
                yield return _messages.Dequeue();
            }
            else
            {
                await Task.Delay(100);
            }
        }
    }
    
    public async IAsyncEnumerable<JobConfirmationDTO> ListenToJobConfirmations()
    {
        while (true)
        {
            if (_jobConfirmations.Count > 0)
            {
                yield return _jobConfirmations.Dequeue();
            }
            else
            {
                await Task.Delay(100);
            }
        }
    }

    public async Task ConnectAsync(int chatId)
    {
        if (!IsConnected())
        {
            Console.WriteLine("NEW CONNECTION");
            await _webSocket.ConnectAsync(_webSocketUrl, CancellationToken.None);
            await JoinChatAsync(chatId);
        } 
        else if (IsConnected() && _currentChatId != chatId)
        {
            Console.WriteLine("NEW CHAT");
            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Switching chats", CancellationToken.None);
            await _webSocket.ConnectAsync(_webSocketUrl, CancellationToken.None);
            await JoinChatAsync(chatId);
        } 
        else if (IsConnected() && _currentChatId == chatId)
        {
            Console.WriteLine("ALREADY CONNECTED");
            return;
        }

        ListenToMessages();
    }

    private async void ListenToMessages()
    {
        while (_webSocket.State == WebSocketState.Open)
        {
            string? newMessageText = await GetNewMessageAsync();
            if (newMessageText == null) continue;
            
            MessageWrapper? wrapper = JsonConvert.DeserializeObject<MessageWrapper>(newMessageText);
            if (wrapper == null) continue;
            
            switch (wrapper.MessageType)
            {
                case MessageType.JOB_CONFIRMATION_UPDATE:
                {
                    JobConfirmationDTO? jobConfirmationDto = JsonConvert.DeserializeObject<JobConfirmationDTO>(wrapper.Data.ToString());
                    if (jobConfirmationDto != null) _jobConfirmations.Enqueue(jobConfirmationDto);
                    break;
                }
                case MessageType.CHAT_MESSAGE:
                {
                    MessageDTO? messageDto = JsonConvert.DeserializeObject<MessageDTO>(wrapper.Data.ToString());
                    if (messageDto != null) _messages.Enqueue(messageDto);
                    break;
                }
            }
        }
        
        _currentChatId = null;
    }
    
    private bool IsConnected()
    {
        return _webSocket.State is WebSocketState.Open or WebSocketState.Connecting;
    }
    
    private async Task JoinChatAsync(int chatId)
    {
        if (_currentChatId == chatId) return;
        if (!IsConnected()) throw new Exception("You must connect to the socket before joining a chat");
        
        JoinChatMessage joinChatMessage = new(chatId);
        
        string jsonString = JsonConvert.SerializeObject(joinChatMessage);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonString);
        await _webSocket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
        _currentChatId = chatId;
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
}