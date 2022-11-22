package com.example.businessserver.services.builders.interfaces.dto;

import UserService.SubstitutesForMatching;
import UserService.WorkpIds;
import com.example.businessserver.dtos.matching.SubstituteMatchingDTOs;
import com.example.businessserver.dtos.matching.GigMatchingDTOs;

public interface MatchingDTOBuilder {
    SubstituteMatchingDTOs substituteMatchingDTOs(SubstitutesForMatching possibleMatches);
    GigMatchingDTOs gigMatchingDTOs(WorkpIds possibleMatches);
}
