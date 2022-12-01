package com.example.businessserver.services.factories;

import ChatService.*;
import com.example.businessserver.dtos.chat.*;
import com.example.businessserver.dtos.chat.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationDTO;

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

	// Chat Overview
	public static GetChatOverviewRequest toGetChatOverviewRequest(GetChatOverviewDTO dto) {
		return GetChatOverviewRequest
						.newBuilder()
						.setUserId(dto.getUserId())
						.build();
	}

	public static ChatOverviewDTO toChatOverviewDTO(GetChatOverviewResponse response) {
		List<BasicChatDTO> chats = response.getChatsList()
						.stream()
						.map(ChatServiceFactory::toBasicChatDTO)
						.toList();

		return new ChatOverviewDTO(chats);
	}

	// Shared Methods
	public static BasicChatDTO toBasicChatDTO(ChatOverviewObject chat) {
		return new BasicChatDTO(chat.getId(), chat.getUserIdsList());
	}

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

	public static CreateJobConfirmationRequest toCreateJobConfirmationRequest(
			CreateJobConfirmationDTO dto)
	{
		return CreateJobConfirmationRequest
						.newBuilder()
						.setChatId(dto.getChatId())
						.setSubstituteId(dto.getSubstituteId())
						.setEmployerId(dto.getEmployerId())
						.build();
	}

	public static JobConfirmationDTO toJobConfirmationDTO(
			CreateJobConfirmationResponse response)
	{
		return new JobConfirmationDTO(
						response.getId(),
						response.getChatId(),
						response.getSubstituteId(),
						response.getEmployerId(),
						response.getIsAccepted()
		);
	}

	private ChatUserObject toChatUserObject(ChatUserDTO dto) {
		return ChatUserObject
						.newBuilder()
						.setId(dto.getId())
						.build();
	}
}
