package com.example.businessserver.dtos.chat;

public class JobConfirmationDTO
{
  private int id;
  private int chatId;
  private int substituteId;
  private int employerId;

  private boolean isAccepted;

  public JobConfirmationDTO(int id, int chatId, int substituteId,
      int employerId, boolean isAccepted)
  {
    this.id = id;
    this.chatId = chatId;
    this.substituteId = substituteId;
    this.employerId = employerId;
    this.isAccepted = isAccepted;
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
}
