package com.example.businessserver.dtos.chat;

import com.fasterxml.jackson.annotation.JsonProperty;

import java.util.List;

public class CreateChatDTO {
	private final List<Integer> ids;

	public CreateChatDTO(@JsonProperty("ids") List<Integer> ids) {
		this.ids = ids;
	}

	public List<Integer> getIds() {
		return ids;
	}
}
