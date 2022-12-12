package com.example.businessserver.services.factories;

import AuthService.*;
import com.example.businessserver.dtos.auth.LoginEmployerResponseDTO;
import com.example.businessserver.dtos.auth.LoginSubstituteResponseDTO;
import com.example.businessserver.dtos.auth.LoginUserResponseDTO;
import com.example.businessserver.dtos.auth.SignUpEmployerRequestDTO;
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
							sub.getAddress());
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
							emp.getWorkplace()
			);
		}
		throw new BuildingException("unrecognised user type: " + userData.getRoleCase().getClass());
	}

	public static CreateUserRequest createUserRequestEmployer(SignUpEmployerRequestDTO employerRequestDTO) {
		return CreateUserRequest.newBuilder()
						.setUser(
										UserData.newBuilder()
														.setFirstName(employerRequestDTO.getFirstName())
														.setLastName(employerRequestDTO.getLastName())
														.setPasswordHash(employerRequestDTO.getPassword())
														.setEmail(employerRequestDTO.getEmail())
														.setEmp(
																		EmployerObject.newBuilder()
																						.setTitle(employerRequestDTO.getTitle())
																						.setWorkplace(employerRequestDTO.getWorkplace()).build()
														).build()
						).build();
	}
}
