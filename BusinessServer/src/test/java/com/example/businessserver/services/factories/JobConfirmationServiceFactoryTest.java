package com.example.businessserver.services.factories;

import JobConfirmationService.*;
import com.example.businessserver.dtos.jobconfirmation.AnswerJobConfirmationDTO;
import com.example.businessserver.dtos.jobconfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.jobconfirmation.JobConfirmationDTO;
import com.example.businessserver.dtos.jobconfirmation.JobConfirmationStatus;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.MethodSource;

import java.time.LocalDateTime;
import java.util.stream.Stream;

import static org.junit.jupiter.api.Assertions.assertEquals;

class JobConfirmationServiceFactoryTest {

	private static Stream<Object> toCreateJobConfirmationRequest() {
		return Stream.of(
						new Object[]{1, 2, 3},
						new Object[]{-1, -2, 3},
						new Object[]{0, 0, 0}
		);
	}

	private static Stream<Object> toJobConfirmationDTO() {
		return Stream.of(
						new Object[]{1, 2, 3, 4, JobConfirmationStatusGrpc.ACCEPTED, LocalDateTime.now()},
						new Object[]{0, Integer.MAX_VALUE, Integer.MIN_VALUE, 5, JobConfirmationStatusGrpc.DECLINED, LocalDateTime.now()},
						new Object[]{-1, -2, 8, -5, JobConfirmationStatusGrpc.UNANSWERED, LocalDateTime.now()}
		);
	}

	private static Stream<Object> ToJobConfirmationDTOFromResponse() {
		return Stream.of(
						new Object[]{1, 2, 3, JobConfirmationStatusGrpc.ACCEPTED},
						new Object[]{0, Integer.MAX_VALUE, Integer.MIN_VALUE, JobConfirmationStatusGrpc.DECLINED},
						new Object[]{-1, -2, 8, JobConfirmationStatusGrpc.UNANSWERED}
		);
	}

	private static Stream<Object> toJobConfirmationAnswerRequest() {
		return Stream.of(
						new Object[]{1, 2, JobConfirmationStatus.ACCEPTED},
						new Object[]{0, Integer.MAX_VALUE, JobConfirmationStatus.DECLINED},
						new Object[]{-1, -2, JobConfirmationStatus.UNANSWERED}
		);
	}

	private static Stream<Object> toGetJobConfirmationRequest() {
		return Stream.of(
						new Object[]{1},
						new Object[]{Integer.MAX_VALUE},
						new Object[]{-1}
		);
	}

	private static Stream<Object> ToJobConfirmationDTOFromJobConfirmationResponse() {
		return Stream.of(
						new Object[]{1, 2, 3, JobConfirmationStatusGrpc.ACCEPTED},
						new Object[]{0, Integer.MAX_VALUE, Integer.MIN_VALUE, JobConfirmationStatusGrpc.DECLINED},
						new Object[]{-1, -2, 8, JobConfirmationStatusGrpc.UNANSWERED}
		);
	}

	@ParameterizedTest
	@MethodSource()
	void toCreateJobConfirmationRequest(int chatId, int substituteId, int employerId) {
		CreateJobConfirmationDTO dto = new CreateJobConfirmationDTO(chatId, substituteId, employerId);
		CreateJobConfirmationRequest request = JobConfirmationServiceFactory.toCreateJobConfirmationRequest(dto);
		assertEquals(chatId, request.getChatId());
		assertEquals(substituteId, request.getSubstituteId());
		assertEquals(employerId, request.getEmployerId());
	}

	@ParameterizedTest
	@MethodSource()
	void toJobConfirmationDTO(int cdId, int chatId, int employerId, int substituteId, JobConfirmationStatusGrpc status, LocalDateTime localDateTime) {
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

		JobConfirmationDTO dto =
						JobConfirmationServiceFactory.toJobConfirmationDTO(response);

		assertEquals(cdId, dto.getId());
		assertEquals(chatId, dto.getChatId());
		assertEquals(employerId, dto.getEmployerId());
		assertEquals(substituteId, dto.getSubstituteId());
		assertEquals(status, JobConfirmationServiceFactory.toJobConfirmationStatusGrpc(dto.getStatus()));
		assertEquals(localDateTime, dto.getOfferedAt());
	}

	@ParameterizedTest
	@MethodSource()
	void ToJobConfirmationDTOFromResponse(int chatId, int employerId, int substituteId, JobConfirmationStatusGrpc status) {
		JobConfirmationObject object = JobConfirmationObject.newBuilder()
						.setChatId(chatId)
						.setEmployerId(employerId)
						.setSubstituteId(substituteId)
						.setStatus(status)
						.build();

		GetJobConfirmationResponse response = GetJobConfirmationResponse.newBuilder()
						.setJobConfirmation(object)
						.build();

		JobConfirmationDTO dto =
						JobConfirmationServiceFactory.toJobConfirmationDTO(response);

		assertEquals(chatId, dto.getChatId());
		assertEquals(employerId, dto.getEmployerId());
		assertEquals(substituteId, dto.getSubstituteId());
		assertEquals(status, JobConfirmationServiceFactory.toJobConfirmationStatusGrpc(dto.getStatus()));
	}

	@ParameterizedTest
	@MethodSource()
	void toJobConfirmationAnswerRequest(int jcId, int chatId, JobConfirmationStatus status) {
		AnswerJobConfirmationDTO dto = new AnswerJobConfirmationDTO(jcId, chatId, status);

		JobConfirmationAnswerRequest request = JobConfirmationServiceFactory.toJobConfirmationAnswerRequest(dto);

		assertEquals(jcId, request.getId());
		assertEquals(chatId, request.getChatId());
		assertEquals(status.name(), JobConfirmationServiceFactory.toJobConfirmationStatus(request.getStatus()));
	}

	@ParameterizedTest
	@MethodSource()
	void toGetJobConfirmationRequest(int chatId) {
		GetJobConfirmationRequest request = JobConfirmationServiceFactory.toGetJobConfirmationRequest(chatId);
		assertEquals(chatId, request.getId());
	}

	@ParameterizedTest
	@MethodSource()
	void ToJobConfirmationDTOFromJobConfirmationResponse(int chatId, int empId, int subId, JobConfirmationStatusGrpc status) {
		JobConfirmationObject object = JobConfirmationObject.newBuilder()
						.setChatId(chatId)
						.setEmployerId(empId)
						.setSubstituteId(subId)
						.setStatus(status)
						.build();

		GetJobConfirmationResponse response = GetJobConfirmationResponse.newBuilder()
						.setJobConfirmation(object)
						.build();

		JobConfirmationDTO dto = JobConfirmationServiceFactory.toJobConfirmationDTO(response);

		assertEquals(chatId, dto.getChatId());
		assertEquals(empId, dto.getEmployerId());
		assertEquals(subId, dto.getSubstituteId());
		assertEquals(status, JobConfirmationServiceFactory.toJobConfirmationStatusGrpc(dto.getStatus()));
	}
}