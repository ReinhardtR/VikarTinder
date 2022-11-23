package com.example.businessserver.services.factories;

import ChatService.*;
import com.example.businessserver.dtos.chat.*;

import java.util.List;

public class ChatServiceFactory {
	// Create Chat
	public static CreateChatRequest toCreateChatRequest(CreateChatDTO dto) {
		return CreateChatRequest
						.newBuilder()
						.addAllUserIds(dto.getIds())
						.build();
	}

	public static ChatDTO toChatDto(CreateChatResponse response) {
		return new ChatDTO(response.getId());
	}

	// Chat History
	public static GetChatHistoryRequest toGetChatHistoryRequest(GetChatHistoryDTO dto) {
		return GetChatHistoryRequest
						.newBuilder()
						.setChatId(dto.getId())
						.build();
	}

	public static ChatHistoryDTO toChatHistoryDto(GetChatHistoryResponse response) {
		List<MessageDTO> messages = response.getMessagesList()
						.stream()
						.map(ChatServiceFactory::toMessageDTO)
						.toList();

		return new ChatHistoryDTO(messages);
	}

	// Send Message
	public static SendMessageRequest toSendMessageRequest(SendMessageDTO dto) {
		return SendMessageRequest
						.newBuilder()
						.setChatId(dto.getChatId())
						.setAuthorId(dto.getAuthorId())
						.setContent(dto.getContent())
						.build();
	}

	// Shared Methods
	public static MessageDTO toMessageDTO(MessageObject message) {
		return new MessageDTO(
						message.getId(),
						message.getChatId(),
						message.getAuthorId(),
						message.getContent()
		);
	}

	public static MessageObject toMessageObject(MessageDTO dto) {
		return MessageObject
						.newBuilder()
						.setId(dto.getId())
						.setChatId(dto.getChatId())
						.setAuthorId(dto.getAuthorId())
						.setContent(dto.getContent())
						.build();

	}
}
