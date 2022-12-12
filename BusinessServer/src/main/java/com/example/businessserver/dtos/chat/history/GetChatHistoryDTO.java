package com.example.businessserver.dtos.chat.history;

public class GetChatHistoryDTO {
	private int chatId;

	public GetChatHistoryDTO(int chatId) {
		this.chatId = chatId;
	}

	public int getChatId() {
		return chatId;
	}
}
