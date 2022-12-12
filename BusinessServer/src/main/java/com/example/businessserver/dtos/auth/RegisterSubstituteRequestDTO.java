package com.example.businessserver.dtos.auth;

import java.time.LocalDateTime;

public class RegisterSubstituteRequestDTO {
	private String firstName;
	private String lastName;
	private String password;
	private String email;
	private LocalDateTime birthDate;
	private String bio;
	private String address;

	public RegisterSubstituteRequestDTO(String firstName, String lastName, String password, String email, LocalDateTime birthDate, String bio, String address) {
		this.firstName = firstName;
		this.lastName = lastName;
		this.password = password;
		this.email = email;
		this.birthDate = birthDate;
		this.bio = bio;
		this.address = address;
	}

	public String getFirstName() {
		return firstName;
	}

	public String getLastName() {
		return lastName;
	}

	public String getPassword() {
		return password;
	}

	public String getEmail() {
		return email;
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
