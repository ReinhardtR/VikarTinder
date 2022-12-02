package com.example.businessserver.services.factories;


import JobConfirmationService.CreateJobConfirmationRequest;
import JobConfirmationService.CreateJobConfirmationResponse;
import JobConfirmationService.JobConfirmationAnswerRequest;
import com.example.businessserver.dtos.chat.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationAnswer;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationDTO;

public class JobConfirmationServiceFactory
{

  public static CreateJobConfirmationRequest toCreateJobConfirmationRequest(
      CreateJobConfirmationDTO dto)
  {
    return CreateJobConfirmationRequest
        .newBuilder()
        .setChatId(dto.getChatId())
        .setSubstituteId(dto.getSubstituteId())
        .setEmployerId(dto.getEmployerId())
        .build();
  }

  public static JobConfirmationDTO toJobConfirmationDTO(
      CreateJobConfirmationResponse response)
  {
    return new JobConfirmationDTO(
        response.getJobConfirmation().getId(),
        response.getJobConfirmation().getChatId(),
        response.getJobConfirmation().getSubstituteId(),
        response.getJobConfirmation().getEmployerId(),
        response.getJobConfirmation().getIsAccepted()
        response.getJobConfirmation().get
    );
  }

  public static JobConfirmationAnswerRequest toJobConfirmationAnswerRequest(
      JobConfirmationAnswer dto)
  {

  }
}
