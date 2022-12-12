package com.example.businessserver.dtos.chat.history;

import com.example.businessserver.dtos.chat.message.MessageDTO;
import com.example.businessserver.dtos.jobconfirmation.JobConfirmationDTO;

import java.util.List;

public class ChatHistoryDTO {
	private List<MessageDTO> messages;

	private JobConfirmationDTO jobConfirmation;

	private ChatEmployerDTO employer;

	private ChatSubstituteDTO substitute;

	public ChatHistoryDTO(List<MessageDTO> messages, JobConfirmationDTO jobConfirmation, ChatEmployerDTO chatEmployer, ChatSubstituteDTO chatSubstitute) {
		this.messages = messages;
		this.jobConfirmation = jobConfirmation;
		this.employer = chatEmployer;
		this.substitute = chatSubstitute;
	}

	public List<MessageDTO> getMessages() {
		return messages;
	}

	public JobConfirmationDTO getJobConfirmation() {
		return jobConfirmation;
	}

	public ChatEmployerDTO getEmployer() {
		return employer;
	}

	public ChatSubstituteDTO getSubstitute() {
		return substitute;
	}
}
