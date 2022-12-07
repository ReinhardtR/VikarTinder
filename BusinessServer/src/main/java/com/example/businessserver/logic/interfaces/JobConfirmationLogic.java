package com.example.businessserver.logic.interfaces;

import com.example.businessserver.dtos.jobconfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.jobconfirmation.AnswerJobConfirmationDTO;
import com.example.businessserver.dtos.jobconfirmation.JobConfirmationDTO;

public interface JobConfirmationLogic {

	JobConfirmationDTO createJobConfirmation(CreateJobConfirmationDTO dto)
					throws Exception;

	JobConfirmationDTO answerJobConfirmation(AnswerJobConfirmationDTO dto);
}
