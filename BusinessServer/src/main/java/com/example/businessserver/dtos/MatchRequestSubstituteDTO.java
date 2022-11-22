package com.example.businessserver.dtos;

public class MatchRequestSubstituteDTO {
    private SubstituteDatingDTO currentSubstitute;
    private WorkPositionDatingDTO positionToMatch;

    public MatchRequestSubstituteDTO() {
    }

    public MatchRequestSubstituteDTO(SubstituteDatingDTO currentSubstitute, WorkPositionDatingDTO wantingToMatch) {
        this.currentSubstitute = currentSubstitute;
        this.positionToMatch = wantingToMatch;
    }

    public SubstituteDatingDTO getCurrentSubstitute() {
        return currentSubstitute;
    }

    public WorkPositionDatingDTO getPositionToMatch() {
        return positionToMatch;
    }
}
