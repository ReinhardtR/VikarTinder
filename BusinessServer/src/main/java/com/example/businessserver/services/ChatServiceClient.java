package com.example.businessserver.services;

import com.example.businessserver.dtos.chat.*;

public interface ChatServiceClient {
	ChatDTO createChat(CreateChatDTO dto);

	ChatHistoryDTO getChatHistory(GetChatHistoryDTO dto);

	MessageDTO sendMessage(SendMessageDTO dto);

	JobConfirmationDTO createJobConfirmation(CreateJobConfirmationDTO dto);

	ChatOverviewDTO getChatOverview(GetChatOverviewDTO dto);


}
