package com.example.businessserver.services.implementations;

import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetails;

import java.util.Collection;

public class AuthUser extends User {
    private int id;
    private String role;
    private String email;

    public AuthUser(String username, String password, int id, String role, String email, Collection<? extends GrantedAuthority> authorities) {
        super(username, password, authorities);
        this.id = id;
        this.role = role;
        this.email = email;
    }

    public AuthUser(String username, String password, boolean enabled, boolean accountNonExpired, boolean credentialsNonExpired, boolean accountNonLocked, Collection<? extends GrantedAuthority> authorities) {
        super(username, password, enabled, accountNonExpired, credentialsNonExpired, accountNonLocked, authorities);
    }

    @Override
    public String getPassword() {
        return super.getPassword();
    }

    @Override
    public String getUsername() {
        return super.getUsername();
    }

    public int getId() {
        return id;
    }

    public String getRole() {
        return role;
    }

    public String getEmail() {
        return email;
    }
}
