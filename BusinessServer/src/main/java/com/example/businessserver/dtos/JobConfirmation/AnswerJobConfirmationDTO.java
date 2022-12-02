package com.example.businessserver.dtos.JobConfirmation;

import JobConfirmationService.JobConfirmationStatus;

public class AnswerJobConfirmationDTO {
	private int id;
	private int chatId;
	private JobConfirmationStatus isAccepted;

	public AnswerJobConfirmationDTO(int id, int chatId, JobConfirmationStatus isAccepted) {
		this.id = id;
		this.chatId = chatId;
		this.isAccepted = isAccepted;
	}

	public int getId() {
		return id;
	}

	public int getChatId() {
		return chatId;
	}

	public JobConfirmationStatus getIsAccepted() {
		return isAccepted;
	}
}
