package com.example.businessserver.dtos.chat;

public class SendMessageDTO {
	private int chatId;
	private int authorId;
	private String message;

	public SendMessageDTO(int chatId, int authorId, String message) {
		this.chatId = chatId;
		this.authorId = authorId;
		this.message = message;
	}

	public int getChatId() {
		return chatId;
	}

	public int getAuthorId() {
		return authorId;
	}

	public String getMessage() {
		return message;
	}
}
