package com.example.businessserver.dtos.chat;

public class CreateJobConfirmationDTO
{
  private int chatId;
  private int substituteId;
  private int EmployerId;


  public CreateJobConfirmationDTO(int chatId, int substituteId, int EmployerId)
  {
    this.chatId = chatId;
    this.substituteId = substituteId;
    this.EmployerId = EmployerId;
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
