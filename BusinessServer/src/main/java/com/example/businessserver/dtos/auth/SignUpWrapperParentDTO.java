package com.example.businessserver.dtos.auth;

public abstract class SignUpWrapperParentDTO {
    private String salt;
    private String hashedPassword;

    public SignUpWrapperParentDTO(String salt, String hashedPassword) {
        this.salt = salt;
        this.hashedPassword = hashedPassword;
    }

    public String getSalt() {
        return salt;
    }

    public String getPassword() {
        return hashedPassword;
    }

    public abstract String getFirstName();
    public abstract String getLastName();
    public abstract String getEmail();
}
