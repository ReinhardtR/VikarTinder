package com.example.businessserver.services.factories;

import ChatService.*;
import com.example.businessserver.dtos.chat.*;
import com.example.businessserver.dtos.chat.message.MessageDTO;
import com.example.businessserver.dtos.chat.message.SendMessageDTO;
import com.google.protobuf.Timestamp;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.MethodSource;

import java.time.Instant;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.stream.Stream;

import static org.junit.jupiter.api.Assertions.*;

class ChatServiceFactoryTest
{

  //TODO: JobConfirmation-factory test
  //TODO: Test for ZOMBIES (mangler boundary og exceptions et par steder)
  //TODO: Testing af validering
  @ParameterizedTest
  @MethodSource()
  void toCreateChatRequest(int substituteId, int employerId)

  {
    CreateChatDTO CreateChatDTO = new CreateChatDTO(substituteId, employerId);

     CreateChatRequest createChatRequest =  ChatServiceFactory.toCreateChatRequest(CreateChatDTO);

      //Check if all fields in class CreateChatDTO and CreateChatRequest are equal
      assertEquals(CreateChatDTO.getSubstituteId(), createChatRequest.getSubstitute().getId());
      assertEquals(CreateChatDTO.getEmployerId(), createChatRequest.getEmployer().getId());
  }

  private static Stream<Object> toCreateChatRequest()
  {
    return Stream.of(
      new Object[]{1, 2},
      new Object[]{3, 4},
      new Object[]{5, 6}
    );
  }


  @ParameterizedTest
  @MethodSource()
  void toChatDto(int chatId)
  {
   CreateChatResponse createChatResponse = CreateChatResponse.newBuilder().setId(1).build();

    BasicChatDTO chatDTO = ChatServiceFactory.toBasicChatDTO(createChatResponse);

    //Check if all fields in class CreateChatResponse and ChatDTO are equal
    assertEquals(createChatResponse.getId(), chatDTO.getId());
  }

  private static Stream<Object> toChatDto()
  {
    return Stream.of(
      new Object[]{1},
      new Object[]{2},
      new Object[]{3}
    );
  }

  @ParameterizedTest
  @MethodSource()
  void toGetChatHistoryRequest(int chatId)
  {
    GetChatHistoryDTO getChatHistoryDTO = new GetChatHistoryDTO(chatId);

    GetChatHistoryRequest getChatHistoryRequest = ChatServiceFactory.toGetChatHistoryRequest(getChatHistoryDTO);

    //Check if all fields in class GetChatHistoryDTO and GetChatHistoryRequest are equal
    assertEquals(getChatHistoryDTO.getChatId(), getChatHistoryRequest.getChatId());
    //assert that getChatHistoryRequest is not null
    assertNotNull(getChatHistoryRequest);
  }

  private static Stream<Object> toGetChatHistoryRequest()
  {
    return Stream.of(
      new Object[]{1},
      new Object[]{2},
      new Object[]{3}
    );
  }

  @Test
  void toChatHistoryDto()
  {
    GetChatHistoryResponse getChatHistoryResponse = GetChatHistoryResponse.newBuilder()
        .addAllMessages(new ArrayList<>(Arrays.asList(
            ChatServiceFactory.toMessageObject(
            new MessageDTO(1,1,1,"request", LocalDateTime.now())),
            ChatServiceFactory.toMessageObject(
            new MessageDTO(2,1,2,"response", LocalDateTime.now())),
            ChatServiceFactory.toMessageObject(
            new MessageDTO(3,1,1,"request2", LocalDateTime.now())),
            ChatServiceFactory.toMessageObject(
            new MessageDTO(4,1,2,"response2" , LocalDateTime.now())))
        )).build();

    ChatHistoryDTO chatHistoryDTO = ChatServiceFactory.toChatHistoryDto(getChatHistoryResponse);

    //Check if all fields in class GetChatHistoryResponse and ChatHistoryDTO are equal
    assertNotNull(getChatHistoryResponse.getMessagesList(), "GetChatHistoryResponse.getMessages is null");
    assertNotNull(chatHistoryDTO.getMessages(), "ChatHistoryDTO.getMessages is null");
    assertEquals(getChatHistoryResponse.getMessagesList().size(), chatHistoryDTO.getMessages().size());

    for (int i = 0; i < getChatHistoryResponse.getMessagesList().size(); i++)
    {
      assertEquals(getChatHistoryResponse.getMessagesList().get(i).getId(), chatHistoryDTO.getMessages().get(i).getId());
      assertEquals(getChatHistoryResponse.getMessagesList().get(i).getChatId(), chatHistoryDTO.getMessages().get(i).getChatId());
      assertEquals(getChatHistoryResponse.getMessagesList().get(i).getAuthor().getId(), chatHistoryDTO.getMessages().get(i).getAuthorId());
      assertEquals(getChatHistoryResponse.getMessagesList().get(i).getContent(), chatHistoryDTO.getMessages().get(i).getContent());
    }
  }

  @ParameterizedTest
  @MethodSource()
  void toSendMessageRequest(int chatId, int authorId, String content)
  {
    SendMessageDTO sendMessageDTO = new SendMessageDTO(chatId,authorId,content);

    SendMessageRequest sendMessageRequest = ChatServiceFactory.toSendMessageRequest(sendMessageDTO);

    //Check if all fields in class SendMessage and SendMessageRequest are equal
    assertEquals(sendMessageDTO.getChatId(), sendMessageRequest.getChatId());
    assertEquals(sendMessageDTO.getAuthorId(), sendMessageRequest.getAuthor().getId());
    assertEquals(sendMessageDTO.getContent(), sendMessageRequest.getContent());
  }
  private static Stream<Object> toSendMessageRequest()
  {
    return Stream.of(
      new Object[]{1,1,"request"},
      new Object[]{1,2,"response"},
      new Object[]{1,1,"request2"},
      new Object[]{1,2,"response2"}
    );
  }

  @ParameterizedTest
  @MethodSource()
  void toGetChatOverviewRequest(int userId)
  {
    GetChatOverviewDTO getChatOverviewDTO = new GetChatOverviewDTO(userId);

    GetChatOverviewRequest getChatOverviewRequest = ChatServiceFactory.toGetChatOverviewRequest(getChatOverviewDTO);

    //Check if all fields in class GetChatOverviewDTO and GetChatOverviewRequest are equal
    assertEquals(getChatOverviewDTO.getUserId(), getChatOverviewRequest.getUser().getId(),"Userids are not equal in GetChatOverviewDTO and GetChatOverviewRequest");
  }

  private static Stream<Object> toGetChatOverviewRequest()
  {
    return Stream.of(
      new Object[]{1},
      new Object[]{2},
      new Object[]{3}
    );
  }

  @Test void toChatOverviewDTO()
  {
    GetChatOverviewResponse getChatOverviewResponse = GetChatOverviewResponse.newBuilder()
        .addAllChats(new ArrayList<>(Arrays.asList(
            ChatOverviewObject.newBuilder()
                .setId(1)
                .setSubstitute(ChatUserObject.newBuilder().setId(1).build())
                .setEmployer(ChatUserObject.newBuilder().setId(2).build())
                .build(),
            ChatOverviewObject.newBuilder()
                .setId(2)
                .setSubstitute(ChatUserObject.newBuilder().setId(3).build())
                .setEmployer(ChatUserObject.newBuilder().setId(4).build())
                .build(),
            ChatOverviewObject.newBuilder()
                .setId(3)
                .setSubstitute(ChatUserObject.newBuilder().setId(5).build())
                .setEmployer(ChatUserObject.newBuilder().setId(6).build())
                .build()
        ))).build();

    ChatOverviewDTO chatOverviewDTO = ChatServiceFactory.toChatOverviewDTO(getChatOverviewResponse);

    //Check if all fields in class GetChatOverviewResponse and ChatOverviewDTO are equal
    //Null check
    assertNotNull(chatOverviewDTO.getChats(), "ChatOverviewDTO.getChats is null");

    //check if all fields are equal
    for (int i = 0; i < getChatOverviewResponse.getChatsCount(); i++)
    {
      assertEquals(getChatOverviewResponse.getChats(i).getId(), chatOverviewDTO.getChats().get(i).getId());
      assertEquals(getChatOverviewResponse.getChats(i).getEmployer().getId(), chatOverviewDTO.getChats().get(i).getEmployerId());
      assertEquals(getChatOverviewResponse.getChats(i).getSubstitute().getId(), chatOverviewDTO.getChats().get(i).getSubstituteId());
    }
  }
  @ParameterizedTest
  @MethodSource()
  void toBasicChatDTO(int chatId ,int employerId, int substituteId)
  {
    ChatOverviewObject chatOverviewObject = ChatOverviewObject.newBuilder()
        .setId(chatId)
        .setEmployer(ChatUserObject
                      .newBuilder()
                      .setId(employerId)
                      .build())
        .setSubstitute(ChatUserObject
                        .newBuilder()
                        .setId(substituteId)
                        .build())
        .build();

    BasicChatDTO basicChatDTO = ChatServiceFactory.toBasicChatDTO(chatOverviewObject);

    //Check if all fields in class ChatOverviewObject and BasicChatDTO are equal
    assertEquals(chatOverviewObject.getId(), basicChatDTO.getId(), "ChatOverviewObject.getId() is not equal to BasicChatDTO.getId()");
    assertEquals(chatOverviewObject.getEmployer().getId(), basicChatDTO.getEmployerId(), "ChatOverviewObject.getEmployer().getId() is not equal to BasicChatDTO.getEmployerId()");
    assertEquals(chatOverviewObject.getSubstitute().getId(), basicChatDTO.getSubstituteId(), "ChatOverviewObject.getSubstitute().getId() is not equal to BasicChatDTO.getSubstituteId()");

  }
  private static Stream<Object> toBasicChatDTO()
  {
    return Stream.of(
      new Object[]{1,1,2},
      new Object[]{2,4,3},
      new Object[]{3,2,4}
    );
  }

  @ParameterizedTest
  @MethodSource()
  void toMessageDTO(int messageId, int chatId, int authorId, String content)
  {
    MessageObject messageObject = MessageObject.newBuilder()
        .setId(messageId)
        .setChatId(chatId)
        .setAuthor(ChatUserObject.newBuilder().setId(authorId).build())
        .setContent(content)
        .setCreatedAt(Timestamp.newBuilder().setNanos(LocalDateTime.now().getNano()).build())
        .build();


    MessageDTO messageDTO = ChatServiceFactory.toMessageDTO(messageObject);

    //Check if all fields in class MessageObject and MessageDTO are equal
    assertEquals(messageObject.getId(), messageDTO.getId(), "MessageObject.getId() is not equal to MessageDTO.getId()");
    assertEquals(messageObject.getChatId(), messageDTO.getChatId(), "MessageObject.getChatId() is not equal to MessageDTO.getChatId()");
    assertEquals(messageObject.getAuthor().getId(), messageDTO.getAuthorId(), "MessageObject.getAuthorId() is not equal to MessageDTO.getAuthorId()");
    assertEquals(messageObject.getContent(), messageDTO.getContent(), "MessageObject.getContent() is not equal to MessageDTO.getContent()");
  }
  private static Stream<Object> toMessageDTO()
  {
    return Stream.of(
      new Object[]{1,1,1,"request"},
      new Object[]{2,3,2,"response"},
      new Object[]{3,4,1,"request2"},
      new Object[]{4,4,2,"response2"}
    );
  }

  @ParameterizedTest
  @MethodSource()
  void toMessageObject(int messageId, int chatId, int authorId, String content)
  {
    MessageDTO messageDTO = new MessageDTO(1,1,1,"request", LocalDateTime.now());

    MessageObject messageObject = ChatServiceFactory.toMessageObject(messageDTO);

    //Check if all fields in class MessageDTO and MessageObject are equal
    assertEquals(messageDTO.getId(), messageObject.getId(), "MessageDTO.getId() is not equal to MessageObject.getId()");
    assertEquals(messageDTO.getChatId(), messageObject.getChatId(), "MessageDTO.getChatId() is not equal to MessageObject.getChatId()");
    assertEquals(messageDTO.getAuthorId(), messageObject.getAuthor().getId(), "MessageDTO.getAuthorId() is not equal to MessageObject.getAuthorId()");
    assertEquals(messageDTO.getContent(), messageObject.getContent(), "MessageDTO.getContent() is not equal to MessageObject.getContent()");
  }
  private static Stream<Object> toMessageObject()
  {
    return Stream.of(
      new Object[]{1,1,1,"request"},
      new Object[]{2,3,2,"response"},
      new Object[]{3,4,1,"request2"},
      new Object[]{4,4,2,"response2"}
    );
  }
}