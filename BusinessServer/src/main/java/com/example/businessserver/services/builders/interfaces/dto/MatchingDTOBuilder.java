package com.example.businessserver.services.builders.interfaces.dto;

import UserService.SubstitutesForMatching;
import UserService.WorkpIds;
import com.example.businessserver.dtos.SubstituteDatesDTO;
import com.example.businessserver.dtos.WorkPositionDatesDTO;

public interface MatchingDTOBuilder {
    SubstituteDatesDTO buildSubstituteDates(SubstitutesForMatching possibleMatches);
    WorkPositionDatesDTO workPositionDates(WorkpIds possibleMatches);
}
