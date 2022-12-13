package com.example.businessserver.dtos.auth;

import java.time.LocalDateTime;

public class SubstituteInfoDTO {
    private String firstName;
    private String lastName;
    private LocalDateTime birthDate;
    private String bio;
    private String address;

    public SubstituteInfoDTO(String firstName, String lastName, LocalDateTime birthDate, String bio, String address) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.birthDate = birthDate;
        this.bio = bio;
        this.address = address;
    }

    public String getFirstName() {
        return firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public LocalDateTime getBirthDate() {
        return birthDate;
    }

    public String getBio() {
        return bio;
    }

    public String getAddress() {
        return address;
    }
}
