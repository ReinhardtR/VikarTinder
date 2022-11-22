package com.example.businessserver.logic.interfaces;

import com.example.businessserver.dtos.*;
import com.example.businessserver.exceptions.DTOException;

public interface MatchingLogic {
    SubstituteDatesDTO getSubstitutesByEmployerId(DatingSearchParametersEmployee parameters) throws DTOException;
    WorkPositionDatesDTO getWorkPositionsBySubstituteId(DatingSearchParametersSubstitute parameters) throws DTOException;
    void sendMatchRequestSubstitute(MatchRequestSubstituteDTO request) throws DTOException;
    void sendMatchRequestEmployer(MatchRequestEmployerDTO request) throws DTOException;

}
