package com.example.businessserver.dtos.matching;

public class GigSearchParametersDTO {
    private int currentSubstituteId;

    public GigSearchParametersDTO() {
    }

    public GigSearchParametersDTO(int currentSubstituteId) {
        this.currentSubstituteId = currentSubstituteId;
    }

    public int getCurrentSubstituteId() {
        return currentSubstituteId;
    }
}
