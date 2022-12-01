package com.example.businessserver.services;

import com.example.businessserver.dtos.chat.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationAnswer;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationDTO;

public interface JobConfirmationServiceClient
{
  JobConfirmationDTO CreateJobConfirmation(CreateJobConfirmationDTO dto);
  JobConfirmationDTO answerJobConfirmation(JobConfirmationAnswer dto);
}
