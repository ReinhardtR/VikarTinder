package com.example.businessserver.dtos.chat.JobConfirmation;

import com.google.type.DateTime;

public class JobConfirmationAnswer
{
  private int id;
  private int chatId;
  private boolean isAccepted;

  private DateTime acceptedAt;

  public JobConfirmationAnswer(int id, int chatId, boolean isAccepted, DateTime acceptedAt)
  {
    this.id = id;
    this.chatId = chatId;
    this.isAccepted = isAccepted;
    this.acceptedAt = acceptedAt;
  }


  public int getId()
  {
    return id;
  }

  public int getChatId()
  {
    return chatId;
  }

  public boolean isAccepted()
  {
    return isAccepted;
  }

  public DateTime getAcceptedAt()
  {
    return acceptedAt;
  }
}
