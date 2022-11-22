package com.example.businessserver.dtos;

import com.example.businessserver.dtos.matching.GigMatchingDTO;
import com.example.businessserver.dtos.matching.SubstituteMatchingDTO;

public class MatchRequestSubstituteDTO {
    private SubstituteMatchingDTO currentSubstitute;
    private GigMatchingDTO positionToMatch;

    public MatchRequestSubstituteDTO() {
    }

    public MatchRequestSubstituteDTO(SubstituteMatchingDTO currentSubstitute, GigMatchingDTO wantingToMatch) {
        this.currentSubstitute = currentSubstitute;
        this.positionToMatch = wantingToMatch;
    }

    public SubstituteMatchingDTO getCurrentSubstitute() {
        return currentSubstitute;
    }

    public GigMatchingDTO getPositionToMatch() {
        return positionToMatch;
    }
}
