package com.example.businessserver.logic.interfaces;

import com.example.businessserver.dtos.matching.*;
import com.example.businessserver.exceptions.DTOException;

public interface MatchingLogic {
    SubstituteMatchingDTOs getSubstitutes(SubstituteSearchParametersDTO searchParameters) throws DTOException;
    GigMatchingDTOs getGigs(GigSearchParametersDTO searchParameters) throws DTOException;
    void gigsMatchRequest(MatchRequestDTO matchRequest) throws DTOException;
    void substitutesMatchRequest(MatchRequestDTO matchRequest) throws DTOException;

}
