package com.example.businessserver.services.interfaces;

import com.example.businessserver.dtos.auth.LoginUserResponseDTO;
import org.springframework.security.core.userdetails.UserDetailsService;

public interface AuthService extends UserDetailsService {
    LoginUserResponseDTO SignUp(LoginUserResponseDTO dto);
}
