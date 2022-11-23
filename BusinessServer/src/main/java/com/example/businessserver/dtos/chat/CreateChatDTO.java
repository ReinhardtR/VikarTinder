package com.example.businessserver.dtos.chat;

import java.util.List;

public class CreateChatDTO {
	private List<Integer> ids;

	public CreateChatDTO(List<Integer> ids) {
		this.ids = ids;
	}

	public List<Integer> getIds() {
		return ids;
	}
}
