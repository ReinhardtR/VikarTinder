package com.example.businessserver.dtos.auth;

public class EmployerInfoDTO {
    String firstName;
    String lastName;
    String title;
    String workplace;

    public EmployerInfoDTO(String firstName, String lastName, String title, String workplace) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.title = title;
        this.workplace = workplace;
    }

    public String getFirstName() {
        return firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public String getTitle() {
        return title;
    }

    public String getWorkplace() {
        return workplace;
    }
}
