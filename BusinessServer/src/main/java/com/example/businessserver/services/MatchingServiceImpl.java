package com.example.businessserver.services;

import MatchingProto.MatchValidation;
import MatchingProto.MatchingGigs;
import MatchingProto.MatchingServiceGrpc;
import MatchingProto.MatchingSubstitutes;
import com.example.businessserver.dtos.matching.*;
import com.example.businessserver.services.builders.DTOBuilder;
import com.example.businessserver.services.builders.GRPCBuilder;
import com.example.businessserver.services.interfaces.MatchingService;
import net.devh.boot.grpc.client.inject.GrpcClient;
import org.springframework.stereotype.Service;


@Service
public class MatchingServiceImpl implements MatchingService {
	private final GRPCBuilder grpcBuilder = new GRPCBuilder();
	private final DTOBuilder dtoBuilder = new DTOBuilder();
	@GrpcClient("grpc-server")
	private MatchingServiceGrpc.MatchingServiceBlockingStub userServiceBlockingStub;

	@Override
	public SubstituteMatchingDTOs getSubstitutes(SubstituteSearchParametersDTO searchParameters) {
		MatchingSubstitutes possibleMatches = userServiceBlockingStub.getSubstitutes(
						grpcBuilder.buildSubstituteSearchParameters(searchParameters)
		);

		return dtoBuilder.substituteMatchingDTOs(possibleMatches);
	}

	@Override
	public GigMatchingDTOs getGigs(GigSearchParametersDTO searchParameters) {
		MatchingGigs possibleMatches = userServiceBlockingStub.getGigs(
						grpcBuilder.buildGigsSearchParameters(searchParameters)
		);

		return dtoBuilder.gigMatchingDTOs(possibleMatches);
	}

	@Override
	public MatchValidationDTO gigsMatchRequest(MatchRequestDTO matchRequest) {
		MatchValidation validation = userServiceBlockingStub.sendMatchFromSubstitute(
						grpcBuilder.buildMatchRequest(matchRequest)
		);

		return dtoBuilder.matchValidationDTO(validation);
	}

	@Override
	public MatchValidationDTO substitutesMatchRequest(MatchRequestDTO matchRequest) {
		MatchValidation validation = userServiceBlockingStub.sendMatchFromEmployer(
						grpcBuilder.buildMatchRequest(matchRequest)
		);

		return dtoBuilder.matchValidationDTO(validation);
	}
}
