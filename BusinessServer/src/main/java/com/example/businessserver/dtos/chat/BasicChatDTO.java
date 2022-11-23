package com.example.businessserver.dtos.chat;

import java.util.List;

public class BasicChatDTO {
	private int id;
	private List<Integer> userIds;
	private List<MessageDTO> messages;

	public BasicChatDTO(int id, List<Integer> userIds, List<MessageDTO> messages) {
		this.id = id;
		this.userIds = userIds;
		this.messages = messages;
	}

	public int getId() {
		return id;
	}

	public List<Integer> getUserIds() {
		return userIds;
	}

	public List<MessageDTO> getMessages() {
		return messages;
	}
}
