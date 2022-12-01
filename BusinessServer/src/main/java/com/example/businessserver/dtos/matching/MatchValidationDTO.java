package com.example.businessserver.dtos.matching;

public class MatchValidationDTO {
    private boolean isMatched;
    private int employerId;
    private int substituteId;
    private int gigId;

    public MatchValidationDTO() {
    }

    public MatchValidationDTO(boolean isMatched, int employerId, int substituteId, int gigId) {
        this.isMatched = isMatched;
        this.employerId = employerId;
        this.substituteId = substituteId;
        this.gigId = gigId;
    }

    public boolean isMatched() {
        return isMatched;
    }

    public int getEmployerId() {
        return employerId;
    }

    public int getSubstituteId() {
        return substituteId;
    }

    public int getGigId() {
        return gigId;
    }
}
