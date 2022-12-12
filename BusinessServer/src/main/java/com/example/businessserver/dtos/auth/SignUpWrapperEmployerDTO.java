package com.example.businessserver.dtos.auth;

public class SignUpWrapperEmployerDTO extends SignUpWrapperParentDTO{
    private SignUpEmployerRequestDTO requestDTO;

    public SignUpWrapperEmployerDTO(String salt, String hashedPassword, SignUpEmployerRequestDTO requestDTO) {
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

    public String getTitle()
    {
        return requestDTO.getTitle();
    }

    public String getWorkplace()
    {
        return requestDTO.getWorkplace();
    }
}
