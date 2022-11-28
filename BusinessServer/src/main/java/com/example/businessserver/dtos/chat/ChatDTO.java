package com.example.businessserver.dtos.chat;

import java.util.List;

public class ChatDTO {
	private int chatId;


	public ChatDTO(int chatId) {
		this.chatId = chatId;

	}


	public int getChatId() {
		return chatId;
	}
}
