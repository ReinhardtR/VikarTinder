package com.example.businessserver.dtos.jobconfirmation;

import java.time.LocalDateTime;

public class JobConfirmationDTO {
	private int id;
	private int chatId;
	private int substituteId;
	private int employerId;
	private JobConfirmationStatus status;

	private LocalDateTime offeredAt;

	public JobConfirmationDTO(int id, int chatId, int substituteId,
														int employerId, JobConfirmationStatus status, LocalDateTime offeredAt) {
		this.id = id;
		this.chatId = chatId;
		this.substituteId = substituteId;
		this.employerId = employerId;
		this.status = status;
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

	public JobConfirmationStatus getStatus() {
		return status;
	}

	public LocalDateTime getOfferedAt() {
		return offeredAt;
	}
}
