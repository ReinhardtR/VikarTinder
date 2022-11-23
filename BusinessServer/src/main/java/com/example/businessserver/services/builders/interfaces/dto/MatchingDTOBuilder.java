package com.example.businessserver.services.builders.interfaces.dto;

import MatchingProto.MatchValidation;
import MatchingProto.MatchingGigs;
import MatchingProto.MatchingSubstitutes;
import com.example.businessserver.dtos.matching.GigMatchingDTOs;
import com.example.businessserver.dtos.matching.MatchValidationDTO;
import com.example.businessserver.dtos.matching.SubstituteMatchingDTOs;
;

public interface MatchingDTOBuilder {
    SubstituteMatchingDTOs substituteMatchingDTOs(MatchingSubstitutes possibleMatches);
    GigMatchingDTOs gigMatchingDTOs(MatchingGigs possibleMatches);

    MatchValidationDTO matchValidationDTO(MatchValidation validation);
}
