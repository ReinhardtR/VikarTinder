package com.example.businessserver.services.interfaces;

import com.example.businessserver.dtos.chat.*;
import com.example.businessserver.dtos.chat.message.MessageDTO;
import com.example.businessserver.dtos.chat.message.SendMessageDTO;

public interface ChatServiceClient {
	BasicChatDTO createChat(CreateChatDTO dto);

	ChatHistoryDTO getChatHistory(GetChatHistoryDTO dto);

	MessageDTO sendMessage(SendMessageDTO dto);

	ChatOverviewDTO getChatOverview(GetChatOverviewDTO dto);


}
