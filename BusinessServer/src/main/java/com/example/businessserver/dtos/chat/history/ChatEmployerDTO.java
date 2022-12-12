package com.example.businessserver.dtos.chat.history;

public class ChatEmployerDTO {
	private int id;
	private String firstName;
	private String lastName;

	public ChatEmployerDTO(int id, String firstName, String lastName) {
		this.id = id;
		this.firstName = firstName;
		this.lastName = lastName;
	}

	public int getId() {
		return id;
	}

	public String getFirstName() {
		return firstName;
	}

	public String getLastName() {
		return lastName;
	}
}
