package com.example.businessserver.services.factories;


import JobConfirmationService.*;
import com.example.businessserver.dtos.JobConfirmation.AnswerJobConfirmationDTO;
import com.example.businessserver.dtos.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.JobConfirmation.JobConfirmationDTO;

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
						.setIsAccepted(dto.getStatus())
						.build();
	}

	public static GetJobConfirmationRequest toGetJobConfirmationRequest(int chatId) {
		return GetJobConfirmationRequest
						.newBuilder()
						.setId(chatId)
						.build();
	}

	public static JobConfirmationDTO toJobConfirmationDTO(GetJobConfirmationResponse response) {
		return toJobConfirmationDTO(response.getJobConfirmation());
	}

	private static JobConfirmationDTO toJobConfirmationDTO(JobConfirmationObject jobConfirmation) {
		return new JobConfirmationDTO(
						jobConfirmation.getId(),
						jobConfirmation.getChatId(),
						jobConfirmation.getSubstituteId(),
						jobConfirmation.getEmployerId(),
						jobConfirmation.getStatus(),
						LocalDateTime.ofInstant(Instant.ofEpochSecond(jobConfirmation.getCreatedAt().getSeconds()), ZoneId.of("UTC"))
		);
	}
}
