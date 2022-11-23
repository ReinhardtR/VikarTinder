package com.example.businessserver.restcontrollers;

import com.example.businessserver.dtos.chat.*;
import com.example.businessserver.logic.ChatLogic;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@CrossOrigin
@RequestMapping("/chat")
public class ChatController {

	@Autowired
	private ChatLogic chatLogic;

	@PostMapping("/")
	public ChatDTO createChat(@RequestBody CreateChatDTO dto) {
		System.out.println("CREATE CHAT");
		return chatLogic.createChat(dto);
	}

	@GetMapping("/")
	public ChatHistoryDTO getChatHistory(@RequestBody GetChatHistoryDTO dto) {
		System.out.println("GET CHAT HISTORY");
		return chatLogic.getChatHistory(dto);
	}

	@PostMapping("/message")
	public MessageDTO sendMessage(@RequestBody SendMessageDTO dto) {
		System.out.println("SEND MESSAGE");
		return chatLogic.sendMessage(dto);
	}

	@GetMapping("/overview")
	public ChatOverviewDTO getChatOverview(@RequestBody GetChatOverviewDTO dto) {
		System.out.println("GET CHAT OVERVIEW");
		return chatLogic.getChatOverview(dto);
	}
}
