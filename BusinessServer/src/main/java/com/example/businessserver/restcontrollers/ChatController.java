package com.example.businessserver.restcontrollers;

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
import com.example.businessserver.websockets.SocketHandler;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/chat")
public class ChatController {

	@Autowired
	private ChatLogic chatLogic;

	@Autowired
	private SocketHandler socketHandler;

	@GetMapping("/user/{userId}")
	public ChatOverviewDTO getChatOverviewByUser(@PathVariable int userId) {
		return chatLogic.getChatOverviewByUser(new GetChatOverviewByUserDTO(userId));
	}

	@GetMapping("/gig/{gigId}")
	public ChatOverviewDTO getChatOverviewByGig(@PathVariable int gigId) {
		return chatLogic.getChatOverviewByGig(new GetChatOverviewByGigDTO(gigId));
	}

	@GetMapping("/gigs/{employerId}")
	public EmployerGigsDTO getEmployerGigs(@PathVariable int employerId) {
		return chatLogic.getEmployerGigs(new GetEmployerGigsDTO(employerId));
	}

	@PostMapping("/")
	public BasicChatDTO createChat(@RequestBody CreateChatDTO dto) {
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
		MessageDTO messageDTO = chatLogic.sendMessage(dto);

		// Send message to websocket for real-time message receiving
		socketHandler.sendChatMessage(messageDTO);

		return messageDTO;
	}
}
