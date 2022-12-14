package com.example.businessserver.services.implementations;

import AuthService.AuthServiceGrpc;
import AuthService.GetUserResponse;
import AuthService.LoginUserResponse;
import com.example.businessserver.dtos.auth.*;
import com.example.businessserver.exceptions.BuildingException;
import com.example.businessserver.services.factories.AuthServiceFactory;
import com.example.businessserver.services.interfaces.AuthService;
import com.example.businessserver.services.utils.UserSaltHolder;
import net.devh.boot.grpc.client.inject.GrpcClient;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

// TODO : Refactor navngivning til gruppens standard
@Service
public class AuthServiceImpl implements AuthService {
	@GrpcClient("grpc-server")
	private AuthServiceGrpc.AuthServiceBlockingStub authServiceBlockingStub;

	@Override
	public UserDetails loadUserByUsername(String userName) throws UsernameNotFoundException {
		LoginUserResponse returnedUser = authServiceBlockingStub.login(
						AuthServiceFactory.createLoginRequest(userName)
		);

		LoginUserResponseDTO userObjectDTO;

		try {
			userObjectDTO = AuthServiceFactory.userObjectDTO(returnedUser);
		} catch (BuildingException e) {
			throw new RuntimeException(e);
		}

		return createUser(userObjectDTO);
	}

	//TODO: Burde der laves en extended class med claims i stedet for at sige Auth
	private UserDetails createUser(LoginUserResponseDTO userObjectDTO) {
		List<GrantedAuthority> authorities = new ArrayList<>();
		authorities.add(new SimpleGrantedAuthority("ROLE_" + userObjectDTO.getRole()));
		authorities.add(new SimpleGrantedAuthority("NAMEF_" + userObjectDTO.getFirstName()));
		authorities.add(new SimpleGrantedAuthority("NAMEL_" + userObjectDTO.getLastName()));
		authorities.add(new SimpleGrantedAuthority("ID_" + userObjectDTO.getId()));
		return new UserSaltHolder(userObjectDTO.getEmail(), userObjectDTO.getPasswordHashed(), authorities, userObjectDTO.getSalt());
	}

	@Override
	public void SignUpEmployer(SignUpWrapperEmployerDTO employerRequestDTO) {
		authServiceBlockingStub.createUser(
						AuthServiceFactory.createUserRequestEmployer(employerRequestDTO)
		);
	}

	@Override
	public void SignUpSubstitute(SignUpWrapperSubstituteDTO substituteRequestDTO) {
		authServiceBlockingStub.createUser(
						AuthServiceFactory.createUserRequestSubstitute(substituteRequestDTO)
		);
	}

	@Override
	public EmployerInfoDTO getEmployerInfo(GetUserInfoParamsDTO getEmployerInfoParamsDTO) {
		GetUserResponse response = authServiceBlockingStub.getUser(
						AuthServiceFactory.getUserRequest(getEmployerInfoParamsDTO)
		);
		return AuthServiceFactory.employerInfoDTO(response);
	}

	@Override
	public SubstituteInfoDTO getSubstituteInfo(GetUserInfoParamsDTO getUserInfoParamsDTO) {
		GetUserResponse response = authServiceBlockingStub.getUser(
				AuthServiceFactory.getUserRequest(getUserInfoParamsDTO)
		);
		return AuthServiceFactory.substituteInfoDTO(response);
	}

	@Override
	public void updateEmployerInfo(UpdateEmployerInfoDTO updateRequest) {
		authServiceBlockingStub.updateUser(
				AuthServiceFactory.updateUserRequestEmp(updateRequest)
		);
	}

	@Override
	public void updateSubstituteInfo(UpdateSubstituteInfoDTO updateRequest) {
		authServiceBlockingStub.updateUser(
				AuthServiceFactory.updateUserRequestSub(updateRequest)
		);
	}

	@Override
	public void deleteUser(DeleteRequestDTO deleteRequest) {
		authServiceBlockingStub.deleteUser(
				AuthServiceFactory.deleteUserRequest(deleteRequest)
		);
	}
}
