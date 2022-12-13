package com.example.businessserver.dtos.auth;

public class UpdateSubstituteInfoDTO {
    private int id;
    private SubstituteInfoDTO updatedInfo;

    public UpdateSubstituteInfoDTO(int id, SubstituteInfoDTO updatedInfo) {
        this.id = id;
        this.updatedInfo = updatedInfo;
    }

    public int getId() {
        return id;
    }

    public SubstituteInfoDTO getUpdatedInfo() {
        return updatedInfo;
    }
}
