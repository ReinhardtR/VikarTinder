package com.example.businessserver.services;

import MatchingProto.MatchValidation;
import MatchingProto.MatchingGigs;
import MatchingProto.MatchingServiceGrpc;
import MatchingProto.MatchingSubstitutes;
import com.example.businessserver.dtos.matching.*;
import com.example.businessserver.services.builders.MatchDTOBuilder;
import com.example.businessserver.services.builders.MatchGRPCBuilder;
import com.example.businessserver.services.interfaces.MatchingService;
import net.devh.boot.grpc.client.inject.GrpcClient;
import org.springframework.stereotype.Service;


@Service
public class MatchingServiceImpl implements MatchingService {
	@GrpcClient("grpc-server")
	private MatchingServiceGrpc.MatchingServiceBlockingStub userServiceBlockingStub;

	@Override
	public SubstituteMatchingDTOs getSubstitutes(SubstituteSearchParametersDTO searchParameters) {
		MatchingSubstitutes possibleMatches = userServiceBlockingStub.getSubstitutes(
						MatchGRPCBuilder.substituteSearchParameters(searchParameters)
		);

		return MatchDTOBuilder.substituteMatchingDTOs(possibleMatches);
	}

	@Override
	public GigMatchingDTOs getGigs(GigSearchParametersDTO searchParameters) {
		MatchingGigs possibleMatches = userServiceBlockingStub.getGigs(
						MatchGRPCBuilder.gigsSearchParameters(searchParameters)
		);

		return MatchDTOBuilder.gigMatchingDTOs(possibleMatches);
	}

	@Override
	public MatchValidationDTO gigsMatchRequest(MatchRequestDTO matchRequest) {
		MatchValidation validation = userServiceBlockingStub.sendMatchFromSubstitute(
						MatchGRPCBuilder.matchRequest(matchRequest)
		);

		return MatchDTOBuilder.matchValidationDTO(validation);
	}

	@Override
	public MatchValidationDTO substitutesMatchRequest(MatchRequestDTO matchRequest) {
		MatchValidation validation = userServiceBlockingStub.sendMatchFromEmployer(
						MatchGRPCBuilder.matchRequest(matchRequest)
		);

		return MatchDTOBuilder.matchValidationDTO(validation);
	}
}
