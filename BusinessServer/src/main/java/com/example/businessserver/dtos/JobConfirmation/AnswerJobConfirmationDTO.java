package com.example.businessserver.dtos.JobConfirmation;

public class AnswerJobConfirmationDTO
{
	private int id;
	private int chatId;
	private boolean isAccepted;

	public AnswerJobConfirmationDTO(int id, int chatId, boolean isAccepted) {
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

	public boolean getIsAccepted() {
		return isAccepted;
	}
}
