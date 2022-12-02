package com.example.businessserver.dtos.chat.JobConfirmation;

import com.google.type.DateTime;

public class CreateJobConfirmationDTO
{
  private int chatId;
  private int substituteId;
  private int EmployerId;

  private DateTime offeredAt;
  public CreateJobConfirmationDTO(int chatId, int substituteId, int EmployerId, DateTime offeredAt)
  {
    this.chatId = chatId;
    this.substituteId = substituteId;
    this.EmployerId = EmployerId;
    this.offeredAt = offeredAt;
  }

  public int getChatId()
  {
    return chatId;
  }

  public int getSubstituteId()
  {
    return substituteId;
  }

  public int getEmployerId()
  {
    return EmployerId;
  }
}
