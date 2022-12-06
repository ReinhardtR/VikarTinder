package com.example.businessserver.dtos.matching;

public class MatchRequestDTO {
	private int currentUser;
	private int matchId;
	private boolean wantToMatch;

	public MatchRequestDTO() {
	}

	public MatchRequestDTO(int currentUser, int matchId, boolean wantToMatch) {
		this.currentUser = currentUser;
		this.matchId = matchId;
		this.wantToMatch = wantToMatch;
	}

	public int getCurrentUser() {
		return currentUser;
	}

	public int getMatchId() {
		return matchId;
	}

	// TODO: rename to getWantToMatch, makes more sense
	public boolean isWantToMatch() {
		return wantToMatch;
	}
}
