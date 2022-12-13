package com.example.businessserver.dtos.auth;

public class UpdateEmployerInfoDTO {
    private int id;
    private EmployerInfoDTO updatedInfo;

    public UpdateEmployerInfoDTO(int id, EmployerInfoDTO updatedInfo) {
        this.id = id;
        this.updatedInfo = updatedInfo;
    }

    public int getId() {
        return id;
    }

    public EmployerInfoDTO getUpdatedInfo() {
        return updatedInfo;
    }
}
