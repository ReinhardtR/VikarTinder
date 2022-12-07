package com.example.businessserver.services.factories;

import ChatService.*;
import com.example.businessserver.dtos.chat.*;
import com.example.businessserver.dtos.chat.message.MessageDTO;
import com.example.businessserver.dtos.chat.message.SendMessageDTO;
import com.example.businessserver.dtos.jobconfirmation.JobConfirmationDTO;

import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.util.List;

public class ChatServiceFactory {
	// Create Chat
	public static CreateChatRequest toCreateChatRequest(CreateChatDTO dto) {
		return CreateChatRequest
						.newBuilder()
						.setEmployer(toChatUserObject(dto.getEmployerId()))
						.setSubstitute(toChatUserObject(dto.getSubstituteId()))
						.build();
	}


	// Chat History
	public static GetChatHistoryRequest toGetChatHistoryRequest(GetChatHistoryDTO dto) {
		return GetChatHistoryRequest
						.newBuilder()
						.setChatId(dto.getChatId())
						.build();
	}

	public static ChatHistoryDTO toChatHistoryDto(GetChatHistoryResponse response) {
		List<MessageDTO> messages = response.getMessagesList()
						.stream()
						.map(ChatServiceFactory::toMessageDTO)
						.toList();

		JobConfirmationDTO jobConfirmation = JobConfirmationServiceFactory.toJobConfirmationDTO(response.getJobConfirmation());

		return new ChatHistoryDTO(messages, jobConfirmation, response.getSubstitute().getId(), response.getEmployer().getId());

	}


	// Send Message
	public static SendMessageRequest toSendMessageRequest(SendMessageDTO dto) {
		return SendMessageRequest
						.newBuilder()
						.setChatId(dto.getChatId())
						.setAuthor(toChatUserObject(dto.getAuthorId()))
						.setContent(dto.getContent())
						.build();
	}

	// Chat Overview
	public static GetChatOverviewRequest toGetChatOverviewRequest(GetChatOverviewDTO dto) {
		return GetChatOverviewRequest
						.newBuilder()
						.setUser(toChatUserObject(dto.getUserId()))
						.build();
	}

	public static BasicChatDTO toBasicChatDTO(CreateChatResponse response) {
		return new BasicChatDTO(response.getId(), response.getSubstitute().getId(), response.getEmployer().getId());
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
		return new BasicChatDTO(chat.getId(), chat.getSubstitute().getId(), chat.getEmployer().getId());
	}

	public static MessageDTO toMessageDTO(MessageObject message) {
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

	//TO DO Hvaa skal disse ikke bruges i future? Venter lige med at slette dem.
	private ChatUserObject toChatUserObject(ChatUserDTO dto) {
		return ChatUserObject
						.newBuilder()
						.setId(dto.getId())
						.build();
	}
}
