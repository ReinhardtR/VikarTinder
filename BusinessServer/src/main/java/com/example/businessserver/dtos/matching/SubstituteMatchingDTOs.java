package com.example.businessserver.dtos.matching;

import java.util.List;

public class SubstituteMatchingDTOs {
    private List<SubstituteMatchingDTO> possibleMatches;

    public SubstituteMatchingDTOs() {
    }

    public SubstituteMatchingDTOs(List<SubstituteMatchingDTO> dates) {
        this.possibleMatches = dates;
    }

    public List<SubstituteMatchingDTO> getPossibleMatches() {
        return possibleMatches;
    }
}
