package com.example.businessserver.dtos.chat;

import java.util.List;

public class BasicChatDTO {
	private int id;
	private List<Integer> userIds;

	public BasicChatDTO(int id, List<Integer> userIds) {
		this.id = id;
		this.userIds = userIds;
	}

	public int getId() {
		return id;
	}

	public List<Integer> getUserIds() {
		return userIds;
	}
	
}
