package com.example.businessserver.dtos.auth;

public class SignUpRequestDTO {
    private String firstName;
    private String lastName;
    private String password;
    private String email;
    private enum role {
        SUBSTITUTE,
        EMPLOYER
    }
}
