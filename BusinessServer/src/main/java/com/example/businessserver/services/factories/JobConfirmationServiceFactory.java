package com.example.businessserver.services.factories;

import ChatService.CreateJobConfirmationRequest;
import ChatService.CreateJobConfirmationResponse;
import com.example.businessserver.dtos.chat.JobConfirmation.CreateJobConfirmationDTO;
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
        response.getId(),
        response.getChatId(),
        response.getSubstituteId(),
        response.getEmployerId(),
        response.getIsAccepted()
    );
  }
}
