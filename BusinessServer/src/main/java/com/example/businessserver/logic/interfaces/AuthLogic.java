package com.example.businessserver.logic.interfaces;

import com.example.businessserver.dtos.auth.JwtResponseDTO;
import com.example.businessserver.dtos.auth.LoginRequestDTO;
import com.example.businessserver.dtos.auth.SignUpEmployerRequestDTO;
import com.example.businessserver.dtos.auth.SignUpSubstituteRequestDTO;
import com.example.businessserver.exceptions.DTOException;
import com.example.businessserver.exceptions.DTONullPointerException;

public interface AuthLogic {
    JwtResponseDTO login(LoginRequestDTO loginRequest) throws DTOException;
    void signUpEmployer(SignUpEmployerRequestDTO requestDTO) throws DTOException;
    void signUpSubstitute(SignUpSubstituteRequestDTO requestDTO) throws DTOException;
}
