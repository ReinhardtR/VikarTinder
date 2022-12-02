package com.example.businessserver.services.implementations;


import JobConfirmationService.*;
import com.example.businessserver.dtos.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.JobConfirmation.AnswerJobConfirmationDTO;
import com.example.businessserver.dtos.JobConfirmation.JobConfirmationDTO;
import com.example.businessserver.services.JobConfirmationServiceClient;
import com.example.businessserver.services.factories.JobConfirmationServiceFactory;
import net.devh.boot.grpc.client.inject.GrpcClient;
import org.springframework.stereotype.Service;

@Service
public class JobConfirmationServiceImpl implements JobConfirmationServiceClient {
	@GrpcClient("grpc-server")
	private JobConfirmationServiceGrpc.JobConfirmationServiceBlockingStub jobConfirmationServiceBlockingStub;


	@Override
	public JobConfirmationDTO CreateJobConfirmation(CreateJobConfirmationDTO dto) {
		CreateJobConfirmationRequest request = JobConfirmationServiceFactory.toCreateJobConfirmationRequest(dto);
		CreateJobConfirmationResponse response = jobConfirmationServiceBlockingStub.createJobConfirmation(request);
		return JobConfirmationServiceFactory.toJobConfirmationDTO(response);
	}

	@Override
	public JobConfirmationDTO answerJobConfirmation(AnswerJobConfirmationDTO dto) {
		JobConfirmationAnswerRequest request = JobConfirmationServiceFactory.toJobConfirmationAnswerRequest(dto);
		JobConfirmationAnswerResponse response = jobConfirmationServiceBlockingStub.answerJobConfirmation(request);

		return JobConfirmationServiceFactory.toJobConfirmationDTO(response);

	}

	@Override public JobConfirmationDTO getJobConfirmation(int chatId)
	{
		GetJobConfirmationRequest request = JobConfirmationServiceFactory.toGetJobConfirmationRequest(chatId);
		GetJobConfirmationResponse response = jobConfirmationServiceBlockingStub.getJobConfirmation(request);

		return JobConfirmationServiceFactory.toJobConfirmationDTO(response);
	}
}
