package com.example.businessserver.dtos.chat;

import com.example.businessserver.dtos.JobConfirmation.JobConfirmationDTO;
import com.example.businessserver.dtos.chat.message.MessageDTO;

import java.util.List;

public class ChatHistoryDTO {
	private List<MessageDTO> messages;

	private List<JobConfirmationDTO> jobConfirmations;

	private int substituteId;
	private int employerId;

	public ChatHistoryDTO(List<MessageDTO> messages,
			List<JobConfirmationDTO> jobConfirmations, int substituteId,
			int employerId)
	{
		this.messages = messages;
		this.jobConfirmations = jobConfirmations;
		this.substituteId = substituteId;
		this.employerId = employerId;
	}

	public List<MessageDTO> getMessages()
	{
		return messages;
	}

	public List<JobConfirmationDTO> getJobConfirmations()
	{
		return jobConfirmations;
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
