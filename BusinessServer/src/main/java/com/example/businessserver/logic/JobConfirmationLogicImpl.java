package com.example.businessserver.logic;

import com.example.businessserver.dtos.chat.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationAnswer;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationDTO;
import com.example.businessserver.services.JobConfirmationServiceClient;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class JobConfirmationLogicImpl implements JobConfirmationLogic {

	@Autowired
	private JobConfirmationServiceClient jobConfirmationServiceClient;


	@Override
	public JobConfirmationDTO createJobConfirmation(
					CreateJobConfirmationDTO dto) {
		return jobConfirmationServiceClient.CreateJobConfirmation(dto);
	}

	@Override
	public JobConfirmationDTO answerJobConfirmation(JobConfirmationAnswer dto) {
		return jobConfirmationServiceClient.answerJobConfirmation(dto);
	}
}
