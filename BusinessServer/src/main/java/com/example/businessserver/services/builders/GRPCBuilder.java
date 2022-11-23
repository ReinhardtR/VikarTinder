package com.example.businessserver.services.builders;

import MatchingService.GigSearchParameters;
import MatchingService.MatchRequest;
import MatchingService.SubstituteSearchParameters;
import com.example.businessserver.dtos.matching.SubstituteSearchParametersDTO;
import com.example.businessserver.dtos.matching.GigSearchParametersDTO;
import com.example.businessserver.dtos.MatchRequestSubstituteDTO;
import com.example.businessserver.services.builders.interfaces.grpc.MatchingGRPCBuilder;

public class GRPCBuilder implements MatchingGRPCBuilder {
    @Override
    public SubstituteSearchParameters buildSubstituteSearchParameters(SubstituteSearchParametersDTO parameters)
    {
        return SubstituteSearchParameters.newBuilder()
                .setCurrentUserId(parameters.getCurrentEmployerId()).build();
    }
    @Override
    public GigSearchParameters buildGigsSearchParameters(GigSearchParametersDTO parameters)
    {
        return GigSearchParameters.newBuilder()
                .setCurrentUserId(parameters.getCurrentSubstituteId()).build();
    }

    @Override
    public MatchRequest buildMatchRequest(MatchRequestSubstituteDTO request) {
        return null;
    }
}
