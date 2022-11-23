package com.example.businessserver.logic.interfaces;

import com.example.businessserver.dtos.matching.*;
import com.example.businessserver.exceptions.DTOException;

public interface MatchingLogic {
    SubstituteMatchingDTOs getSubstitutes(SubstituteSearchParametersDTO searchParameters) throws DTOException;
    GigMatchingDTOs getGigs(GigSearchParametersDTO searchParameters) throws DTOException;
    MatchValidationDTO gigsMatchRequest(MatchRequestDTO matchRequest) throws DTOException;
    MatchValidationDTO substitutesMatchRequest(MatchRequestDTO matchRequest) throws DTOException;

}
