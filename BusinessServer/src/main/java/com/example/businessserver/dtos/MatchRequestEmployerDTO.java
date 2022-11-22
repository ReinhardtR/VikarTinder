package com.example.businessserver.dtos;

public class MatchRequestEmployerDTO {
    private EmployerDatingDTO currentEmployer;
    private SubstituteDatingDTO substituteToMatch;

    public MatchRequestEmployerDTO() {
    }

    public MatchRequestEmployerDTO(EmployerDatingDTO currentEmployer, SubstituteDatingDTO substituteToMatch) {
        this.currentEmployer = currentEmployer;
        this.substituteToMatch = substituteToMatch;
    }

    public EmployerDatingDTO getCurrentEmployer() {
        return currentEmployer;
    }

    public SubstituteDatingDTO getSubstituteToMatch() {
        return substituteToMatch;
    }
}
