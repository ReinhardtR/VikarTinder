package com.example.businessserver.services.factories;

import AdministrationService.*;
import com.example.businessserver.dtos.auth.EmployerDTO;
import com.example.businessserver.dtos.auth.LoginRequestDTO;
import com.example.businessserver.dtos.auth.SubstituteDTO;
import com.example.businessserver.dtos.auth.UserObjectDTO;
import com.example.businessserver.exceptions.BuildingException;

public class AuthServiceFactory {
    public static CreateLoginRequest createLoginRequest(String email)
    {
        return CreateLoginRequest.newBuilder()
                .setEmail(email)
                .build();
    }

    public static UserObjectDTO userObjectDTO(LoginUserResponse loginResponse) throws BuildingException {
        return userObjectDTO(loginResponse.getUser());
    }

    public static UserObjectDTO userObjectDTO(UserObject userObjectGRPC) throws BuildingException {
        UserData userData = userObjectGRPC.getUserData();
        if (userData.hasSub())
        {
            SubstituteObject sub = userData.getSub();
            return new SubstituteDTO(
                    userObjectGRPC.getId(),
                    userData.getFirstName(),
                    userData.getLastName(),
                    userData.getPasswordHash(),
                    userData.getEmail(),
                    sub.getAge(),
                    sub.getBio(),
                    sub.getAddress());
        }
        if (userData.hasEmp()) {
            EmployerObject emp = userData.getEmp();
            return new EmployerDTO(
                    userObjectGRPC.getId(),
                    userData.getFirstName(),
                    userData.getLastName(),
                    userData.getPasswordHash(),
                    userData.getEmail(),
                    emp.getTitle(),
                    emp.getWorkplace()
            );
        }
        throw new BuildingException("unrecognised user type: " + userData.getRoleCase().getClass());
    }
}
