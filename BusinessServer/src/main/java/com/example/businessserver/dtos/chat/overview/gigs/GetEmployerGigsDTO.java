package com.example.businessserver.dtos.chat.overview.gigs;

public class GetEmployerGigsDTO {
	private int employerId;

	public GetEmployerGigsDTO(int employerId) {
		this.employerId = employerId;
	}

	public int getEmployerId() {
		return employerId;
	}
}
