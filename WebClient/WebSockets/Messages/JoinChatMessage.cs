using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebSockets;

public class JoinChatMessage
{

    [JsonConverter(typeof(StringEnumConverter))]
    public MessageType MessageType { get; set; }
    public int ChatId { get; set; }
        
    public JoinChatMessage(int chatId)
    {
        ChatId = chatId;
        MessageType = MessageType.JOIN_CHAT;
    }
}