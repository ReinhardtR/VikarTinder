package com.example.businessserver.services.implementations;

import MatchingProto.GigsForMatchingResponse;
import MatchingProto.MatchValidationResponse;
import MatchingProto.MatchingServiceGrpc;
import MatchingProto.SubstitutesForMatchingResponse;
import com.example.businessserver.dtos.matching.*;
import com.example.businessserver.services.factories.MatchServiceFactory;
import com.example.businessserver.services.interfaces.MatchingService;
import net.devh.boot.grpc.client.inject.GrpcClient;
import org.springframework.stereotype.Service;


@Service
public class MatchingServiceImpl implements MatchingService {
	@GrpcClient("grpc-server")
	private MatchingServiceGrpc.MatchingServiceBlockingStub userServiceBlockingStub;

	@Override
	public SubstituteMatchingDTOs getSubstitutes(SubstituteSearchParametersDTO searchParameters) {
		SubstitutesForMatchingResponse possibleMatches = userServiceBlockingStub.getSubstitutes(
						MatchServiceFactory.substituteSearchParameters(searchParameters)
		);

		return MatchServiceFactory.substituteMatchingDTOs(possibleMatches);
	}

	@Override
	public GigMatchingDTOs getGigs(GigSearchParametersDTO searchParameters) {
		GigsForMatchingResponse possibleMatches = userServiceBlockingStub.getGigs(
						MatchServiceFactory.gigsSearchParameters(searchParameters)
		);

		return MatchServiceFactory.gigMatchingDTOs(possibleMatches);
	}

	@Override
	public MatchValidationDTO gigsMatchRequest(MatchRequestDTO matchRequest) {
		MatchValidationResponse validation = userServiceBlockingStub.sendMatchFromSubstitute(
						MatchServiceFactory.matchRequest(matchRequest)
		);

		return MatchServiceFactory.matchValidationDTO(validation);
	}

	@Override
	public MatchValidationDTO substitutesMatchRequest(MatchRequestDTO matchRequest) {
		MatchValidationResponse validation = userServiceBlockingStub.sendMatchFromEmployer(
						MatchServiceFactory.matchRequest(matchRequest)
		);

		return MatchServiceFactory.matchValidationDTO(validation);
	}
}
