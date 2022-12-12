package com.example.businessserver.dtos.auth;

public abstract class UserObjectDTO {
    private int id;
    private String firstName;
    private String lastName;
    private String passwordHashed;
    private String email;
    private Role role;
    public enum Role{
        SUBSTITUTE,
        EMPLOYER
    }

    public UserObjectDTO(int id, String firstName, String lastName, String passwordHashed, String email, String role) {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.passwordHashed = passwordHashed;
        this.email = email;
        this.role = Role.valueOf(role);
    }

    public int getId() {
        return id;
    }

    public String getFirstName() {
        return firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public String getPasswordHashed() {
        return passwordHashed;
    }

    public String getEmail() {
        return email;
    }

    public Role getRole() {
        return role;
    }
}
