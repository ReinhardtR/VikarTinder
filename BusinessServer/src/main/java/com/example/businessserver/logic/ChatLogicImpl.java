package com.example.businessserver.logic;

import com.example.businessserver.dtos.chat.*;
import com.example.businessserver.services.ChatServiceClient;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class ChatLogicImpl implements ChatLogic {

	@Autowired
	private ChatServiceClient chatServiceClient;

	@Override
	public ChatDTO createChat(CreateChatDTO dto) {
		return chatServiceClient.createChat(dto);
	}

	@Override
	public ChatHistoryDTO getChatHistory(GetChatHistoryDTO dto) {
		return chatServiceClient.getChatHistory(dto);
	}

	@Override
	public MessageDTO sendMessage(SendMessageDTO dto) {
		return chatServiceClient.sendMessage(dto);
	}
}
