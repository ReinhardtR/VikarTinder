package com.example.businessserver.services.implementations;

import com.example.businessserver.services.interfaces.AuthService;
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
    @Override
    public UserDetails loadUserByUsername(String userName) throws UsernameNotFoundException {

        List<GrantedAuthority> authorities = new ArrayList<>();
        authorities.add(new SimpleGrantedAuthority("ROLE_SUBSTITUTE"));
        authorities.add(new SimpleGrantedAuthority("AGE_5"));
        authorities.add(new SimpleGrantedAuthority("EMAIL_VIKAR@VIKAR.VIKAR"));
        return new User("admin", "password", authorities);
    }
}
