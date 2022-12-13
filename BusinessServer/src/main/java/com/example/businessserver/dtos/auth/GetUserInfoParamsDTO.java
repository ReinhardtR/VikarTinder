package com.example.businessserver.dtos.auth;

public class GetUserInfoParamsDTO {
	private int id;
	private LoginUserResponseDTO.Role role;

	public GetUserInfoParamsDTO(int id, LoginUserResponseDTO.Role role) {
		this.id = id;
		this.role = role;
	}

	public int getId() {
		return id;
	}

	public LoginUserResponseDTO.Role getRole() {
		return role;
	}
}
