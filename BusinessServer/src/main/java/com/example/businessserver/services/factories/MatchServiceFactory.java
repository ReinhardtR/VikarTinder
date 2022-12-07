package com.example.businessserver.services.factories;

import MatchingProto.*;
import com.example.businessserver.dtos.matching.*;

import java.util.ArrayList;
import java.util.List;

public class MatchServiceFactory {
	public static SubstituteMatchingDTOs substituteMatchingDTOs(MatchingSubstitutes possibleMatches) {
		List<SubstituteMatchingDTO> substituteDatingDTOs = new ArrayList<>();
		for (SubstituteToBeMatched id : possibleMatches.getSubstitutesList()) {
			substituteDatingDTOs.add(new SubstituteMatchingDTO(id.getId()));
		}
		return new SubstituteMatchingDTOs(substituteDatingDTOs);
	}

	public static GigMatchingDTOs gigMatchingDTOs(MatchingGigs possibleMatches) {
		List<GigMatchingDTO> gigMatchingDTOs = new ArrayList<>();
		for (GigToBeMatched id : possibleMatches.getGigsList()) {
			gigMatchingDTOs.add(new GigMatchingDTO(id.getId()));
		}
		return new GigMatchingDTOs(gigMatchingDTOs);
	}

	public static MatchValidationDTO matchValidationDTO(MatchValidation validation) {
		return new MatchValidationDTO(
						validation.getIsMatched(),
						validation.getEmployerId(),
						validation.getSubstituteId(),
						validation.getGigId()
		);
	}

	public static SubstituteSearchParameters substituteSearchParameters(SubstituteSearchParametersDTO parameters) {
		return SubstituteSearchParameters.newBuilder()
						.setCurrentUserId(parameters.getCurrentEmployerId()).build();
	}

	public static GigSearchParameters gigsSearchParameters(GigSearchParametersDTO parameters) {
		return GigSearchParameters.newBuilder()
						.setCurrentUserId(parameters.getCurrentSubstituteId()).build();
	}

	public static MatchRequest matchRequest(MatchRequestDTO request) {
		return MatchRequest.newBuilder()
						.setCurrentUser(request.getCurrentUser())
						.setToBeMatchedId(request.getMatchId())
						.setWantToMatch(request.getWantsToMatch()).build();
	}
}
