package com.example.businessserver.services.builders;

import MatchingProto.GigSearchParameters;
import MatchingProto.MatchRequest;
import MatchingProto.SubstituteSearchParameters;
import com.example.businessserver.dtos.matching.GigSearchParametersDTO;
import com.example.businessserver.dtos.matching.MatchRequestDTO;
import com.example.businessserver.dtos.matching.SubstituteSearchParametersDTO;
import com.example.businessserver.services.builders.interfaces.grpc.MatchingGRPCBuilder;

public class GRPCBuilder implements MatchingGRPCBuilder {
	@Override
	public SubstituteSearchParameters buildSubstituteSearchParameters(SubstituteSearchParametersDTO parameters) {
		return SubstituteSearchParameters.newBuilder()
						.setCurrentUserId(parameters.getCurrentEmployerId()).build();
	}

	@Override
	public GigSearchParameters buildGigsSearchParameters(GigSearchParametersDTO parameters) {
		return GigSearchParameters.newBuilder()
						.setCurrentUserId(parameters.getCurrentSubstituteId()).build();
	}

	@Override
	public MatchRequest buildMatchRequest(MatchRequestDTO request) {
		return MatchRequest.newBuilder()
						.setCurrentUser(request.getCurrentUser())
						.setToBeMatchedId(request.getMatchId())
						.setWantToMatch(request.isWantToMatch()).build();
	}
}
