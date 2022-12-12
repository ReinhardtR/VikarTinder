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
            GigId = chat.GigId,
            Employer = ToEmployerUserObject(new Employer {Id = chat.EmployerId}),
            Substitute = ToSubstituteUserObject(new Substitute{Id = chat.SubstituteId})
        };
    }

    public static GetUserChatsResponse ToGetUserChatsResponse(List<Chat> chats)
    {
        RepeatedField<ChatOverviewObject> chatOverviewObjects = new();

        foreach (Chat chat in chats)
        {
            chatOverviewObjects.Add(new ChatOverviewObject()
            {
                Id = chat.Id,
                GigId = chat.GigId,
                Employer = ToEmployerUserObject(new Employer{ Id = chat.EmployerId }),
                Substitute = ToSubstituteUserObject(new Substitute{ Id = chat.SubstituteId })
            });
        }
        
        GetUserChatsResponse getChatOverviewResponse = new()
        {
            Chats = { chatOverviewObjects }
        };
        
        return getChatOverviewResponse;
    }

    private static EmployerUserObject ToEmployerUserObject(Employer employer)
    {
        return new EmployerUserObject()
        {
            Id = employer.Id,
            FirstName = employer.FirstName,
            LastName = employer.LastName
        };
    }
    private static SubstituteUserObject ToSubstituteUserObject(Substitute substitute)
    {
        return new SubstituteUserObject()
        {
            Id = substitute.Id,
            FirstName = substitute.FirstName,
            LastName = substitute.LastName
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

    public static GetGigChatsResponse ToGigChatsResponse(List<Chat> chats)
    {
        RepeatedField<ChatOverviewObject> chatOverviewObjects = new();

        foreach (Chat chat in chats)
        {
            chatOverviewObjects.Add(new ChatOverviewObject()
            {
                Id = chat.Id,
                GigId = chat.GigId,
                Employer = ToEmployerUserObject(new Employer{ Id = chat.EmployerId }),
                Substitute = ToSubstituteUserObject(new Substitute{ Id = chat.SubstituteId })
            });
        }
        
        GetGigChatsResponse getGigChatsResponse = new()
        {
            Chats = { chatOverviewObjects }
        };
        
        return getGigChatsResponse;
    }
}