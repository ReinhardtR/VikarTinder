package com.example.businessserver.logic;

import com.example.businessserver.dtos.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.JobConfirmation.AnswerJobConfirmationDTO;
import com.example.businessserver.dtos.JobConfirmation.JobConfirmationDTO;

public interface JobConfirmationLogic
{

  JobConfirmationDTO createJobConfirmation(CreateJobConfirmationDTO dto)
      throws Exception;

  JobConfirmationDTO answerJobConfirmation(AnswerJobConfirmationDTO dto);
}
