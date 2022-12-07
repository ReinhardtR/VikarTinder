package com.example.businessserver.logic;

import com.example.businessserver.dtos.chat.*;
import com.example.businessserver.dtos.chat.message.MessageDTO;
import com.example.businessserver.dtos.chat.message.SendMessageDTO;

public interface ChatLogic {
	BasicChatDTO createChat(CreateChatDTO dto);

	ChatHistoryDTO getChatHistory(GetChatHistoryDTO dto);

	MessageDTO sendMessage(SendMessageDTO dto);

	ChatOverviewDTO getChatOverview(GetChatOverviewDTO dto);
}
