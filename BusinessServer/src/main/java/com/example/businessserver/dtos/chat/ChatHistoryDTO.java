package com.example.businessserver.dtos.chat;

import java.util.List;

public class ChatHistoryDTO {
	private List<MessageDTO> messages;

	public ChatHistoryDTO(List<MessageDTO> messages) {
		this.messages = messages;
	}

	public List<MessageDTO> getMessages() {
		return messages;
	}
}
