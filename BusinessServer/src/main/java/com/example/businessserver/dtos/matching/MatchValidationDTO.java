package com.example.businessserver.dtos.matching;

public class MatchValidationDTO {
    private boolean isMatched;
    private int matchId;

    public MatchValidationDTO() {
    }

    public MatchValidationDTO(boolean isMatched, int matchId) {
        this.isMatched = isMatched;
        this.matchId = matchId;
    }

    public boolean isMatched() {
        return isMatched;
    }

    public int getMatchId() {
        return matchId;
    }
}
