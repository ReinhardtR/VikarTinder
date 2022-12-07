package com.example.businessserver.services.interfaces;

import com.example.businessserver.dtos.jobconfirmation.AnswerJobConfirmationDTO;
import com.example.businessserver.dtos.jobconfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.jobconfirmation.JobConfirmationDTO;

public interface JobConfirmationServiceClient {
	JobConfirmationDTO createJobConfirmation(CreateJobConfirmationDTO dto);

	JobConfirmationDTO answerJobConfirmation(AnswerJobConfirmationDTO dto);

	JobConfirmationDTO getJobConfirmation(int chatId);
}
