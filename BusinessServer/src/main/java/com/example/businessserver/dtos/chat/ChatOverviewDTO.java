package com.example.businessserver.dtos.chat;

import java.util.List;

public class ChatOverviewDTO {
	private List<BasicChatDTO> chats;

	public ChatOverviewDTO(List<BasicChatDTO> chats) {
		this.chats = chats;
	}

	public List<BasicChatDTO> getChats() {
		return chats;
	}
}
