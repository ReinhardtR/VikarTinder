package com.example.businessserver.services.implementations;

import com.example.businessserver.services.interfaces.AuthService;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
// TODO : Refactor navngivning til gruppens standard
@Service
public class AuthServiceImpl implements AuthService {
    @Override
    public UserDetails loadUserByUsername(String userName) throws UsernameNotFoundException {
        new User("admin", "password", new ArrayList<>());
        return new AuthUser("admin", "password", 5, "SUBSTITUTE", "vikar@viakr.vikar", new ArrayList<>());
    }
}
