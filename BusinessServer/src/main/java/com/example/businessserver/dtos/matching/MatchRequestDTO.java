package com.example.businessserver.dtos.matching;

public class MatchRequestDTO {
	private int currentUser;
	private int matchId;
	private boolean wantsToMatch;

	public MatchRequestDTO() {
	}

	public MatchRequestDTO(int currentUser, int matchId, boolean wantsToMatch) {
		this.currentUser = currentUser;
		this.matchId = matchId;
		this.wantsToMatch = wantsToMatch;
	}

	public int getCurrentUser() {
		return currentUser;
	}

	public int getMatchId() {
		return matchId;
	}

	public boolean getWantsToMatch() {
		return wantsToMatch;
	}
}
