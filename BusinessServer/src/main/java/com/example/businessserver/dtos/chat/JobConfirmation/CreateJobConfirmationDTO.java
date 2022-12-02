package com.example.businessserver.dtos.chat.JobConfirmation;

public class CreateJobConfirmationDTO {
	private int chatId;
	private int substituteId;
	private int employerId;

	public CreateJobConfirmationDTO(int chatId, int substituteId, int employerId) {
		this.chatId = chatId;
		this.substituteId = substituteId;
		this.employerId = employerId;
	}

	public int getChatId() {
		return chatId;
	}

	public int getSubstituteId() {
		return substituteId;
	}

	public int getEmployerId() {
		return employerId;
	}
}
