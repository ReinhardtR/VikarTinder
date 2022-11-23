package com.example.businessserver.dtos;

import com.example.businessserver.dtos.matching.SubstituteMatchingDTO;

public class MatchRequestEmployerDTO {
    private EmployerDatingDTO currentEmployer;
    private SubstituteMatchingDTO substituteToMatch;

    public MatchRequestEmployerDTO() {
    }

    public MatchRequestEmployerDTO(EmployerDatingDTO currentEmployer, SubstituteMatchingDTO substituteToMatch) {
        this.currentEmployer = currentEmployer;
        this.substituteToMatch = substituteToMatch;
    }

    public EmployerDatingDTO getCurrentEmployer() {
        return currentEmployer;
    }

    public SubstituteMatchingDTO getSubstituteToMatch() {
        return substituteToMatch;
    }
}
