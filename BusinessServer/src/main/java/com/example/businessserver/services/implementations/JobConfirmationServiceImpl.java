package com.example.businessserver.services.implementations;

import ChatService.ChatServiceGrpc;
import ChatService.CreateJobConfirmationRequest;
import ChatService.CreateJobConfirmationResponse;
import com.example.businessserver.dtos.chat.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationAnswer;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationDTO;
import com.example.businessserver.services.JobConfirmationServiceClient;
import com.example.businessserver.services.factories.ChatServiceFactory;
import net.devh.boot.grpc.client.inject.GrpcClient;

public class JobConfirmationServiceImpl implements JobConfirmationServiceClient
{
  @GrpcClient("grpc-server")
  private ChatServiceGrpc.ChatServiceBlockingStub chatServiceBlockingStub;


  @Override public JobConfirmationDTO CreateJobConfirmation(
      CreateJobConfirmationDTO dto)
  {
    CreateJobConfirmationRequest request = ChatServiceFactory.toCreateJobConfirmationRequest(dto);
    CreateJobConfirmationResponse response = chatServiceBlockingStub.createJobConfirmation(request);

    return ChatServiceFactory.toJobConfirmationDTO(response);
  }

  @Override public JobConfirmationDTO answerJobConfirmation(JobConfirmationAnswer dto)
  {
    JobConfirmationAnswerRequest request = ChatServiceFactory.toJobConfirmationAnswerRequest(dto);
    JobConfirmationAnswerResponse response = chatServiceBlockingStub.answerJobConfirmation(request);

    return ChatServiceFactory.toJobConfirmationDTO(response);
  }
}
