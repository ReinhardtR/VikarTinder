using System.Collections;
using Microsoft.VisualBasic.CompilerServices;
using NUnit.Framework.Internal;
using Persistence;
using Persistence.Models;
using Persistence.Services;

namespace UnitTest.Chat;


//TODO:	Mangler jobconfirmation factory


//Factory cass without validation, only creates objects.
[TestFixture]
public class ChatServiceFactoryTest
{
    private static object[] ToSendMessageResponseTestData =
    {
        new object[] { 1, -1, 1, "yes" },
        new object[] { -2, 2, int.MaxValue, "2" },
        new object[] { 3, 4, int.MinValue, " " }
    };

    private static object[] ToCreateChatResponseTestData =
    {
        new object[] { 1 },
        new object[] { -2 },
        new object[] { int.MinValue }
    };

    private static object[] ToGetChatOverviewResponseTestData =
    {
        new object[] { 1, -2 },
        new object[] { -2, 3 },
        new object[] { int.MinValue, int.MaxValue }
    };

    private static object[] ToGetChatHistoryResponseTestData =
    {
        new object[] { 1, -1, 2 },
        new object[] { 2, -1, 3 },
        new object[] { 3, -2, 3 },
        new object[] { 4, 2, 4 },
    };
    
    
        
        
    
    [Test, TestCaseSource(nameof(ToSendMessageResponseTestData))]
    public void ToSendMessageResponseTest(int id, int authorId, int chatId, string content)
    {
        Message message = new Message
        {
            Id = id,
            AuthorId = authorId,
            ChatId = chatId,
            Content = content
        };
        
        SendMessageResponse messageResponse = ChatServiceFactory.ToSendMessageResponse(message);
        
                //Assert that all matching fields in message and messageResponse are equal
                Assert.Multiple(() =>
        {
            Assert.That(messageResponse.Message.Id, Is.EqualTo(message.Id));
            Assert.That(messageResponse.Message.AuthorId, Is.EqualTo(message.AuthorId));
            Assert.That(messageResponse.Message.ChatId, Is.EqualTo(message.ChatId));
            Assert.That(messageResponse.Message.Content, Is.EqualTo(message.Content));
        });
    }
    
    
    
    [Test, TestCaseSource(nameof(ToCreateChatResponseTestData))]
    public void ToCreateChatResponseTest(int id)
    {
        Persistence.Models.Chat chat = new Persistence.Models.Chat
        {
            Id = id,
        };
        CreateChatResponse chatResponse = ChatServiceFactory.ToCreateChatResponse(chat);
        
        //Assert that all matching fields in chat and chatResponse are equal
        Assert.Multiple(() =>
        {
            Assert.That(chatResponse.Id, Is.EqualTo(chat.Id));
        });
    }
    
    [Test, TestCaseSource(nameof(ToGetChatOverviewResponseTestData))]
    public void ToGetChatOverviewResponseTest(int user1Id, int user2Id)
    {
        //Make a list of Chats
        List<Persistence.Models.Chat> chats = new();
        
        //Make a list of users and put users into the list
        Employer employer = new()
        {
            Id = 1,
        };
        
        Substitute substitute = new()
        {
            Id = 2,
        };
    
        //Make a list of messages and put messages into the list
        List<Message> messages = new();
    
        messages.Add(new Message
        {
            Id = 1,
            AuthorId = user1Id,
            ChatId = 1,
            Content = "Hello"
        });
    
        messages.Add(new Message
        {
            Id = 2,
            AuthorId = user2Id,
            ChatId = 1,
            Content = "Hello to you too!"
        });
    
        chats.Add(new Persistence.Models.Chat
        {
            Id = 1,
            Messages = messages,
            Employer = employer,
            Substitute = substitute
        });
        chats.Add(new Persistence.Models.Chat
        {
            Id = 2,
            Messages = messages,
            Employer = employer,
            Substitute = substitute
        });
    
    
        GetUserChatsResponse chatsToTest = ChatServiceFactory.ToGetUserChatsResponse(chats);
    
        //Assert that all matching fields in chats and chatsToTest are equal
        foreach (var originalChat in chats)
        {
            Assert.That(originalChat.Employer.Id, Is.EqualTo(chatsToTest.Chats[originalChat.Id - 1].Employer.Id));
            Assert.That(originalChat.Substitute.Id, Is.EqualTo(chatsToTest.Chats[originalChat.Id - 1].Substitute.Id));
        }
    }

    [Test, TestCaseSource(nameof(ToGetChatHistoryResponseTestData))]
    public static void ToGetChatHistoryResponseTest(int chatId, int substituteId, int employerId)
    {
        
        //Make a list of messages and put messages into the list
        List<Message> messages = new List<Message>();

        for (int i = 0; i < 3; i++)
        {
            Message message = new Message
            {
                Id = i + 1,
                AuthorId = (i % 2 == 0) ? substituteId : employerId,
                ChatId = chatId,
                Content = "Hello: " + i
            };
        }
        
        //make a chat
        Persistence.Models.Chat chat = new Persistence.Models.Chat
        {
            Id = chatId,
            Messages = messages,
            SubstituteId = substituteId,
            EmployerId = employerId
        };
    
        GetChatHistoryResponse chatToTest = ChatServiceFactory.ToGetChatHistoryResponse(chat);
        
        //Assert that all matching fields in messages and messagesToTest are equal
        foreach (var originalMessage in messages)
        {
            Assert.That(originalMessage.Id, Is.EqualTo(chatToTest.Messages[originalMessage.Id - 1].Id));
            Assert.That(originalMessage.AuthorId, Is.EqualTo(chatToTest.Messages[originalMessage.Id - 1].AuthorId));
            Assert.That(originalMessage.ChatId, Is.EqualTo(chatToTest.Messages[originalMessage.Id - 1].ChatId));
            Assert.That(originalMessage.Content, Is.EqualTo(chatToTest.Messages[originalMessage.Id - 1].Content));
        }
    
    }
}