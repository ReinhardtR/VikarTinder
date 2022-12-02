namespace WebSockets;

public class MessageWrapper
{
    public MessageType MessageType { get; set; }
    public dynamic Data { get; set; }
    
    public MessageWrapper(MessageType messageType, dynamic data)
    {
        MessageType = messageType;
        Data = data;
    }
}