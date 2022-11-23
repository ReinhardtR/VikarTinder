package com.example.businessserver.dtos.chat;

public class MessageDTO {
	private int chatId;
	private int authorId;
	private String message;

	public MessageDTO(int chatId, int authorId, String message) {
		this.chatId = chatId;
		this.authorId = authorId;
		this.message = message;
	}
}
