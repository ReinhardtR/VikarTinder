package com.example.businessserver.dtos.auth;

import java.time.LocalDateTime;

public class SignUpWrapperSubstituteDTO extends SignUpWrapperParentDTO {
    private SignUpSubstituteRequestDTO requestDTO;

    public SignUpWrapperSubstituteDTO(String salt, String hashedPassword, SignUpSubstituteRequestDTO requestDTO) {
        super(salt, hashedPassword);
        this.requestDTO = requestDTO;
    }

    @Override
    public String getFirstName() {
        return requestDTO.getFirstName();
    }

    @Override
    public String getLastName() {
        return requestDTO.getLastName();
    }

    @Override
    public String getEmail() {
        return requestDTO.getEmail();
    }

    public LocalDateTime getBirthDate()
    {
        return requestDTO.getBirthDate();
    }

    public String getBio()
    {
        return requestDTO.getBio();
    }

    public String getAddress()
    {
        return requestDTO.getAddress();
    }
}
