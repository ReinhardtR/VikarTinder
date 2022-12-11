package com.example.businessserver.services.implementations;

import AdministrationService.AdministrationServiceGrpc;
import AdministrationService.LoginUserResponse;
import AdministrationService.UserObject;
import ChatService.ChatServiceGrpc;
import com.example.businessserver.dtos.auth.UserObjectDTO;
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
import java.util.Collections;
import java.util.List;

// TODO : Refactor navngivning til gruppens standard
@Service
public class AuthServiceImpl implements AuthService {
    @GrpcClient("grpc-server")
    private AdministrationServiceGrpc.AdministrationServiceBlockingStub chatServiceBlockingStub;

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

        if (userName.equals("admin"))
        {
            List<GrantedAuthority> authorities = new ArrayList<>();
            authorities.add(new SimpleGrantedAuthority("ROLE_SUBSTITUTE"));
            authorities.add(new SimpleGrantedAuthority("AGE_5"));
            authorities.add(new SimpleGrantedAuthority("EMAIL_VIKAR@VIKAR.VIKAR"));
            return new User("admin", "password", authorities);
        }
        if (userName.equals("heysa"))
        {
            System.out.println("here");
            List<GrantedAuthority> authorities = new ArrayList<>();
            authorities.add(new SimpleGrantedAuthority("ROLE_SUBSTITUTE"));
            authorities.add(new SimpleGrantedAuthority("AGE_5"));
            authorities.add(new SimpleGrantedAuthority("EMAIL_VIKAR@VIKAR.VIKAR"));
            return new User("heysa", "carl", authorities);
        }

        return null;
    }

    //TODO: Burde der laves en extended class med claims i stedet for at sige Auth
    private UserDetails createUser(UserObjectDTO userObjectDTO) {
        List<GrantedAuthority> authorities = new ArrayList<>();
        authorities.add(new SimpleGrantedAuthority("ROLE_" + userObjectDTO.getRole()));
        authorities.add(new SimpleGrantedAuthority("NAMEF_" + userObjectDTO.getFirstName()));
        authorities.add(new SimpleGrantedAuthority("NAMEL_" + userObjectDTO.getLastName()));
        authorities.add(new SimpleGrantedAuthority("ID_" + userObjectDTO.getId()));
        return new User(userObjectDTO.getEmail(), userObjectDTO.getPasswordHashed(), authorities);
    }
}
