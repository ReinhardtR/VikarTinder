package com.example.businessserver.services.factories;


import JobConfirmationService.*;
import com.example.businessserver.dtos.jobconfirmation.AnswerJobConfirmationDTO;
import com.example.businessserver.dtos.jobconfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.jobconfirmation.JobConfirmationDTO;
import com.example.businessserver.dtos.jobconfirmation.JobConfirmationStatus;

import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;

public class JobConfirmationServiceFactory {


	public static CreateJobConfirmationRequest toCreateJobConfirmationRequest(
					CreateJobConfirmationDTO dto) {
		return CreateJobConfirmationRequest
						.newBuilder()
						.setChatId(dto.getChatId())
						.setSubstituteId(dto.getSubstituteId())
						.setEmployerId(dto.getEmployerId())
						.build();
	}


	public static JobConfirmationDTO toJobConfirmationDTO(
					CreateJobConfirmationResponse response) {
		return toJobConfirmationDTO(response.getJobConfirmation());
	}

	public static JobConfirmationDTO toJobConfirmationDTO(
					JobConfirmationAnswerResponse response) {
		return toJobConfirmationDTO(response.getJobConfirmation());
	}

	public static JobConfirmationAnswerRequest toJobConfirmationAnswerRequest(
					AnswerJobConfirmationDTO dto) {
		return JobConfirmationAnswerRequest
						.newBuilder()
						.setId(dto.getId())
						.setChatId(dto.getChatId())
						.setStatus(toJobConfirmationStatusGrpc(dto.getStatus()))
						.build();
	}

	public static GetJobConfirmationRequest toGetJobConfirmationRequest(int chatId) {
		return GetJobConfirmationRequest
						.newBuilder()
						.setId(chatId)
						.build();
	}

	public static JobConfirmationDTO toJobConfirmationDTO(GetJobConfirmationResponse response) {
		if (!response.hasJobConfirmation()) {
			return null;
		}

		return toJobConfirmationDTO(response.getJobConfirmation());
	}

	public static JobConfirmationDTO toJobConfirmationDTO(JobConfirmationObject jobConfirmation) {
		return new JobConfirmationDTO(
						jobConfirmation.getId(),
						jobConfirmation.getChatId(),
						jobConfirmation.getSubstituteId(),
						jobConfirmation.getEmployerId(),
						toJobConfirmationStatus(jobConfirmation.getStatus()),
						LocalDateTime.ofInstant(Instant.ofEpochSecond(jobConfirmation.getCreatedAt().getSeconds()), ZoneId.of("UTC"))
		);
	}

	public static JobConfirmationStatus toJobConfirmationStatus(JobConfirmationStatusGrpc value) {
		return switch (value) {
			case ACCEPTED -> JobConfirmationStatus.ACCEPTED;
			case DECLINED -> JobConfirmationStatus.DECLINED;
			case UNANSWERED -> JobConfirmationStatus.UNANSWERED;
			default -> throw new IllegalArgumentException("Unknown value: " + value);
		};
	}

	public static JobConfirmationStatusGrpc toJobConfirmationStatusGrpc(JobConfirmationStatus value) {
		return switch (value) {
			case ACCEPTED -> JobConfirmationStatusGrpc.ACCEPTED;
			case DECLINED -> JobConfirmationStatusGrpc.DECLINED;
			case UNANSWERED -> JobConfirmationStatusGrpc.UNANSWERED;
		};
	}
}
