package com.example.businessserver.dtos.chat.history;

public class ChatSubstituteDTO {
	private int id;
	private String firstName;
	private String lastName;

	public ChatSubstituteDTO(int id, String firstName, String lastName) {
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
