package com.example.businessserver.logic;

import com.example.businessserver.dtos.chat.*;

public interface ChatLogic {
	ChatDTO createChat(CreateChatDTO dto);

	ChatHistoryDTO getChatHistory(GetChatHistoryDTO dto);

	MessageDTO sendMessage(SendMessageDTO dto);

	ChatOverviewDTO getChatOverview(GetChatOverviewDTO dto);
}
