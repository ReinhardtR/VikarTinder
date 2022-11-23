package com.example.businessserver.dtos.chat;

public class GetChatOverviewDTO {
	private int userId;

	public GetChatOverviewDTO(int userId) {
		this.userId = userId;
	}

	public int getUserId() {
		return userId;
	}
}
