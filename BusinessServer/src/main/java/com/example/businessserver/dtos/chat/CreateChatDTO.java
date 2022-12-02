package com.example.businessserver.dtos.chat;

import com.fasterxml.jackson.annotation.JsonProperty;

import java.util.List;

public class CreateChatDTO {
	private int substituteId;
	private int employerId;

	public CreateChatDTO(int substituteId, int employerId)
	{
		this.substituteId = substituteId;
		this.employerId = employerId;
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
