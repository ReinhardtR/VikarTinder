package com.example.businessserver.services.factories;


import JobConfirmationService.CreateJobConfirmationRequest;
import JobConfirmationService.CreateJobConfirmationResponse;
import JobConfirmationService.JobConfirmationAnswerRequest;
import JobConfirmationService.JobConfirmationAnswerResponse;
import com.example.businessserver.dtos.chat.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationAnswer;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationDTO;

import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;

public class JobConfirmationServiceFactory {


  public static CreateJobConfirmationRequest toCreateJobConfirmationRequest(
      CreateJobConfirmationDTO dto)
  {
    if (ObjectIsNull(dto))
      return null;

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
    if (ObjectIsNull(response))
      return null;

    return new JobConfirmationDTO(
        response.getJobConfirmation().getId(),
        response.getJobConfirmation().getChatId(),
        response.getJobConfirmation().getSubstituteId(),
        response.getJobConfirmation().getEmployerId(),
        response.getJobConfirmation().getIsAccepted(),
      LocalDateTime.ofInstant(Instant.ofEpochSecond(response.getJobConfirmation().getCreatedAt().getSeconds()), ZoneId.of("UTC"))
    );
  }

  public static JobConfirmationDTO toJobConfirmationDTO(
      JobConfirmationAnswerResponse response)
  {
    if (ObjectIsNull(response))
      return null;

    return new JobConfirmationDTO(
        response.getJobConfirmation().getId(),
        response.getJobConfirmation().getChatId(),
        response.getJobConfirmation().getSubstituteId(),
        response.getJobConfirmation().getEmployerId(),
        response.getJobConfirmation().getIsAccepted(),
        LocalDateTime.ofInstant(Instant.ofEpochSecond(response.getJobConfirmation().getCreatedAt().getSeconds()), ZoneId.of("UTC"))
        );
  }

  public static JobConfirmationAnswerRequest toJobConfirmationAnswerRequest(
      JobConfirmationAnswer dto)
  {
    if (ObjectIsNull(dto))
      return null;

    return JobConfirmationAnswerRequest
        .newBuilder()
        .setId(dto.getId())
        .setChatId(dto.getChatId())
        .setIsAccepted(dto.getIsAccepted())
        .build();
  }

  private static boolean ObjectIsNull(Object o)
  {
    return o == null;
  }
}
