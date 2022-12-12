package com.example.businessserver.dtos.auth;

public class SubstituteDTO extends UserObjectDTO{
    private int age;
    private String bio;
    private String address;

    public SubstituteDTO(int id, String firstName, String lastName, String passwordHashed, String email, int age, String bio, String address) {
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
