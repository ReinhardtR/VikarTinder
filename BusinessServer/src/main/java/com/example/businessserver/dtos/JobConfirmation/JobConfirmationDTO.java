package com.example.businessserver.dtos.JobConfirmation;

import JobConfirmationService.JobConfirmationStatus;

import java.time.LocalDateTime;

public class JobConfirmationDTO {
	private int id;
	private int chatId;
	private int substituteId;
	private int employerId;
	private JobConfirmationStatus isAccepted;

	private LocalDateTime offeredAt;

	public JobConfirmationDTO(int id, int chatId, int substituteId,
														int employerId, JobConfirmationStatus isAccepted, LocalDateTime offeredAt) {
		this.id = id;
		this.chatId = chatId;
		this.substituteId = substituteId;
		this.employerId = employerId;
		this.isAccepted = isAccepted;
		this.offeredAt = offeredAt;
	}

	public int getId() {
		return id;
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

	public JobConfirmationStatus getIsAccepted() {
		return isAccepted;
	}

	public LocalDateTime getOfferedAt() {
		return offeredAt;
	}
}
