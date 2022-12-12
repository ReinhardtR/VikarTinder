package com.example.businessserver.dtos.auth;

import java.time.LocalDateTime;

public class LoginSubstituteResponseDTO extends LoginUserResponseDTO {
	private LocalDateTime birthDate;
	private String bio;
	private String address;

	public LoginSubstituteResponseDTO(int id, String firstName, String lastName, String passwordHashed, String email, LocalDateTime birthDate, String bio, String address, String salt) {
		super(id, firstName, lastName, passwordHashed, email, "SUBSTITUTE", salt);
		this.birthDate = birthDate;
		this.bio = bio;
		this.address = address;
	}

	public LocalDateTime getBirthDate() {
		return birthDate;
	}

	public String getBio() {
		return bio;
	}

	public String getAddress() {
		return address;
	}
}
