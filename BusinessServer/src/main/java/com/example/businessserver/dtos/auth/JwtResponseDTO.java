package com.example.businessserver.dtos.auth;
public class JwtResponseDTO {
    private String jwtToken;

    public JwtResponseDTO(String jwtToken) {
        this.jwtToken = jwtToken;
    }

    public String getJwtToken() {
        return jwtToken;
    }
}
