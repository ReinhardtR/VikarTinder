package com.example.businessserver.services.implementations;

import com.example.businessserver.services.interfaces.AuthService;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
// TODO : Refactor navngivning til gruppens standard
@Service
public class AuthServiceImpl implements AuthService {
    @Override
    public UserDetails loadUserByUsername(String userName) throws UsernameNotFoundException {

        //Logic to get the user form the Database

        return new User("admin","password",new ArrayList<>());
    }
}
