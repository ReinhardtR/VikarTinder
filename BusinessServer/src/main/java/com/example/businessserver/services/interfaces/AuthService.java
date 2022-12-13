package com.example.businessserver.services.interfaces;

import com.example.businessserver.dtos.auth.*;
import org.springframework.security.core.userdetails.UserDetailsService;

public interface AuthService extends UserDetailsService {
    void SignUpEmployer(SignUpWrapperEmployerDTO employerRequestDTO);
    void SignUpSubstitute(SignUpWrapperSubstituteDTO substituteRequestDTO);

    EmployerInfoDTO getEmployerInfo(GetUserInfoParamsDTO getEmployerInfoParamsDTO);

    void getSubstituteInfo(GetUserInfoParamsDTO getUserInfoParamsDTO);
}
