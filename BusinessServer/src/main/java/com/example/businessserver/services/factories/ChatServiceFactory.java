package com.example.businessserver.services.factories;

import ChatService.*;
import JobConfirmationService.JobConfirmationObject;
import JobConfirmationService.JobConfirmationStatus;
import com.example.businessserver.dtos.JobConfirmation.JobConfirmationDTO;
import com.example.businessserver.dtos.chat.*;
import com.example.businessserver.dtos.chat.message.MessageDTO;
import com.example.businessserver.dtos.chat.message.SendMessageDTO;

import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.util.List;

public class ChatServiceFactory {
	// Create Chat
	public static CreateChatRequest toCreateChatRequest(CreateChatDTO dto) {
		if (ObjectIsNull(dto))
			return null;

		return CreateChatRequest
						.newBuilder()
						.setEmployer(toChatUserObject(dto.getEmployerId()))
						.setSubstitute(toChatUserObject(dto.getSubstituteId()))
						.build();
	}


	// Chat History
	public static GetChatHistoryRequest toGetChatHistoryRequest(GetChatHistoryDTO dto) {
		if (ObjectIsNull(dto))
			return null;

		return GetChatHistoryRequest
						.newBuilder()
						.setChatId(dto.getChatId())
						.build();
	}

	public static ChatHistoryDTO toChatHistoryDto(GetChatHistoryResponse response) {
		if (ObjectIsNull(response))
			return null;

		List<MessageDTO> messages = response.getMessagesList()
						.stream()
						.map(ChatServiceFactory::toMessageDTO)
						.toList();

		JobConfirmationDTO jobConfirmation = toJobConfirmationDTO(response.getJobConfirmation());

		return new ChatHistoryDTO(messages, jobConfirmation, response.getSubstitute().getId(), response.getEmployer().getId());

	}


	// Send Message
	public static SendMessageRequest toSendMessageRequest(SendMessageDTO dto) {
		if (ObjectIsNull(dto))
			return null;

		return SendMessageRequest
						.newBuilder()
						.setChatId(dto.getChatId())
						.setAuthor(toChatUserObject(dto.getAuthorId()))
						.setContent(dto.getContent())
						.build();
	}

	// Chat Overview
	public static GetChatOverviewRequest toGetChatOverviewRequest(GetChatOverviewDTO dto) {
		if (ObjectIsNull(dto))
			return null;

		return GetChatOverviewRequest
						.newBuilder()
						.setUser(toChatUserObject(dto.getUserId()))
						.build();
	}

	public static BasicChatDTO toBasicChatDTO(CreateChatResponse response) {
		if (ObjectIsNull(response))
			return null;

		return new BasicChatDTO(response.getId(), response.getSubstitute().getId(), response.getEmployer().getId());
	}

	public static ChatOverviewDTO toChatOverviewDTO(GetChatOverviewResponse response) {
		if (ObjectIsNull(response))
			return null;

		List<BasicChatDTO> chats = response.getChatsList()
						.stream()
						.map(ChatServiceFactory::toBasicChatDTO)
						.toList();

		return new ChatOverviewDTO(chats);
	}

	// Shared Methods
	public static BasicChatDTO toBasicChatDTO(ChatOverviewObject chat) {
		if (ObjectIsNull(chat))
			return null;

		return new BasicChatDTO(chat.getId(), chat.getSubstitute().getId(), chat.getEmployer().getId());
	}

	public static MessageDTO toMessageDTO(MessageObject message) {
		if (ObjectIsNull(message))
			return null;

		return new MessageDTO(
						message.getId(),
						message.getChatId(),
						message.getAuthor().getId(),
						message.getContent(),
						LocalDateTime.ofInstant(
										Instant.ofEpochSecond(message.getCreatedAt().getSeconds()), ZoneId.of("UTC"))
		);
	}

	public static MessageObject toMessageObject(MessageDTO dto) {
		if (ObjectIsNull(dto))
			return null;

		return MessageObject
						.newBuilder()
						.setId(dto.getId())
						.setChatId(dto.getChatId())
						.setAuthor(toChatUserObject(dto.getAuthorId()))
						.setContent(dto.getContent())
						.build();

	}

	private static ChatUserObject toChatUserObject(int authorId) {
		return ChatUserObject.newBuilder()
						.setId(authorId)
						.build();
	}

	private static boolean ObjectIsNull(Object o) {
		return o == null;
	}

	private static JobConfirmationDTO toJobConfirmationDTO(
					JobConfirmationObject jobConfirmationObject) {

		return new JobConfirmationDTO(
						jobConfirmationObject.getId(),
						jobConfirmationObject.getChatId(),
						jobConfirmationObject.getSubstituteId(),
						jobConfirmationObject.getEmployerId(),
						jobConfirmationObject.getIsAccepted(),
						LocalDateTime.ofInstant(
										Instant.ofEpochSecond(jobConfirmationObject.getCreatedAt().getSeconds()), ZoneId.of("UTC"))
		);
	}

	private ChatUserObject toChatUserObject(ChatUserDTO dto) {
		return ChatUserObject
						.newBuilder()
						.setId(dto.getId())
						.build();
	}

	private Boolean fromJobConfirmationStatus(JobConfirmationStatus status) {
		return switch (status) {
			case ACCEPTED -> true;
			case DECLINED -> false;
			default -> null;
		};
	}
}
