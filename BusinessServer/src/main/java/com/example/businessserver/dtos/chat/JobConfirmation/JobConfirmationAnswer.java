package com.example.businessserver.dtos.chat.JobConfirmation;

public class JobConfirmationAnswer {
	private int id;
	private int chatId;
	private boolean isAccepted;

	public JobConfirmationAnswer(int id, int chatId, boolean isAccepted) {
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
