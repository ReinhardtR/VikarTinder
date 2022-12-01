package com.example.businessserver.logic;

import com.example.businessserver.dtos.chat.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationAnswer;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationDTO;

public interface JobConfirmationLogic
{

  JobConfirmationDTO createJobConfirmation(CreateJobConfirmationDTO dto);

  JobConfirmationDTO answerJobConfirmation(JobConfirmationAnswer dto);
}
