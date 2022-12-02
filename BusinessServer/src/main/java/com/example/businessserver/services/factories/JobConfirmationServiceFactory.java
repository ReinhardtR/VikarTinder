package com.example.businessserver.services.factories;



import JobConfirmationService.CreateJobConfirmationRequest;
import JobConfirmationService.CreateJobConfirmationResponse;
import JobConfirmationService.JobConfirmationAnswerRequest;
import JobConfirmationService.JobConfirmationAnswerResponse;
import com.example.businessserver.dtos.chat.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationAnswer;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationDTO;
import com.google.type.DateTime;
import com.google.type.DateTimeOrBuilder;

import java.time.Instant;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.ZoneId;

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
        response.getJobConfirmation().getIsAccepted(),
      LocalDateTime.ofInstant(Instant.ofEpochSecond(response.getJobConfirmation().getCreatedAt().getSeconds()), ZoneId.of("UTC"))
    );
  }

  public static JobConfirmationDTO toJobConfirmationDTO(
      JobConfirmationAnswerResponse response)
  {
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
  return null;
  }
}
