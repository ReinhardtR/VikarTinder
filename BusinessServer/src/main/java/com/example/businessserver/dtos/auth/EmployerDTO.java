package com.example.businessserver.dtos.auth;

public class EmployerDTO extends UserObjectDTO {
    private String title;
    private String workplace;

    public EmployerDTO(int id, String firstName, String lastName, String passwordHashed, String email, String title, String workplace) {
        super(id, firstName, lastName, passwordHashed, email, "EMPLOYER");
        this.title = title;
        this.workplace = workplace;
    }

    public String getTitle() {
        return title;
    }

    public String getWorkplace() {
        return workplace;
    }
}
