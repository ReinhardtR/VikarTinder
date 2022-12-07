package com.example.businessserver.dtos.JobConfirmation;

import JobConfirmationService.JobConfirmationStatus;

public class AnswerJobConfirmationDTO {
	private int id;
	private int chatId;
	private JobConfirmationStatus status;

	public AnswerJobConfirmationDTO(int id, int chatId, JobConfirmationStatus status) {
		this.id = id;
		this.chatId = chatId;
		this.status = status;
	}

	public int getId() {
		return id;
	}

	public int getChatId() {
		return chatId;
	}

	public JobConfirmationStatus getStatus() {
		return status;
	}
}
