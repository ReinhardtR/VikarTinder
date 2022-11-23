package com.example.businessserver.dtos.matching;

import java.util.List;

public class GigMatchingDTOs {
    private List<GigMatchingDTO> possibleMatches;

    public GigMatchingDTOs() {
    }

    public GigMatchingDTOs(List<GigMatchingDTO> dates) {
        this.possibleMatches = dates;
    }

    public List<GigMatchingDTO> getPossibleMatches() {
        return possibleMatches;
    }
}
