package com.example.businessserver.services.implementations;

import ChatService.*;
import com.example.businessserver.dtos.chat.*;
import com.example.businessserver.dtos.chat.message.MessageDTO;
import com.example.businessserver.dtos.chat.message.SendMessageDTO;
import com.example.businessserver.services.interfaces.ChatServiceClient;
import com.example.businessserver.services.factories.ChatServiceFactory;
import net.devh.boot.grpc.client.inject.GrpcClient;
import org.springframework.stereotype.Service;

@Service
public class ChatServiceImpl implements ChatServiceClient {

	@GrpcClient("grpc-server")
	private ChatServiceGrpc.ChatServiceBlockingStub chatServiceBlockingStub;

	@Override
	public BasicChatDTO createChat(CreateChatDTO dto) {
		CreateChatRequest request = ChatServiceFactory.toCreateChatRequest(dto);
		CreateChatResponse response = chatServiceBlockingStub.createChat(request);

		return ChatServiceFactory.toBasicChatDTO(response);
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


	@Override
	public ChatOverviewDTO getChatOverview(GetChatOverviewDTO dto) {
		GetChatOverviewRequest request = ChatServiceFactory.toGetChatOverviewRequest(dto);
		GetChatOverviewResponse response = chatServiceBlockingStub.getChatOverview(request);

		return ChatServiceFactory.toChatOverviewDTO(response);
	}


}
