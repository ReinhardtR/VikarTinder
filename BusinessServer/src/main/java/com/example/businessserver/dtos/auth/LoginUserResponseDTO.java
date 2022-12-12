package com.example.businessserver.dtos.auth;

public abstract class LoginUserResponseDTO {
    private int id;
    private String firstName;
    private String lastName;
    private String passwordHashed;
    private String email;
    private Role role;
    private String salt;


    public enum Role{
        SUBSTITUTE,
        EMPLOYER
    }

    public LoginUserResponseDTO(int id, String firstName, String lastName, String passwordHashed, String email, String role, String salt) {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.passwordHashed = passwordHashed;
        this.email = email;
        this.role = Role.valueOf(role);
        this.salt = salt;
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

    public String getSalt() {
        return salt;
    }
}
