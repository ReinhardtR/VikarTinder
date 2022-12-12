package com.example.businessserver.dtos.chat.overview;

public class BasicChatDTO {
	private int id;
	private int substituteId;
	private int employerId;

	public BasicChatDTO(int id, int substituteId, int employerId) {
		this.id = id;
		this.substituteId = substituteId;
		this.employerId = employerId;
	}

	public int getId() {
		return id;
	}

	public int getSubstituteId() {
		return substituteId;
	}

	public int getEmployerId() {
		return employerId;
	}
}
