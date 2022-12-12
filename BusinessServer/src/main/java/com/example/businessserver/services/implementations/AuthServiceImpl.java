package com.example.businessserver.services.implementations;

import AuthService.AuthServiceGrpc;
import AuthService.SubstituteObject;
import AuthService.UserData;
import AuthService.UserObject;
import com.example.businessserver.dtos.auth.LoginUserResponseDTO;
import com.example.businessserver.exceptions.BuildingException;
import com.example.businessserver.services.factories.AuthServiceFactory;
import com.example.businessserver.services.interfaces.AuthService;
import net.devh.boot.grpc.client.inject.GrpcClient;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.User;
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
        /*LoginUserResponse returnedUser = chatServiceBlockingStub.login(
                AuthServiceFactory.createLoginRequest(userName)
        );
        UserObjectDTO userObjectDTO;
        try {
            userObjectDTO = AuthServiceFactory.userObjectDTO(returnedUser);
        } catch (BuildingException e) {throw new RuntimeException(e);}

        return createUser(userObjectDTO);*/

		if (userName.equals("heysa@hej.hej")) {
			int id = 1;
			String firstName = "firstNameTest";
			String lastName = "lastNameTest";
			String password = "testPassword";
			int age = 55;
			String bio = "This is a test";
			String address = "testAddress";
			SubstituteObject s1 = SubstituteObject.newBuilder()
							.setAge(age)
							.setBio(bio)
							.setAddress(address)
							.build();
			UserData s2 = UserData.newBuilder()
							.setFirstName(firstName)
							.setLastName(lastName)
							.setPasswordHash("Carlos")
							.setEmail(userName)
							.setSub(s1)
							.build();
			UserObject s3 = UserObject.newBuilder()
							.setId(id)
							.setUserData(s2)
							.build();
			LoginUserResponseDTO dto;
			try {
				dto = AuthServiceFactory.userObjectDTO(s3);
			} catch (BuildingException e) {
				throw new RuntimeException(e);
			}
			return createUser(dto);
		}

		return null;
	}

	//TODO: Burde der laves en extended class med claims i stedet for at sige Auth
	private UserDetails createUser(LoginUserResponseDTO userObjectDTO) {
		List<GrantedAuthority> authorities = new ArrayList<>();
		authorities.add(new SimpleGrantedAuthority("ROLE_" + userObjectDTO.getRole()));
		authorities.add(new SimpleGrantedAuthority("NAMEF_" + userObjectDTO.getFirstName()));
		authorities.add(new SimpleGrantedAuthority("NAMEL_" + userObjectDTO.getLastName()));
		authorities.add(new SimpleGrantedAuthority("ID_" + userObjectDTO.getId()));
		return new User(userObjectDTO.getEmail(), userObjectDTO.getPasswordHashed(), authorities);
	}

	@Override
	public LoginUserResponseDTO SignUp(LoginUserResponseDTO dto) {
		return null;
	}
}
