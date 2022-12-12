package com.example.businessserver.services.factories;

import ChatService.*;
import com.example.businessserver.dtos.chat.CreateChatDTO;
import com.example.businessserver.dtos.chat.history.ChatEmployerDTO;
import com.example.businessserver.dtos.chat.history.ChatHistoryDTO;
import com.example.businessserver.dtos.chat.history.ChatSubstituteDTO;
import com.example.businessserver.dtos.chat.history.GetChatHistoryDTO;
import com.example.businessserver.dtos.chat.message.MessageDTO;
import com.example.businessserver.dtos.chat.message.SendMessageDTO;
import com.example.businessserver.dtos.chat.overview.BasicChatDTO;
import com.example.businessserver.dtos.chat.overview.ChatOverviewDTO;
import com.example.businessserver.dtos.chat.overview.GetChatOverviewByGigDTO;
import com.example.businessserver.dtos.chat.overview.GetChatOverviewByUserDTO;
import com.example.businessserver.dtos.chat.overview.gigs.EmployerGigsDTO;
import com.example.businessserver.dtos.chat.overview.gigs.GetEmployerGigsDTO;
import com.example.businessserver.dtos.chat.overview.gigs.GigDTO;
import com.example.businessserver.dtos.jobconfirmation.JobConfirmationDTO;

import java.util.List;

public class ChatServiceFactory {
	// Create Chat
	public static CreateChatRequest toCreateChatRequest(CreateChatDTO dto) {
		return CreateChatRequest
						.newBuilder()
						.setEmployer(toEmployerObject(dto.getEmployerId()))
						.setSubstitute(toSubstituteObject(dto.getSubstituteId()))
						.build();
	}

	private static SubstituteUserObject toSubstituteObject(int substituteId) {
		return SubstituteUserObject
						.newBuilder()
						.setId(substituteId)
						.build();
	}

	private static EmployerUserObject toEmployerObject(int employerId) {
		return EmployerUserObject
						.newBuilder()
						.setId(employerId)
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

		return new ChatHistoryDTO(messages, jobConfirmation, toChatEmployerDTO(response.getEmployer()), toChatSubstituteDTO(response.getSubstitute()));

	}

	public static ChatEmployerDTO toChatEmployerDTO(EmployerUserObject employer) {
		return new ChatEmployerDTO(employer.getId(), employer.getFirstName(), employer.getLastName());
	}

	public static ChatSubstituteDTO toChatSubstituteDTO(SubstituteUserObject substitute) {
		return new ChatSubstituteDTO(substitute.getId(), substitute.getFirstName(), substitute.getLastName());
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
	public static GetUserChatsRequest toGetUserChatsRequest(GetChatOverviewByUserDTO dto) {
		return GetUserChatsRequest
						.newBuilder()
						.setUserId(dto.getUserId())
						.build();
	}

	public static GetGigChatsRequest toGetGigsChatsRequest(GetChatOverviewByGigDTO dto) {
		return GetGigChatsRequest
						.newBuilder()
						.setGigId(dto.getGigId())
						.build();
	}

	public static BasicChatDTO toBasicChatDTO(CreateChatResponse response) {
		return new BasicChatDTO(response.getId(), response.getSubstitute().getId(), response.getEmployer().getId());
	}

	public static ChatOverviewDTO toChatOverviewDTO(GetUserChatsResponse response) {
		List<BasicChatDTO> chats = response.getChatsList()
						.stream()
						.map(ChatServiceFactory::toBasicChatDTO)
						.toList();

		return new ChatOverviewDTO(chats);
	}

	public static ChatOverviewDTO toChatOverviewDTO(GetGigChatsResponse response) {
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
						message.getId(),
						message.getContent(),
						SharedFactory.toLocalDateTime(message.getCreatedAt())
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

	public static GetGigChatsRequest toGetGigChatsRequest(GetChatOverviewByGigDTO dto) {
		return GetGigChatsRequest
						.newBuilder()
						.setGigId(dto.getGigId())
						.build();
	}

	public static GetEmployerGigsRequest toGetEmployerGigsRequest(GetEmployerGigsDTO dto) {
		return GetEmployerGigsRequest
						.newBuilder()
						.setEmployerId(dto.getEmployerId())
						.build();
	}

	public static EmployerGigsDTO toEmployerGigsDTO(GetEmployerGigsResponse response) {
		List<GigDTO> gigs = response.getGigsList()
						.stream()
						.map(ChatServiceFactory::toGigDTO)
						.toList();

		return new EmployerGigsDTO(gigs);
	}

	public static GigDTO toGigDTO(GigObject gig) {
		return new GigDTO(gig.getId());
	}
}
