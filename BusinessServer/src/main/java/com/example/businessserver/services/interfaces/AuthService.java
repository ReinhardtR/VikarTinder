package com.example.businessserver.services.interfaces;

import com.example.businessserver.dtos.auth.SignUpEmployerRequestDTO;
import com.example.businessserver.dtos.auth.SignUpSubstituteRequestDTO;
import com.example.businessserver.dtos.auth.SignUpWrapperEmployerDTO;
import com.example.businessserver.dtos.auth.SignUpWrapperSubstituteDTO;
import org.springframework.security.core.userdetails.UserDetailsService;

public interface AuthService extends UserDetailsService {
    void SignUpEmployer(SignUpWrapperEmployerDTO employerRequestDTO);
    void SignUpSubstitute(SignUpWrapperSubstituteDTO substituteRequestDTO);
}
