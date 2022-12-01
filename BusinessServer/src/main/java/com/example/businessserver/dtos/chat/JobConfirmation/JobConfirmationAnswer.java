package com.example.businessserver.dtos.chat.JobConfirmation;

public class JobConfirmationAnswer
{
  private int chatId;
  private boolean isAccepted;

  public JobConfirmationAnswer(int chatID, boolean isAccepted)
  {
    this.chatId = chatID;
    this.isAccepted = isAccepted;
  }

  public int getChatId()
  {
    return chatId;
  }

  public boolean isAccepted()
  {
    return isAccepted;
  }
}
