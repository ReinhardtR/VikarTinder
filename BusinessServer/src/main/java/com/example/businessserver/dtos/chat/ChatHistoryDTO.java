package com.example.businessserver.dtos.chat;

import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationDTO;

import java.util.List;

public class ChatHistoryDTO {
	private List<MessageDTO> messages;

	private List<JobConfirmationDTO> jobConfirmations;

	public ChatHistoryDTO(List<MessageDTO> messages,
			List<JobConfirmationDTO> jobConfirmations)
	{
		this.messages = messages;
		this.jobConfirmations = jobConfirmations;
	}

	public List<MessageDTO> getMessages()
	{
		return messages;
	}

	public List<JobConfirmationDTO> getJobConfirmations()
	{
		return jobConfirmations;
	}
}
