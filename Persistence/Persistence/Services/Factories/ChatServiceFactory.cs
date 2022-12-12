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
                AuthorId = message.AuthorId,
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
            Employer = ToEmployerUserObject(new Employer {Id = chat.EmployerId}),
            Substitute = ToSubstituteUserObject(new Substitute{Id = chat.SubstituteId})
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
                Employer = ToEmployerUserObject(new Employer{Id = chat.EmployerId}),
                Substitute = ToSubstituteUserObject(new Substitute{Id = chat.SubstituteId}) //skal ændres når vi merger usecases
            });
        }
        
        GetChatOverviewResponse getChatOverviewResponse = new()
        {
            Chats = { chatOverviewObjects }
        };
        
        return getChatOverviewResponse;
    }

    private static EmployerUserObject ToEmployerUserObject(Employer employer)
    {
        return new EmployerUserObject()
        {
            Id = employer.Id
        };
    }
    private static SubstituteUserObject ToSubstituteUserObject(Substitute substitute)
    {
        return new SubstituteUserObject()
        {
            Id = substitute.Id
        };
    }

    public static GetChatHistoryResponse ToGetChatHistoryResponse(Chat chat)
    {
        RepeatedField<MessageObject> messageObjects = new();
        
        foreach (Message message in chat.Messages)
        {
            messageObjects.Add(new MessageObject()
            {
                Id = message.Id,
                AuthorId = message.AuthorId,
                ChatId = message.ChatId,
                Content = message.Content,
                CreatedAt = message.CreatedAt.ToTimestamp()
            });
        }
        
        JobConfirmationObject? jobConfirmation = chat.JobConfirmation != null
            ? new JobConfirmationObject()
            {
                Id = chat.JobConfirmation.Id,
                ChatId = chat.JobConfirmation.ChatId,
                SubstituteId = chat.JobConfirmation.SubstituteId,
                EmployerId = chat.JobConfirmation.EmployerId,
                Status = JobConfirmationFactory.ToJobConfirmationStatusGrpc(chat.JobConfirmation.Status),
                CreatedAt = chat.JobConfirmation.CreatedAt.ToTimestamp()
            }
            : null;
        
        return new GetChatHistoryResponse()
        {
            Messages = { messageObjects },
            JobConfirmation =  jobConfirmation ,
            Employer = ToEmployerUserObject(new Employer { Id = chat.EmployerId }),
            Substitute = ToSubstituteUserObject(new Substitute { Id = chat.SubstituteId })
        };
    }
}