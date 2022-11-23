package com.example.businessserver.services.builders.interfaces.grpc;

import MatchingService.GigSearchParameters;
import MatchingService.MatchRequest;
import MatchingService.SubstituteSearchParameters;
import com.example.businessserver.dtos.matching.SubstituteSearchParametersDTO;
import com.example.businessserver.dtos.matching.GigSearchParametersDTO;
import com.example.businessserver.dtos.MatchRequestSubstituteDTO;

public interface MatchingGRPCBuilder {
    SubstituteSearchParameters buildSubstituteSearchParameters(SubstituteSearchParametersDTO parameters);
    GigSearchParameters buildGigsSearchParameters(GigSearchParametersDTO parameters);
    MatchRequest buildMatchRequest(MatchRequestSubstituteDTO request);
}
