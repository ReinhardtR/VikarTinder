package com.example.businessserver.dtos;

public class DatingSearchParametersSubstitute {
    private int currentSubstituteId;

    public DatingSearchParametersSubstitute() {
    }

    public DatingSearchParametersSubstitute(int currentSubstituteId) {
        this.currentSubstituteId = currentSubstituteId;
    }

    public int getCurrentSubstituteId() {
        return currentSubstituteId;
    }
}
