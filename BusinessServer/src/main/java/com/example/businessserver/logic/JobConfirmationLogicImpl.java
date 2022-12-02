package com.example.businessserver.logic;

import com.example.businessserver.dtos.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.JobConfirmation.AnswerJobConfirmationDTO;
import com.example.businessserver.dtos.JobConfirmation.JobConfirmationDTO;
import com.example.businessserver.services.JobConfirmationServiceClient;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class JobConfirmationLogicImpl implements JobConfirmationLogic {

	@Autowired
	private JobConfirmationServiceClient jobConfirmationServiceClient;


	@Override
	public JobConfirmationDTO createJobConfirmation(CreateJobConfirmationDTO dto) {
		//Hvis der er en jobrequest på denne chat, så skal der checkes om den er true.
		//Hvis den ikke er "unanswered", så skal der laves en jobconfirmation

		//Hvis der ikke er en jobrequest på denne chat, så skal der laves en jobconfirmation


		//Get Jobconfirmation by chatId
		JobConfirmationDTO jobConfirmationToBeReplaced = jobConfirmationServiceClient.getJobConfirmation(dto.getChatId());



		if (JobRequestIsPresent(jobConfirmationToBeReplaced) && JobRequestIsNotDeclined(jobConfirmationToBeReplaced)) {

		}

		return jobConfirmationServiceClient.CreateJobConfirmation(dto);

	}

	private boolean JobRequestIsNotDeclined(JobConfirmationDTO jobConfirmationToBeReplaced)
	{
		return jobConfirmationToBeReplaced.
	}

	private boolean JobRequestIsPresent(JobConfirmationDTO jobConfirmationToBeReplaced)
	{
		return jobConfirmationToBeReplaced != null;
	}

	@Override
	public JobConfirmationDTO answerJobConfirmation(AnswerJobConfirmationDTO dto) {
		return jobConfirmationServiceClient.answerJobConfirmation(dto);
	}



}
