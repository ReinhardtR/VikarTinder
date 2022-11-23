package com.example.businessserver.services.builders;

import UserService.SubstituteId;
import UserService.SubstitutesForMatching;
import UserService.WorkpId;
import UserService.WorkpIds;
import com.example.businessserver.dtos.matching.SubstituteMatchingDTOs;
import com.example.businessserver.dtos.matching.SubstituteMatchingDTO;
import com.example.businessserver.dtos.matching.GigMatchingDTOs;
import com.example.businessserver.dtos.matching.GigMatchingDTO;
import com.example.businessserver.services.builders.interfaces.dto.MatchingDTOBuilder;

import java.util.ArrayList;
import java.util.List;

public class DTOBuilder implements MatchingDTOBuilder {
    @Override
    public SubstituteMatchingDTOs substituteMatchingDTOs(SubstitutesForMatching possibleMatches)
    {
        List<SubstituteMatchingDTO> substituteDatingDTOS = new ArrayList<>();
        for (SubstituteId id:possibleMatches.getSubstitutesList()) {
            substituteDatingDTOS.add(new SubstituteMatchingDTO(id.getId()));
        }
        return new SubstituteMatchingDTOs(substituteDatingDTOS);
    }

    @Override
    public GigMatchingDTOs gigMatchingDTOs(WorkpIds possibleMatches) {
        List<GigMatchingDTO> workpDatingDTOS = new ArrayList<>();
        for (WorkpId id:possibleMatches.getWorkpIdsList()) {
            workpDatingDTOS.add(new GigMatchingDTO(id.getId()));
        }
        return new GigMatchingDTOs(workpDatingDTOS);
    }
}
