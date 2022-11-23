package com.example.businessserver.dtos.matching;

public class MatchRequestDTO {
    private int currentUser;
    private int matchId;

    public MatchRequestDTO() {
    }

    public MatchRequestDTO(int currentUser, int matchId) {
        this.currentUser = currentUser;
        this.matchId = matchId;
    }

    public int getCurrentUser() {
        return currentUser;
    }

    public int getMatchId() {
        return matchId;
    }
}
