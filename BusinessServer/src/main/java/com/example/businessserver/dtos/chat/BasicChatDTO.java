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

	@Override public String toString()
	{
		StringBuilder userids = new StringBuilder();
		for (int i = 0; i < userIds.size(); i++)
		{
			userids = new StringBuilder(userids.append("user_ids: " + userIds.get(i) + "\n"));
		}
		return "id: " + id + "\n" + userids;
	}
}
