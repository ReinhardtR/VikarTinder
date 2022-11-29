namespace WebSockets;

public class JoinChatMessage
{
    public string MessageType { get; set; }
    public int ChatId { get; set; }
        
    public JoinChatMessage(int chatId)
    {
        ChatId = chatId;
        MessageType = "JOIN";
    }
}