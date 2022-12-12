package com.example.businessserver.dtos.auth;

public class LoginSubstituteResponseDTO extends LoginUserResponseDTO {
    private int age;
    private String bio;
    private String address;

    public LoginSubstituteResponseDTO(int id, String firstName, String lastName, String passwordHashed, String email, int age, String bio, String address) {
        super(id, firstName, lastName, passwordHashed, email, "SUBSTITUTE");
        this.age = age;
        this.bio = bio;
        this.address = address;
    }

    public int getAge() {
        return age;
    }

    public String getBio() {
        return bio;
    }

    public String getAddress() {
        return address;
    }
}
