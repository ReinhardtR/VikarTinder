package com.example.businessserver.services.builders;

import MatchingProto.GigSearchParameters;
import MatchingProto.MatchRequest;
import MatchingProto.SubstituteSearchParameters;
import com.example.businessserver.dtos.matching.GigSearchParametersDTO;
import com.example.businessserver.dtos.matching.MatchRequestDTO;
import com.example.businessserver.dtos.matching.SubstituteSearchParametersDTO;

public class MatchGRPCBuilder {
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
