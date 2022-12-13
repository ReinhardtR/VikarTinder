package com.example.businessserver.logic.interfaces;

import com.example.businessserver.dtos.auth.*;
import com.example.businessserver.exceptions.DTOException;

public interface AuthLogic {
    JwtResponseDTO login(LoginRequestDTO loginRequest) throws DTOException;
    void signUpEmployer(SignUpEmployerRequestDTO requestDTO) throws DTOException;
    void signUpSubstitute(SignUpSubstituteRequestDTO requestDTO) throws DTOException;

    EmployerInfoDTO getEmployerInfo(GetUserInfoParamsDTO getEmployerInfoParamsDTO) throws DTOException;

    SubstituteInfoDTO getSubstituteInfo(GetUserInfoParamsDTO getUserInfoParamsDTO) throws DTOException;
}
