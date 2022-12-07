package com.example.businessserver.dtos.chat.message;

public class SendMessageDTO {
	private int chatId;
	private int authorId;
	private String content;

	public SendMessageDTO(int chatId, int authorId, String content) {
		this.chatId = chatId;
		this.authorId = authorId;
		this.content = content;
	}

	public int getChatId() {
		return chatId;
	}

	public int getAuthorId() {
		return authorId;
	}

	public String getContent() {
		return content;
	}
}
