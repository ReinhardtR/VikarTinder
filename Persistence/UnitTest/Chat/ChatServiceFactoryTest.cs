using System.Collections;
using Persistence;
using Persistence.Models;
using Persistence.Services;

namespace UnitTest.Chat;

[TestFixture]
public class ChatServiceFactoryTest
{


    [Test]
    public void ToSendMessageResponseTest()
    {
        Message message = new Message
        {
            Id = 1,
            AuthorId = 1,
            ChatId = 1,
            Content = "Hello"
        };
        SendMessageResponse messageResponse = ChatServiceFactory.ToSendMessageResponse(message);

        Assert.Multiple(() =>
        {

            //Assert that all matching fields in message and messageResponse are equal
            Assert.That(messageResponse.Message.Id, Is.EqualTo(message.Id));
            Assert.That(messageResponse.Message.AuthorId, Is.EqualTo(message.AuthorId));
            Assert.That(messageResponse.Message.ChatId, Is.EqualTo(message.ChatId));
            Assert.That(messageResponse.Message.Content, Is.EqualTo(message.Content));
        });
    }

    [Test]
    public void ToCreateChatResponseTest()
    {
        Persistence.Models.Chat chat = new Persistence.Models.Chat
        {
            Id = 1,
        };
        CreateChatResponse chatResponse = ChatServiceFactory.ToCreateChatResponse(chat);

        Assert.Multiple(() =>
        {
            //Assert that all matching fields in chat and chatResponse are equal
            Assert.That(chatResponse.Id, Is.EqualTo(chat.Id));
        });
    }

    [Test]
    public void ToGetChatOverviewResponseTest()
    {
        //Make a list of Chats
        List<Persistence.Models.Chat> chats = new List<Persistence.Models.Chat>();
        //Make a list of users and put users into the list

        List<User> users = new List<User>();
        users.Add(new User
        {
            Id = 1,
            Chats = null
        });
        users.Add(new User
        {
            Id = 2,
            Chats = null
        });

        //Make a list of messages and put messages into the list
        List<Message> messages = new List<Message>();

        messages.Add(new Message
        {
            Id = 1,
            AuthorId = 1,
            ChatId = 1,
            Content = "Hello"
        });

        messages.Add(new Message
        {
            Id = 2,
            AuthorId = 2,
            ChatId = 1,
            Content = "Hello to you too!"
        });

        chats.Add(new Persistence.Models.Chat
        {
            Id = 1,
            Messages = messages,
            Participants = users
        });
        chats.Add(new Persistence.Models.Chat
        {
            Id = 2,
            Messages = messages,
            Participants = users
        });


        GetChatOverviewResponse chatsToTest = ChatServiceFactory.ToGetChatOverviewResponse(chats);

        //Assert that all matching fields in chats and chatsToTest are equal
        foreach (var originalChat in chats)
        {
            Assert.That(originalChat.Id, Is.EqualTo(chatsToTest.Chats[originalChat.Id - 1].Id));
        }
    }

    [Test]
    public static void ToGetChatHistoryResponseTest()
    {
        //Make a list of messages and put messages into the list
        List<Message> messages = new List<Message>();

        messages.Add(new Message
        {
            Id = 1,
            AuthorId = 1,
            ChatId = 1,
            Content = "Hello"
        });

        messages.Add(new Message
        {
            Id = 2,
            AuthorId = 2,
            ChatId = 1,
            Content = "Hello to you too!"
        });

        GetChatHistoryResponse messagesToTest = ChatServiceFactory.ToGetChatHistoryResponse(messages);

        //Assert that all matching fields in messages and messagesToTest are equal
        foreach (var originalMessage in messages)
        {
            Assert.That(originalMessage.Id, Is.EqualTo(messagesToTest.Messages[originalMessage.Id - 1].Id));
            Assert.That(originalMessage.AuthorId, Is.EqualTo(messagesToTest.Messages[originalMessage.Id - 1].AuthorId));
            Assert.That(originalMessage.ChatId, Is.EqualTo(messagesToTest.Messages[originalMessage.Id - 1].ChatId));
            Assert.That(originalMessage.Content, Is.EqualTo(messagesToTest.Messages[originalMessage.Id - 1].Content));

        }

    }
}