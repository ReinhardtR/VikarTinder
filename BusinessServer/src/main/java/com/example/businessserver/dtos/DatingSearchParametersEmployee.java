package com.example.businessserver.dtos;

public class DatingSearchParametersEmployee {
    private int currentEmployerId;

    public DatingSearchParametersEmployee() {
    }

    public DatingSearchParametersEmployee(int currentUserId) {
        this.currentEmployerId = currentUserId;
    }

    public int getCurrentEmployerId() {
        return currentEmployerId;
    }
}
