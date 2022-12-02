using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Persistence.Models;

namespace Persistence.Services;

public class ChatServiceFactory
{
    

    public static SendMessageResponse ToSendMessageResponse(Message message)
    {
        return new SendMessageResponse
        { 
            Message = new MessageObject()
            {
                Id = message.Id,
                Author = ToChatUserObject(message.Author),
                ChatId = message.ChatId,
                Content = message.Content,
                CreatedAt = message.CreatedAt.ToTimestamp()
            }
        };
    }

    public static CreateChatResponse ToCreateChatResponse(Chat chat)
    {
            return new CreateChatResponse
        {
            Id = chat.Id,
            Employer = ToChatUserObject(new User() {Id = chat.EmployerId}),
            Substitute = ToChatUserObject(new User(){Id = chat.SubstituteId})
        };
    }

    public static GetChatOverviewResponse ToGetChatOverviewResponse(List<Chat> chats)
    {
        RepeatedField<ChatOverviewObject> chatOverviewObjects = new RepeatedField<ChatOverviewObject>();

        foreach (Chat chat in chats)
        {
            chatOverviewObjects.Add(new ChatOverviewObject()
            {
                Id = chat.Id,
                Employer = ToChatUserObject(new User() {Id = chat.EmployerId}),
                Substitute = ToChatUserObject(new User(){Id = chat.SubstituteId}) //skal ændres når vi merger usecases
            });
        }
        
        GetChatOverviewResponse getChatOverviewResponse = new()
        {
            Chats = { chatOverviewObjects }
        };
        return getChatOverviewResponse;
    }

   
    
    private static ChatUserObject ToChatUserObject(User user)
    {
        return new ChatUserObject()
        {
            Id = user.Id
        };
    }

    public static GetChatHistoryResponse ToGetChatHistoryResponse(Chat chat)
    {
        RepeatedField<MessageObject> messageObjects = new RepeatedField<MessageObject>();
        foreach (Message message in chat.Messages)
        {
            messageObjects.Add(new MessageObject()
            {
                Id = message.Id,
                Author = ToChatUserObject(message.Author),
                ChatId = message.ChatId,
                Content = message.Content,
                CreatedAt = message.CreatedAt.ToTimestamp()
            });
        }

        RepeatedField<JobConfirmationObject> jobConfirmationObjects = new RepeatedField<JobConfirmationObject>();
        foreach (JobConfirmation jobConfirmation in chat.JobConfirmations)
        {
            jobConfirmationObjects.Add(new JobConfirmationObject()
            {
                Id = jobConfirmation.Id,
                ChatId = jobConfirmation.Chat.Id,
                SubstituteId= jobConfirmation.Substitute.Id,
                EmployerId = jobConfirmation.Employer.Id,
                IsAccepted = jobConfirmation.IsAccepted,
                CreatedAt = jobConfirmation.CreatedAt.ToTimestamp()
            });
        }

        return new GetChatHistoryResponse()
        {
            Messages = { messageObjects },
            JobConfirmations = { jobConfirmationObjects },
            Employer = ToChatUserObject(new User() { Id = chat.EmployerId }),
            Substitute = ToChatUserObject(new User() { Id = chat.SubstituteId })
        };
    }
}