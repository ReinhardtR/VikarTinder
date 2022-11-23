package com.example.businessserver.restcontrollers;

import com.example.businessserver.dtos.chat.*;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;

@RestController
@CrossOrigin
@RequestMapping("/chat")
public class ChatController {

	@PostMapping("/")
	public ChatDTO createChat(@RequestBody CreateChatDTO dto) {
		System.out.println("Chat created: " + dto);
		return new ChatDTO(1);
	}

	@GetMapping("/")
	public ChatHistoryDTO getChatHistory(@RequestBody GetChatHistoryDTO dto) {
		System.out.println("Chat history: " + dto);
		return new ChatHistoryDTO(new ArrayList<>());
	}

	@PostMapping("/message")
	public MessageDTO sendMessage(@RequestBody SendMessageDTO dto) {
		System.out.println("Message sent: " + dto);
		return new MessageDTO(dto.getChatId() ,dto.getAuthorId(), dto.getMessage());
	}
}
