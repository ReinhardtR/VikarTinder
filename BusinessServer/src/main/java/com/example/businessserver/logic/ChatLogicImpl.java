package com.example.businessserver.logic;

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
import com.example.businessserver.logic.interfaces.ChatLogic;
import com.example.businessserver.services.interfaces.ChatServiceClient;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

//TODO: Validering - se meistertask
@Service
public class ChatLogicImpl implements ChatLogic {

	@Autowired
	private ChatServiceClient chatServiceClient;

	@Override
	public BasicChatDTO createChat(CreateChatDTO dto) {
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

	@Override
	public ChatOverviewDTO getChatOverviewByUser(GetChatOverviewByUserDTO dto) {
		return chatServiceClient.getChatOverviewByUser(dto);
	}

	@Override
	public ChatOverviewDTO getChatOverviewByGig(GetChatOverviewByGigDTO dto) {
		return chatServiceClient.getChatOverviewByGig(dto);
	}

	@Override
	public EmployerGigsDTO getEmployerGigs(GetEmployerGigsDTO dto) {
		return chatServiceClient.getEmployerGigs(dto);
	}
}
