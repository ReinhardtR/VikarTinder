package com.example.businessserver.dtos.auth;

public class SignUpEmployerRequestDTO {
	private String firstName;
	private String lastName;
	private String password;
	private String email;
	private String title;
	private String workplace;

	public SignUpEmployerRequestDTO(String firstName, String lastName, String password, String email, String title, String workplace) {
		this.firstName = firstName;
		this.lastName = lastName;
		this.password = password;
		this.email = email;
		this.title = title;
		this.workplace = workplace;
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

	public String getTitle() {
		return title;
	}

	public String getWorkplace() {
		return workplace;
	}
}
