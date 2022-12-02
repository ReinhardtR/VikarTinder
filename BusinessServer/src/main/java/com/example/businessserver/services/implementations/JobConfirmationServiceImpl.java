package com.example.businessserver.services.implementations;


import JobConfirmationService.*;
import com.example.businessserver.dtos.chat.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationAnswer;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationDTO;
import com.example.businessserver.services.JobConfirmationServiceClient;
import com.example.businessserver.services.factories.ChatServiceFactory;
import com.example.businessserver.services.factories.JobConfirmationServiceFactory;
import net.devh.boot.grpc.client.inject.GrpcClient;

public class JobConfirmationServiceImpl implements JobConfirmationServiceClient
{
  @GrpcClient("grpc-server")
  private JobConfirmationServiceGrpc.JobConfirmationServiceBlockingStub jobConfirmationServiceBlockingStub;


  @Override public JobConfirmationDTO CreateJobConfirmation(
      CreateJobConfirmationDTO dto)
  {
    CreateJobConfirmationRequest request = JobConfirmationServiceFactory.toCreateJobConfirmationRequest(dto);
    CreateJobConfirmationResponse response = jobConfirmationServiceBlockingStub.createJobConfirmation(request);

    return JobConfirmationServiceFactory.toJobConfirmationDTO(response);
  }

  @Override public JobConfirmationDTO answerJobConfirmation(JobConfirmationAnswer dto)
  {
    JobConfirmationAnswerRequest request = JobConfirmationServiceFactory.toJobConfirmationAnswerRequest(dto);
    JobConfirmationAnswerResponse response = JobConfirmationServiceGrpc.answerJobConfirmation(request);

    return JobConfirmationServiceFactory.toJobConfirmationDTO(response);
  }
}
