package com.example.businessserver.dtos.chat.overview;

public class GetChatOverviewByGigDTO {
	private int gigId;

	public GetChatOverviewByGigDTO(int gigId) {
		this.gigId = gigId;
	}

	public int getGigId() {
		return gigId;
	}
}
