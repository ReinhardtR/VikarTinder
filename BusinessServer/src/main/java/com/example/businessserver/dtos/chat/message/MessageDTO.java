package com.example.businessserver.dtos.chat.message;

import java.time.LocalDateTime;

public class MessageDTO {
	private int id;
	private int chatId;
	private int authorId;
	private String content;
	private LocalDateTime createdAt;

	public MessageDTO(int id, int chatId, int authorId, String content,
			LocalDateTime createdAt)
	{
		this.id = id;
		this.chatId = chatId;
		this.authorId = authorId;
		this.content = content;
		this.createdAt = createdAt;
	}

	public int getId()
	{
		return id;
	}

	public int getChatId()
	{
		return chatId;
	}

	public int getAuthorId()
	{
		return authorId;
	}

	public String getContent()
	{
		return content;
	}

	public LocalDateTime getCreatedAt()
	{
		return createdAt;
	}
}
