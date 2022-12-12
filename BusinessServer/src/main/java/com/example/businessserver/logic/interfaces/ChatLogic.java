package com.example.businessserver.logic.interfaces;

import com.example.businessserver.dtos.chat.CreateChatDTO;
import com.example.businessserver.dtos.chat.history.ChatHistoryDTO;
import com.example.businessserver.dtos.chat.history.GetChatHistoryDTO;
import com.example.businessserver.dtos.chat.message.MessageDTO;
import com.example.businessserver.dtos.chat.message.SendMessageDTO;
import com.example.businessserver.dtos.chat.overview.BasicChatDTO;
import com.example.businessserver.dtos.chat.overview.ChatOverviewDTO;
import com.example.businessserver.dtos.chat.overview.GetChatOverviewByGigDTO;
import com.example.businessserver.dtos.chat.overview.GetChatOverviewByUserDTO;
import com.example.businessserver.dtos.chat.overview.gigs.EmployerGigsDTO;
import com.example.businessserver.dtos.chat.overview.gigs.GetEmployerGigsDTO;

public interface ChatLogic {
	BasicChatDTO createChat(CreateChatDTO dto);

	ChatHistoryDTO getChatHistory(GetChatHistoryDTO dto);

	MessageDTO sendMessage(SendMessageDTO dto);

	ChatOverviewDTO getChatOverviewByUser(GetChatOverviewByUserDTO dto);

	ChatOverviewDTO getChatOverviewByGig(GetChatOverviewByGigDTO dto);

	EmployerGigsDTO getEmployerGigs(GetEmployerGigsDTO dto);
}
