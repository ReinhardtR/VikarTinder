package com.example.businessserver.services.builders.interfaces.grpc;


import MatchingProto.GigSearchParameters;
import MatchingProto.MatchRequest;
import MatchingProto.SubstituteSearchParameters;
import com.example.businessserver.dtos.matching.MatchRequestDTO;
import com.example.businessserver.dtos.matching.SubstituteSearchParametersDTO;
import com.example.businessserver.dtos.matching.GigSearchParametersDTO;

public interface MatchingGRPCBuilder {
    SubstituteSearchParameters buildSubstituteSearchParameters(SubstituteSearchParametersDTO parameters);
    GigSearchParameters buildGigsSearchParameters(GigSearchParametersDTO parameters);
    MatchRequest buildMatchRequest(MatchRequestDTO request);
}
