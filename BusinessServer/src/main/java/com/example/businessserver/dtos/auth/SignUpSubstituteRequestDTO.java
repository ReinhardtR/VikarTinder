package com.example.businessserver.dtos.auth;

public class SignUpSubstituteRequestDTO {
    private String firstName;
    private String lastName;
    private String password;
    private String email;
    private int age;
    private String bio;
    private String address;

    public SignUpSubstituteRequestDTO(String firstName, String lastName, String password, String email, int age, String bio, String address) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.password = password;
        this.email = email;
        this.age = age;
        this.bio = bio;
        this.address = address;
    }

    public String getFirstName() {
        return firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public String getPassword() {
        return password;
    }

    public String getEmail() {
        return email;
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
