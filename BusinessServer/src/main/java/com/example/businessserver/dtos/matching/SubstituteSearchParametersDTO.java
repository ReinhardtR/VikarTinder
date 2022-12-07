package com.example.businessserver.dtos.matching;

public class SubstituteSearchParametersDTO {
    private int currentEmployerId;

    public SubstituteSearchParametersDTO() {
    }

    public SubstituteSearchParametersDTO(int currentUserId) {
        this.currentEmployerId = currentUserId;
    }

    public int getCurrentEmployerId() {
        return currentEmployerId;
    }
}
