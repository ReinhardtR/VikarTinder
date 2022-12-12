package com.example.businessserver.logic.interfaces;

import com.example.businessserver.dtos.auth.*;
import com.example.businessserver.exceptions.DTOException;

public interface AuthLogic {
    JwtResponseDTO login(LoginRequestDTO loginRequest) throws DTOException;
    void signUpEmployer(SignUpEmployerRequestDTO requestDTO) throws DTOException;
    void signUpSubstitute(SignUpSubstituteRequestDTO requestDTO) throws DTOException;

    void getEmployerInfo(GetEmployerInfoParamsDTO getEmployerInfoParamsDTO) throws DTOException;
}
