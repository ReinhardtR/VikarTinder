package com.example.businessserver.services.builders.interfaces.dto;

import MatchingService.MatchingGigs;
import MatchingService.MatchingSubstitutes;
import com.example.businessserver.dtos.matching.SubstituteMatchingDTOs;
import com.example.businessserver.dtos.matching.GigMatchingDTOs;

public interface MatchingDTOBuilder {
    SubstituteMatchingDTOs substituteMatchingDTOs(MatchingSubstitutes possibleMatches);
    GigMatchingDTOs gigMatchingDTOs(MatchingGigs possibleMatches);
}
