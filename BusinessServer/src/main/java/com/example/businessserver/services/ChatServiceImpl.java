package com.example.businessserver.services;

import ChatService.*;
import com.example.businessserver.dtos.chat.*;
import com.example.businessserver.services.factories.ChatServiceFactory;
import net.devh.boot.grpc.client.inject.GrpcClient;
import org.springframework.stereotype.Service;

@Service
public class ChatServiceImpl implements ChatServiceClient {

	@GrpcClient("grpc-server")
	private ChatServiceGrpc.ChatServiceBlockingStub chatServiceBlockingStub;

	@Override
	public ChatDTO createChat(CreateChatDTO dto) {
		CreateChatRequest request = ChatServiceFactory.toCreateChatRequest(dto);
		CreateChatResponse response = chatServiceBlockingStub.createChat(request);

		return ChatServiceFactory.toChatDto(response);
	}

	@Override
	public ChatHistoryDTO getChatHistory(GetChatHistoryDTO dto) {
		GetChatHistoryRequest request = ChatServiceFactory.toGetChatHistoryRequest(dto);
		GetChatHistoryResponse response = chatServiceBlockingStub.getChatHistory(request);

		return ChatServiceFactory.toChatHistoryDto(response);
	}

	@Override
	public MessageDTO sendMessage(SendMessageDTO dto) {
		SendMessageRequest request = ChatServiceFactory.toSendMessageRequest(dto);
		SendMessageResponse response = chatServiceBlockingStub.sendMessage(request);

		return ChatServiceFactory.toMessageDTO(response.getMessage());
	}



	@Override public JobConfirmationDTO createJobConfirmation(
			CreateJobConfirmationDTO dto)
	{
		CreateJobConfirmationRequest request = ChatServiceFactory.toCreateJobConfirmationRequest(dto);
		CreateJobConfirmationResponse response = chatServiceBlockingStub.createJobConfirmation(request);

		return ChatServiceFactory.toJobConfirmationDTO(response);
	}

	@Override
	public ChatOverviewDTO getChatOverview(GetChatOverviewDTO dto) {
		GetChatOverviewRequest request = ChatServiceFactory.toGetChatOverviewRequest(dto);
		GetChatOverviewResponse response = chatServiceBlockingStub.getChatOverview(request);

		return ChatServiceFactory.toChatOverviewDTO(response);
	}


}
