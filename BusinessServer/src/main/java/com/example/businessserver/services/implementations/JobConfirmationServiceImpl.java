package com.example.businessserver.services.implementations;


import JobConfirmationService.*;
import com.example.businessserver.dtos.jobconfirmation.AnswerJobConfirmationDTO;
import com.example.businessserver.dtos.jobconfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.jobconfirmation.JobConfirmationDTO;
import com.example.businessserver.services.interfaces.JobConfirmationServiceClient;
import com.example.businessserver.services.factories.JobConfirmationServiceFactory;
import net.devh.boot.grpc.client.inject.GrpcClient;
import org.springframework.stereotype.Service;

@Service
public class JobConfirmationServiceImpl implements JobConfirmationServiceClient {
	@GrpcClient("grpc-server")
	private JobConfirmationServiceGrpc.JobConfirmationServiceBlockingStub jobConfirmationServiceBlockingStub;


	@Override
	public JobConfirmationDTO createJobConfirmation(CreateJobConfirmationDTO dto) {
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

	@Override
	public JobConfirmationDTO getJobConfirmation(int chatId) {
		GetJobConfirmationRequest request = JobConfirmationServiceFactory.toGetJobConfirmationRequest(chatId);
		GetJobConfirmationResponse response = jobConfirmationServiceBlockingStub.getJobConfirmation(request);

		return JobConfirmationServiceFactory.toJobConfirmationDTO(response);
	}
}
