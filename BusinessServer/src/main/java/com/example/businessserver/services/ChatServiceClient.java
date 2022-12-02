package com.example.businessserver.services;

import com.example.businessserver.dtos.chat.*;
import com.example.businessserver.dtos.chat.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationDTO;

public interface ChatServiceClient {
	ChatDTO createChat(CreateChatDTO dto);

	ChatHistoryDTO getChatHistory(GetChatHistoryDTO dto);

	MessageDTO sendMessage(SendMessageDTO dto);

	ChatOverviewDTO getChatOverview(GetChatOverviewDTO dto);


}
