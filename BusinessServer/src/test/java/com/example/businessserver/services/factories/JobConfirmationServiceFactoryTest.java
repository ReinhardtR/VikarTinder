package com.example.businessserver.services.factories;

import JobConfirmationService.*;
import com.example.businessserver.dtos.JobConfirmation.AnswerJobConfirmationDTO;
import com.example.businessserver.dtos.JobConfirmation.CreateJobConfirmationDTO;
import io.swagger.v3.oas.annotations.Parameter;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.EmptySource;
import org.junit.jupiter.params.provider.MethodSource;

import java.sql.Timestamp;
import java.time.Instant;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.stream.Stream;

import static org.junit.jupiter.api.Assertions.*;

class JobConfirmationServiceFactoryTest
{

  @ParameterizedTest
  @MethodSource()
  void toCreateJobConfirmationRequest(int chatId, int substituteId, int employerId)
  {
    CreateJobConfirmationDTO dto = new CreateJobConfirmationDTO(chatId, substituteId, employerId);
    CreateJobConfirmationRequest request = JobConfirmationServiceFactory.toCreateJobConfirmationRequest(dto);
    assertEquals(chatId, request.getChatId());
    assertEquals(substituteId, request.getSubstituteId());
    assertEquals(employerId, request.getEmployerId());
  }

  private static Stream<Object> toCreateJobConfirmationRequest()
  {
    return Stream.of(
      new Object[] {1,2,3},
      new Object[] {-1,-2,3},
      new Object[] {0,0,0}
    );
  }


  @ParameterizedTest
  @MethodSource()
  void toJobConfirmationDTO(int cdId, int chatId, int employerId, int substituteId, JobConfirmationStatus status, LocalDateTime localDateTime)
  {
    JobConfirmationObject object = JobConfirmationObject.newBuilder()
      .setChatId(chatId)
      .setEmployerId(employerId)
      .setSubstituteId(substituteId)
      .setStatus(status)
      .setCreatedAt(com.google.protobuf.Timestamp.newBuilder().setNanos(
          localDateTime.getNano()).build())
      .build();

    CreateJobConfirmationResponse response = CreateJobConfirmationResponse.newBuilder()
      .setJobConfirmation(object)
      .build();

    com.example.businessserver.dtos.JobConfirmation.JobConfirmationDTO dto =
        JobConfirmationServiceFactory.toJobConfirmationDTO(response);

    assertEquals(cdId, dto.getId());
    assertEquals(chatId, dto.getChatId());
    assertEquals(employerId, dto.getEmployerId());
    assertEquals(substituteId, dto.getSubstituteId());
    assertEquals(status, dto.getStatus());
    assertEquals(localDateTime, dto.getOfferedAt());
  }


  private static Stream<Object> toJobConfirmationDTO()
  {
    return Stream.of(
      new Object[] {1,2,3,4,JobConfirmationStatus.ACCEPTED, LocalDateTime.now()},
      new Object[] {0,Integer.MAX_VALUE,Integer.MIN_VALUE,5,JobConfirmationStatus.DECLINED, LocalDateTime.now()},
      new Object[] {-1,-2,8,-5,JobConfirmationStatus.UNANSWERED, LocalDateTime.now()}
    );
  }

  @ParameterizedTest
  @MethodSource()
  void ToJobConfirmationDTOFromResponse(int chatId, int employerId, int substituteId, JobConfirmationStatus status)
  {
    JobConfirmationObject object = JobConfirmationObject.newBuilder()
      .setChatId(chatId)
      .setEmployerId(employerId)
      .setSubstituteId(substituteId)
      .setStatus(status)
      .build();

    GetJobConfirmationResponse response = GetJobConfirmationResponse.newBuilder()
      .setJobConfirmation(object)
      .build();

    com.example.businessserver.dtos.JobConfirmation.JobConfirmationDTO dto =
        JobConfirmationServiceFactory.toJobConfirmationDTO(response);

    assertEquals(chatId, dto.getChatId());
    assertEquals(employerId, dto.getEmployerId());
    assertEquals(substituteId, dto.getSubstituteId());
    assertEquals(status, dto.getStatus());
  }

  private static Stream<Object> ToJobConfirmationDTOFromResponse()
  {
    return Stream.of(
      new Object[] {1,2,3,JobConfirmationStatus.ACCEPTED},
      new Object[] {0,Integer.MAX_VALUE,Integer.MIN_VALUE,JobConfirmationStatus.DECLINED},
      new Object[] {-1,-2,8,JobConfirmationStatus.UNANSWERED}
    );
  }

  @ParameterizedTest
  @MethodSource()
  void toJobConfirmationAnswerRequest(int jcId, int chatId, JobConfirmationStatus status)
  {
    AnswerJobConfirmationDTO dto = new AnswerJobConfirmationDTO(jcId, chatId, status);

    JobConfirmationAnswerRequest request = JobConfirmationServiceFactory.toJobConfirmationAnswerRequest(dto);

    assertEquals(jcId, request.getId());
    assertEquals(chatId, request.getChatId());
    assertEquals(status, request.getIsAccepted());
  }
  private static Stream<Object> toJobConfirmationAnswerRequest()
  {
    return Stream.of(
      new Object[] {1,2,JobConfirmationStatus.ACCEPTED},
      new Object[] {0,Integer.MAX_VALUE,JobConfirmationStatus.DECLINED},
      new Object[] {-1,-2,JobConfirmationStatus.UNANSWERED}
    );
  }

  @ParameterizedTest
  @MethodSource()
  void toGetJobConfirmationRequest(int chatId)
  {
    GetJobConfirmationRequest request = JobConfirmationServiceFactory.toGetJobConfirmationRequest(chatId);
    assertEquals(chatId, request.getId());
  }
  private static Stream<Object> toGetJobConfirmationRequest()
  {
    return Stream.of(
      new Object[] {1},
      new Object[] {Integer.MAX_VALUE},
      new Object[] {-1}
    );
  }


  @ParameterizedTest
  @MethodSource()
  void ToJobConfirmationDTOFromJobConfirmationResponse(int chatId, int empId, int subId, JobConfirmationStatus status)
  {
    JobConfirmationObject object = JobConfirmationObject.newBuilder()
      .setChatId(chatId)
      .setEmployerId(empId)
      .setSubstituteId(subId)
      .setStatus(status)
      .build();

    GetJobConfirmationResponse response = GetJobConfirmationResponse.newBuilder()
        .setJobConfirmation(object)
        .build();

    com.example.businessserver.dtos.JobConfirmation.JobConfirmationDTO dto = JobConfirmationServiceFactory.toJobConfirmationDTO(response);

    assertEquals(chatId, dto.getChatId());
    assertEquals(empId, dto.getEmployerId());
    assertEquals(subId, dto.getSubstituteId());
    assertEquals(status, dto.getStatus());
  }
  private static Stream<Object> ToJobConfirmationDTOFromJobConfirmationResponse()
  {
    return Stream.of(
      new Object[] {1,2,3,JobConfirmationStatus.ACCEPTED},
      new Object[] {0,Integer.MAX_VALUE,Integer.MIN_VALUE,JobConfirmationStatus.DECLINED},
      new Object[] {-1,-2,8,JobConfirmationStatus.UNANSWERED}
    );
  }
}