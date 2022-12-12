package com.example.businessserver.services.interfaces;

import com.example.businessserver.dtos.auth.RegisterEmployerRequestDTO;
import com.example.businessserver.dtos.auth.RegisterSubstituteRequestDTO;
import org.springframework.security.core.userdetails.UserDetailsService;

public interface AuthService extends UserDetailsService {
	void SignUpEmployer(RegisterEmployerRequestDTO employerRequestDTO);

	void SignUpSubstitute(RegisterSubstituteRequestDTO substituteRequestDTO);
}
