package com.example.businessserver.services;

import com.example.businessserver.dtos.JobConfirmation.AnswerJobConfirmationDTO;
import com.example.businessserver.dtos.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.JobConfirmation.JobConfirmationDTO;

public interface JobConfirmationServiceClient {
	JobConfirmationDTO createJobConfirmation(CreateJobConfirmationDTO dto);

	JobConfirmationDTO answerJobConfirmation(AnswerJobConfirmationDTO dto);

	JobConfirmationDTO getJobConfirmation(int chatId);
}
