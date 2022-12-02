package com.example.businessserver.dtos.chat.JobConfirmation;

import com.google.type.DateTime;

import java.time.LocalDate;
import java.time.LocalDateTime;

public class JobConfirmationDTO
{
  private int id;
  private int chatId;
  private int substituteId;
  private int employerId;
  private boolean isAccepted;

  private LocalDateTime offeredAt;

  public JobConfirmationDTO(int id, int chatId, int substituteId,
      int employerId, boolean isAccepted, LocalDateTime offeredAt)
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

  public LocalDateTime getOfferedAt()
  {
    return offeredAt;
  }
}
