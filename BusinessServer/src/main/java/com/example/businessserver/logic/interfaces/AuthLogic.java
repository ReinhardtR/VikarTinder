package com.example.businessserver.logic.interfaces;

import com.example.businessserver.dtos.auth.JwtResponseDTO;
import com.example.businessserver.dtos.auth.LoginRequestDTO;
import com.example.businessserver.dtos.auth.RegisterEmployerRequestDTO;
import com.example.businessserver.dtos.auth.RegisterSubstituteRequestDTO;
import com.example.businessserver.exceptions.DTOException;

public interface AuthLogic {
	JwtResponseDTO login(LoginRequestDTO loginRequest) throws DTOException;

	void signUpEmployer(RegisterEmployerRequestDTO requestDTO) throws DTOException;

	void signUpSubstitute(RegisterSubstituteRequestDTO requestDTO) throws DTOException;
}
