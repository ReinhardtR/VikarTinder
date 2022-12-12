package com.example.businessserver.dtos.chat.overview;

public class GetChatOverviewByUserDTO {
	private int userId;

	public GetChatOverviewByUserDTO(int userId) {
		this.userId = userId;
	}

	public int getUserId() {
		return userId;
	}
}
