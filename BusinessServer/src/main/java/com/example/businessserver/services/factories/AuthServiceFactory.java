package com.example.businessserver.services.factories;

import AuthService.*;
import com.example.businessserver.dtos.auth.*;
import com.example.businessserver.exceptions.BuildingException;

public class AuthServiceFactory {
	public static CreateLoginRequest createLoginRequest(String email) {
		return CreateLoginRequest.newBuilder()
						.setEmail(email)
						.build();
	}

	public static LoginUserResponseDTO userObjectDTO(LoginUserResponse loginResponse) throws BuildingException {
		return userObjectDTO(loginResponse.getUser());
	}

	public static LoginUserResponseDTO userObjectDTO(UserObject userObjectGRPC) throws BuildingException {
		UserData userData = userObjectGRPC.getUserData();
		if (userData.hasSub()) {
			SubstituteObject sub = userData.getSub();
			return new LoginSubstituteResponseDTO(
							userObjectGRPC.getId(),
							userData.getFirstName(),
							userData.getLastName(),
							userData.getPasswordHash(),
							userData.getEmail(),
							SharedFactory.toLocalDateTime(sub.getBirthDate()),
							sub.getBio(),
							sub.getAddress(),
							userData.getSalt()
			);
		}
		if (userData.hasEmp()) {
			EmployerObject emp = userData.getEmp();
			return new LoginEmployerResponseDTO(
							userObjectGRPC.getId(),
							userData.getFirstName(),
							userData.getLastName(),
							userData.getPasswordHash(),
							userData.getEmail(),
							emp.getTitle(),
							emp.getWorkplace(),
							userData.getSalt()
			);
		}
		throw new BuildingException("unrecognised user type: " + userData.getRoleCase().getClass());
	}

	public static CreateUserRequest createUserRequestEmployer(SignUpWrapperEmployerDTO employerRequestDTO) {
		return CreateUserRequest.newBuilder()
						.setUser(
										UserData.newBuilder()
														.setFirstName(employerRequestDTO.getFirstName())
														.setLastName(employerRequestDTO.getLastName())
														.setSalt(employerRequestDTO.getSalt())
														.setPasswordHash(employerRequestDTO.getPassword())
														.setEmail(employerRequestDTO.getEmail())
														.setEmp(
																		EmployerObject.newBuilder()
																						.setTitle(employerRequestDTO.getTitle())
																						.setWorkplace(employerRequestDTO.getWorkplace()).build()
														).build()
						).build();
	}

	public static CreateUserRequest createUserRequestSubstitute(SignUpWrapperSubstituteDTO substituteRequestDTO) {
		return CreateUserRequest.newBuilder()
						.setUser(
										UserData.newBuilder()
														.setFirstName(substituteRequestDTO.getFirstName())
														.setLastName(substituteRequestDTO.getLastName())
														.setSalt(substituteRequestDTO.getSalt())
														.setPasswordHash(substituteRequestDTO.getPassword())
														.setEmail(substituteRequestDTO.getEmail())
														.setSub(
																		SubstituteObject.newBuilder()
																						.setBirthDate(SharedFactory.toTimestamp(substituteRequestDTO.getBirthDate()))
																						.setBio(substituteRequestDTO.getBio())
																						.setAddress(substituteRequestDTO.getAddress()
																						).build()
														).build()
						).build();
	}

	public static GetUserRequest getUserRequest(GetUserInfoParamsDTO getEmployerInfoParamsDTO) {
		return GetUserRequest.newBuilder()
						.setUser(
										GetUserParams.newBuilder()
														.setId(getEmployerInfoParamsDTO.getId())
														.setRole(GetUserParams.Role.valueOf(getEmployerInfoParamsDTO.getRole().name()))
														.build()
						).build();
	}

	//TODO : De to nedre metoder kunne godt generaliseres lidt
	public static EmployerInfoDTO employerInfoDTO(GetUserResponse response) {
		UserInfo info = response.getUser();
		EmployerObject empInfo = info.getEmp();
		return new EmployerInfoDTO(
						info.getFirstName(),
						info.getLastName(),
						empInfo.getTitle(),
						empInfo.getWorkplace()
		);
	}

	public static SubstituteInfoDTO substituteInfoDTO(GetUserResponse response) {
		UserInfo info = response.getUser();
		SubstituteObject subInfo = info.getSub();
		return new SubstituteInfoDTO(
						info.getFirstName(),
						info.getLastName(),
						SharedFactory.toLocalDateTime(subInfo.getBirthDate()),
						subInfo.getBio(),
						subInfo.getAddress()
		);
	}
}
