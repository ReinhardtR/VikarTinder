package com.example.businessserver.dtos.chat.overview.gigs;

import java.util.List;

public class EmployerGigsDTO {
	public List<GigDTO> gigs;

	public EmployerGigsDTO(List<GigDTO> gigs) {
		this.gigs = gigs;
	}

	public List<GigDTO> getGigs() {
		return gigs;
	}
}
