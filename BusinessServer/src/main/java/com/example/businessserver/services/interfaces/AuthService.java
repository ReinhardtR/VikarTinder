package com.example.businessserver.services.interfaces;

import com.example.businessserver.dtos.auth.LoginUserResponseDTO;
import com.example.businessserver.dtos.auth.SignUpEmployerRequestDTO;
import org.springframework.security.core.userdetails.UserDetailsService;

public interface AuthService extends UserDetailsService {
    void SignUpEmployer(SignUpEmployerRequestDTO employerRequestDTO);
}
