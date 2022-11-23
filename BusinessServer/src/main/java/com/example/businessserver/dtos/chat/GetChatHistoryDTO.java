package com.example.businessserver.dtos.chat;

public class GetChatHistoryDTO {
	private int id;

	public GetChatHistoryDTO(int id) {
		this.id = id;
	}

	public int getId() {
		return id;
	}
}
