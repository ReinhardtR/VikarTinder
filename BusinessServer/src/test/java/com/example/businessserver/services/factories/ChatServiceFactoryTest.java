package com.example.businessserver.services.factories;

import ChatService.*;
import com.example.businessserver.dtos.chat.*;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

class ChatServiceFactoryTest
{

  @BeforeEach void setUp()
  {
    ChatServiceFactory ChatServiceFactory = new ChatServiceFactory();
  }

  @Test void toCreateChatRequest()
  {
    CreateChatDTO CreateChatDTO = new CreateChatDTO(
        new ArrayList<>(Arrays.asList(1, 2)));

     CreateChatRequest createChatRequest =  ChatServiceFactory.toCreateChatRequest(CreateChatDTO);

      //Check if all fields in class CreateChatDTO and CreateChatRequest are equal
      assertEquals(CreateChatDTO.getIds(), createChatRequest.getUserIdsList());


  }

  @Test void toChatDto()
  {
   CreateChatResponse createChatResponse = CreateChatResponse.newBuilder().setId(1).build();

    ChatDTO chatDTO = ChatServiceFactory.toChatDto(createChatResponse);

    //Check if all fields in class CreateChatResponse and ChatDTO are equal
    assertEquals(createChatResponse.getId(), chatDTO.getChatId());
  }

  @Test void toGetChatHistoryRequest()
  {
    GetChatHistoryDTO getChatHistoryDTO = new GetChatHistoryDTO(1);

    GetChatHistoryRequest getChatHistoryRequest = ChatServiceFactory.toGetChatHistoryRequest(getChatHistoryDTO);

    //Check if all fields in class GetChatHistoryDTO and GetChatHistoryRequest are equal
    assertEquals(getChatHistoryDTO.getId(), getChatHistoryRequest.getChatId());
  }

  @Test void toChatHistoryDto()
  {
    GetChatHistoryResponse getChatHistoryResponse = GetChatHistoryResponse.newBuilder()
        .addAllMessages(new ArrayList<>(Arrays.asList(ChatServiceFactory.toMessageObject(
            new MessageDTO(1,1,1,"request"))
        ,ChatServiceFactory.toMessageObject(
            new MessageDTO(2,1,2,"response"))
        ,ChatServiceFactory.toMessageObject(
            new MessageDTO(3,1,1,"request2"))
        ,ChatServiceFactory.toMessageObject(
            new MessageDTO(4,1,2,"response2" )))
        )).build();

    ChatHistoryDTO chatHistoryDTO = ChatServiceFactory.toChatHistoryDto(getChatHistoryResponse);

    //Check if all fields in class GetChatHistoryResponse and ChatHistoryDTO are equal
    assertNotNull(getChatHistoryResponse.getMessagesList(), "GetChatHistoryResponse.getMessages is null");
    assertNotNull(chatHistoryDTO.getMessages(), "ChatHistoryDTO.getMessages is null");
  }

  @Test void toSendMessageRequest()
  {
    SendMessageDTO sendMessageDTO = new SendMessageDTO(1,1,"request");

    SendMessageRequest sendMessageRequest = ChatServiceFactory.toSendMessageRequest(sendMessageDTO);

    //Check if all fields in class SendMessage and SendMessageRequest are equal
    assertEquals(sendMessageDTO.getChatId(), sendMessageRequest.getChatId());
    assertEquals(sendMessageDTO.getAuthorId(), sendMessageRequest.getAuthorId());
    assertEquals(sendMessageDTO.getContent(), sendMessageRequest.getContent());
  }

  @Test void toGetChatOverviewRequest()
  {
    GetChatOverviewDTO getChatOverviewDTO = new GetChatOverviewDTO(1);

    GetChatOverviewRequest getChatOverviewRequest = ChatServiceFactory.toGetChatOverviewRequest(getChatOverviewDTO);

    //Check if all fields in class GetChatOverviewDTO and GetChatOverviewRequest are equal
    assertEquals(getChatOverviewDTO.getUserId(), getChatOverviewRequest.getUserId(),"Userids are not equal in GetChatOverviewDTO and GetChatOverviewRequest");
  }

  @Test void toChatOverviewDTO()
  {
    GetChatOverviewResponse getChatOverviewResponse = GetChatOverviewResponse.newBuilder()
        .addAllChats(new ArrayList<>(Arrays.asList(
            ChatOverviewObject.newBuilder()
                .setId(1)
                .addAllUserIds(new ArrayList<>(Arrays.asList(1,2)))
                .build(),
            ChatOverviewObject.newBuilder()
                .setId(2)
                .addAllUserIds(new ArrayList<>(Arrays.asList(1,3)))
                .build(),
            ChatOverviewObject.newBuilder()
                .setId(3)
                .addAllUserIds(new ArrayList<>(Arrays.asList(1,4)))
                .build()
        ))).build();

    ChatOverviewDTO chatOverviewDTO = ChatServiceFactory.toChatOverviewDTO(getChatOverviewResponse);

    //Check if all fields in class GetChatOverviewResponse and ChatOverviewDTO are equal
    //Null check
    assertNotNull(chatOverviewDTO.getChats(), "ChatOverviewDTO.getChats is null");

    //Specific chats check
    assertEquals(getChatOverviewResponse.getChatsList().get(0).getId(), chatOverviewDTO.getChats().get(0).getId(), "Specific message in ChatOverviewDTO is not equal to GetChatOverviewResponse");
  }

  @Test void toBasicChatDTO()
  {
    ChatOverviewObject chatOverviewObject = ChatOverviewObject.newBuilder()
        .setId(1)
        .addAllUserIds(new ArrayList<>(Arrays.asList(1,2)))
        .build();

    BasicChatDTO basicChatDTO = ChatServiceFactory.toBasicChatDTO(chatOverviewObject);

    //Check if all fields in class ChatOverviewObject and BasicChatDTO are equal
    assertEquals(chatOverviewObject.getId(), basicChatDTO.getId(), "ChatOverviewObject.getId() is not equal to BasicChatDTO.getId()");
    assertEquals(chatOverviewObject.getUserIdsList(), basicChatDTO.getUserIds(), "ChatOverviewObject.getUserIdsList() is not equal to BasicChatDTO.getUserIds()");
  }

  @Test void toMessageDTO()
  {
    MessageObject messageObject = MessageObject.newBuilder()
        .setId(1)
        .setChatId(1)
        .setAuthorId(1)
        .setContent("request")
        .build();

    MessageDTO messageDTO = ChatServiceFactory.toMessageDTO(messageObject);

    //Check if all fields in class MessageObject and MessageDTO are equal
    assertEquals(messageObject.getId(), messageDTO.getId(), "MessageObject.getId() is not equal to MessageDTO.getId()");
    assertEquals(messageObject.getChatId(), messageDTO.getChatId(), "MessageObject.getChatId() is not equal to MessageDTO.getChatId()");
    assertEquals(messageObject.getAuthorId(), messageDTO.getAuthorId(), "MessageObject.getAuthorId() is not equal to MessageDTO.getAuthorId()");
    assertEquals(messageObject.getContent(), messageDTO.getContent(), "MessageObject.getContent() is not equal to MessageDTO.getContent()");
  }

  @Test void toMessageObject()
  {
    MessageDTO messageDTO = new MessageDTO(1,1,1,"request");

    MessageObject messageObject = ChatServiceFactory.toMessageObject(messageDTO);

    //Check if all fields in class MessageDTO and MessageObject are equal
    assertEquals(messageDTO.getId(), messageObject.getId(), "MessageDTO.getId() is not equal to MessageObject.getId()");
    assertEquals(messageDTO.getChatId(), messageObject.getChatId(), "MessageDTO.getChatId() is not equal to MessageObject.getChatId()");
    assertEquals(messageDTO.getAuthorId(), messageObject.getAuthorId(), "MessageDTO.getAuthorId() is not equal to MessageObject.getAuthorId()");
    assertEquals(messageDTO.getContent(), messageObject.getContent(), "MessageDTO.getContent() is not equal to MessageObject.getContent()");
  }
}