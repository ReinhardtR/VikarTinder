package com.example.businessserver.dtos.chat;

import com.example.businessserver.dtos.JobConfirmation.JobConfirmationDTO;
import com.example.businessserver.dtos.chat.message.MessageDTO;

import java.util.List;

public class ChatHistoryDTO {
	private List<MessageDTO> messages;

	private JobConfirmationDTO jobConfirmation;

	private int substituteId;
	private int employerId;

	public ChatHistoryDTO(List<MessageDTO> messages,
			JobConfirmationDTO jobConfirmation, int substituteId, int employerId)
	{
		this.messages = messages;
		this.jobConfirmation = jobConfirmation;
		this.substituteId = substituteId;
		this.employerId = employerId;
	}

	public List<MessageDTO> getMessages()
	{
		return messages;
	}

	public JobConfirmationDTO getJobConfirmation()
	{
		return jobConfirmation;
	}

	public int getSubstituteId()
	{
		return substituteId;
	}

	public int getEmployerId()
	{
		return employerId;
	}
}
