package com.example.businessserver.services.builders;

import MatchingProto.*;
import com.example.businessserver.dtos.matching.*;
import com.example.businessserver.services.builders.interfaces.dto.MatchingDTOBuilder;

import java.util.ArrayList;
import java.util.List;

public class DTOBuilder implements MatchingDTOBuilder {
    @Override
    public SubstituteMatchingDTOs substituteMatchingDTOs(MatchingSubstitutes possibleMatches)
    {
        List<SubstituteMatchingDTO> substituteDatingDTOs = new ArrayList<>();
        for (SubstituteToBeMatched id:possibleMatches.getSubstitutesList()) {
            substituteDatingDTOs.add(new SubstituteMatchingDTO(id.getId()));
        }
        return new SubstituteMatchingDTOs(substituteDatingDTOs);
    }

    @Override
    public GigMatchingDTOs gigMatchingDTOs(MatchingGigs possibleMatches) {
        List<GigMatchingDTO> gigMatchingDTOs = new ArrayList<>();
        for (GigToBeMatched id:possibleMatches.getGigsList()) {
            gigMatchingDTOs.add(new GigMatchingDTO(id.getId()));
        }
        return new GigMatchingDTOs(gigMatchingDTOs);
    }

    @Override
    public MatchValidationDTO matchValidationDTO(MatchValidation validation) {
        return new MatchValidationDTO(validation.getIsMatched(), validation.getMatchId());
    }
}
