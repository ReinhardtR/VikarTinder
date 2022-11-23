package com.example.businessserver.services.interfaces;

import com.example.businessserver.dtos.matching.*;


public interface MatchingService {
    SubstituteMatchingDTOs getSubstitutes(SubstituteSearchParametersDTO searchParameters);
    GigMatchingDTOs getGigs(GigSearchParametersDTO searchParameters);

    MatchValidationDTO gigsMatchRequest(MatchRequestDTO matchRequest);

    MatchValidationDTO substitutesMatchRequest(MatchRequestDTO matchRequest);
}
