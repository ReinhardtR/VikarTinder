package com.example.businessserver.dtos.chat;

import java.util.List;

public class GetChatHistoryDTO {
	private int id;
	private List<MessageDTO> messages;

	public GetChatHistoryDTO(int id, List<MessageDTO> messages) {
		this.id = id;
		this.messages = messages;
	}

	public int getId() {
		return id;
	}

	public List<MessageDTO> getMessages() {
		return messages;
	}
}
