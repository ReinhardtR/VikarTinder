using Google.Protobuf.Collections;
using Persistence.Models;

namespace Persistence.Services;

public class ChatServiceFactory
{
    public static SendMessageResponse ToSendMessageResponse(Message message)
    {
        return new SendMessageResponse
        { Message = new MessageObject()
            {
            Id = message.Id,
            AuthorId = message.AuthorId,
            ChatId = message.ChatId,
            Content = message.Content,
            }
        };
    }

    public static CreateChatResponse ToCreateChatResponse(Chat chat)
    {
            return new CreateChatResponse
        {
            Id = chat.Id
        };
    }

    public static GetChatOverviewResponse ToGetChatOverviewResponse(List<Chat> chats)
    {
        RepeatedField<ChatOverviewObject> chatOverviewObjects = new RepeatedField<ChatOverviewObject>();

        foreach (Chat chat in chats)
        {
            RepeatedField<int> userIds = new RepeatedField<int>();
            
            foreach (User participant in chat.Participants)
            {
                userIds.Add(participant.Id);
            }

            chatOverviewObjects.Add(new ChatOverviewObject()
            {
                Id = chat.Id,
                UserIds = { userIds }
            });
        }
        
        GetChatOverviewResponse getChatOverviewResponse = new()
        {
            Chats = { chatOverviewObjects }
        };
        return getChatOverviewResponse;
    }

    public static GetChatHistoryResponse ToGetChatHistoryResponse(List<Message> messages)
    {
        IEnumerable<MessageObject> messageObjects = messages.Select((m) => new MessageObject
        {
            Id = m.Id,
            Content = m.Content,
            AuthorId = m.AuthorId,
            ChatId = m.ChatId
        });

        GetChatHistoryResponse getChatHistoryResponse = new GetChatHistoryResponse()
        {
            Messages = { messageObjects }
        };

        return getChatHistoryResponse;
    }
    
}