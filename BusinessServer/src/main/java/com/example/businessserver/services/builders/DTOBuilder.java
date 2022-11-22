package com.example.businessserver.services.builders;

import UserService.SubstituteId;
import UserService.SubstitutesForMatching;
import UserService.WorkpId;
import UserService.WorkpIds;
import com.example.businessserver.dtos.SubstituteDatesDTO;
import com.example.businessserver.dtos.SubstituteDatingDTO;
import com.example.businessserver.dtos.WorkPositionDatesDTO;
import com.example.businessserver.dtos.WorkPositionDatingDTO;
import com.example.businessserver.services.builders.interfaces.dto.MatchingDTOBuilder;

import java.util.ArrayList;
import java.util.List;

public class DTOBuilder implements MatchingDTOBuilder {
    @Override
    public SubstituteDatesDTO buildSubstituteDates(SubstitutesForMatching possibleMatches)
    {
        List<SubstituteDatingDTO> substituteDatingDTOS = new ArrayList<>();
        for (SubstituteId id:possibleMatches.getSubstitutesList()) {
            substituteDatingDTOS.add(new SubstituteDatingDTO(id.getId()));
        }
        return new SubstituteDatesDTO(substituteDatingDTOS);
    }

    @Override
    public WorkPositionDatesDTO workPositionDates(WorkpIds possibleMatches) {
        List<WorkPositionDatingDTO> workpDatingDTOS = new ArrayList<>();
        for (WorkpId id:possibleMatches.getWorkpIdsList()) {
            workpDatingDTOS.add(new WorkPositionDatingDTO(id.getId()));
        }
        return new WorkPositionDatesDTO(workpDatingDTOS);
    }
}
