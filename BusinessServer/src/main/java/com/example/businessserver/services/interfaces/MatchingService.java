package com.example.businessserver.services.interfaces;

import com.example.businessserver.dtos.*;


public interface MatchingService {
    SubstituteDatesDTO getSubstitutesByEmployerId(DatingSearchParametersEmployee parameters);
    WorkPositionDatesDTO getWorkPositionsBySubstituteId(DatingSearchParametersSubstitute parameters);

    void sendMatchRequestSubstitute(MatchRequestSubstituteDTO request);

    void sendMatchRequestEmployer(MatchRequestEmployerDTO request);
}
