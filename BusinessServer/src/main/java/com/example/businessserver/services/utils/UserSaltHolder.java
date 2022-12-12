package com.example.businessserver.services.utils;

import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.User;

import java.util.Collection;

public class UserSaltHolder extends User {
    String salt;

    public UserSaltHolder(String username, String password, Collection<? extends GrantedAuthority> authorities, String salt) {
        super(username, password, authorities);
        this.salt = salt;
    }

    public String getSalt() {
        return salt;
    }
}
