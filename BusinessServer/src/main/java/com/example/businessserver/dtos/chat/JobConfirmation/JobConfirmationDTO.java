package com.example.businessserver.dtos.chat.JobConfirmation;

import com.google.type.DateTime;

public class JobConfirmationDTO
{
  private int id;
  private int chatId;
  private int substituteId;
  private int employerId;
  private boolean isAccepted;

  private DateTime offeredAt;

  public JobConfirmationDTO(int id, int chatId, int substituteId,
      int employerId, boolean isAccepted, DateTime offeredAt)
  {
    this.id = id;
    this.chatId = chatId;
    this.substituteId = substituteId;
    this.employerId = employerId;
    this.isAccepted = isAccepted;
    this.offeredAt = offeredAt;
  }

  public int getId()
  {
    return id;
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
    return employerId;
  }

  public boolean isAccepted()
  {
    return isAccepted;
  }

  public DateTime getOfferedAt()
  {
    return offeredAt;
  }
}
