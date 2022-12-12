package com.example.businessserver.dtos.auth;

public class GetEmployerInfoParamsDTO {
    private int id;
    private LoginUserResponseDTO.Role role;

    public GetEmployerInfoParamsDTO(int id, LoginUserResponseDTO.Role role) {
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
