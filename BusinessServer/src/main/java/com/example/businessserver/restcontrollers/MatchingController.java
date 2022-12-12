package com.example.businessserver.restcontrollers;

import com.example.businessserver.dtos.matching.*;
import com.example.businessserver.exceptions.DTOException;
import com.example.businessserver.logic.interfaces.MatchingLogic;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/matching")
public class MatchingController {

	@Autowired
	private MatchingLogic logic;

	@GetMapping("/substitutes/{currentUserId}")
	public SubstituteMatchingDTOs getSubstitutes(@PathVariable int currentUserId) {
		try {
			return logic.getSubstitutes(new SubstituteSearchParametersDTO(currentUserId));
		} catch (DTOException e) {
			throw new RuntimeException(e);
		}
	}

	@PostMapping("/substitutes")
	public void substitutesMatchRequest(@RequestBody MatchRequestDTO matchRequest) {
		try {
			logic.substitutesMatchRequest(matchRequest);
		} catch (DTOException e) {
			throw new RuntimeException(e);
		}
	}

	@GetMapping("/gigs/{currentUserId}")
	public GigMatchingDTOs getGigs(@PathVariable int currentUserId) {
		try {
			return logic.getGigs(new GigSearchParametersDTO(currentUserId));
		} catch (DTOException e) {
			throw new RuntimeException(e);
		}
	}

	@PostMapping("/gigs")
	public void gigsMatchRequest(@RequestBody MatchRequestDTO matchRequest) {
		try {
			logic.gigsMatchRequest(matchRequest);
		} catch (DTOException e) {
			throw new RuntimeException(e);
		}
	}
}
