package com.example.businessserver.dtos.chat;

public class MessageDTO {
	private int id;
	private int chatId;
	private int authorId;
	private String content;

	public MessageDTO(int id, int chatId, int authorId, String content) {
		this.id = id;
		this.chatId = chatId;
		this.authorId = authorId;
		this.content = content;
	}

	public int getId() {
		return id;
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
