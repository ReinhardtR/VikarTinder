package com.example.businessserver.dtos.auth;

public class DeleteRequestDTO {
    private int id;
    private LoginUserResponseDTO.Role role;

    public DeleteRequestDTO(int id, LoginUserResponseDTO.Role role) {
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
