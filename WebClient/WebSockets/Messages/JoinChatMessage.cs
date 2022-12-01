namespace WebSockets;

public class JoinChatMessage
{
    public MessageType MessageType { get; set; }
    public int ChatId { get; set; }
        
    public JoinChatMessage(int chatId)
    {
        ChatId = chatId;
        MessageType = MessageType.JOIN_CHAT;
    }
}