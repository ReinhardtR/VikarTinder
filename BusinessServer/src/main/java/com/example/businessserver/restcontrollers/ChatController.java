package com.example.businessserver.restcontrollers;

import com.example.businessserver.dtos.chat.*;
import com.example.businessserver.logic.ChatLogic;
import com.example.businessserver.websockets.SocketHandler;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@CrossOrigin
@RequestMapping("/chat")
public class ChatController {

	@Autowired
	private ChatLogic chatLogic;

	@Autowired
	private SocketHandler socketHandler;

	@GetMapping("/{id}")
	public ChatOverviewDTO getChatOverview(@PathVariable int id) {
		return chatLogic.getChatOverview(new GetChatOverviewDTO(id));
	}

	@PostMapping("/")
	public ChatDTO createChat(@RequestBody CreateChatDTO dto) {
		System.out.println("CREATE CHAT");
		return chatLogic.createChat(dto);
	}

	@GetMapping("/history/{id}")
	public ChatHistoryDTO getChatHistory(@PathVariable int id) {
		System.out.println("GET CHAT HISTORY");
		return chatLogic.getChatHistory(new GetChatHistoryDTO(id));
	}

	@PostMapping("/message")
	public MessageDTO sendMessage(@RequestBody SendMessageDTO dto) {
		System.out.println("SEND MESSAGE");
		MessageDTO messageDTO = chatLogic.sendMessage(dto);

		// Send message to websocket for real-time message receiving
		socketHandler.sendMessage(messageDTO);

		return messageDTO;
	}
}
