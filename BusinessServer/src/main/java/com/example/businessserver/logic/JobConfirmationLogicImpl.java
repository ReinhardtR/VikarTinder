package com.example.businessserver.logic;

import JobConfirmationService.JobConfirmationStatus;
import com.example.businessserver.dtos.JobConfirmation.AnswerJobConfirmationDTO;
import com.example.businessserver.dtos.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.JobConfirmation.JobConfirmationDTO;
import com.example.businessserver.services.JobConfirmationServiceClient;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class JobConfirmationLogicImpl implements JobConfirmationLogic {

	@Autowired
	private JobConfirmationServiceClient jobConfirmationServiceClient;

	@Override
	public JobConfirmationDTO createJobConfirmation(CreateJobConfirmationDTO dto) throws Exception {
		JobConfirmationDTO jobConfirmationToBeReplaced = jobConfirmationServiceClient.getJobConfirmation(dto.getChatId());

		if (JobRequestIsPresent(jobConfirmationToBeReplaced) && !JobRequestIsDeclined(jobConfirmationToBeReplaced)) {
			throw new Exception("Job request already exists");
		}

		return jobConfirmationServiceClient.createJobConfirmation(dto);
	}

	private boolean JobRequestIsDeclined(JobConfirmationDTO jobConfirmationToBeReplaced) {
		return jobConfirmationToBeReplaced.getStatus().equals(JobConfirmationStatus.DECLINED);
	}

	private boolean JobRequestIsPresent(JobConfirmationDTO jobConfirmationToBeReplaced) {
		return jobConfirmationToBeReplaced != null;
	}

	@Override
	public JobConfirmationDTO answerJobConfirmation(AnswerJobConfirmationDTO dto) {
		return jobConfirmationServiceClient.answerJobConfirmation(dto);
	}
}
