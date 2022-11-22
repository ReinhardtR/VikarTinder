package com.example.businessserver.logic.interfaces;

import com.example.businessserver.dtos.*;
import com.example.businessserver.dtos.matching.GigMatchingDTOs;
import com.example.businessserver.dtos.matching.GigSearchParametersDTO;
import com.example.businessserver.dtos.matching.SubstituteMatchingDTOs;
import com.example.businessserver.dtos.matching.SubstituteSearchParametersDTO;
import com.example.businessserver.exceptions.DTOException;

public interface MatchingLogic {
    SubstituteMatchingDTOs getSubstitutes(SubstituteSearchParametersDTO searchParameters) throws DTOException;
    GigMatchingDTOs getGigs(GigSearchParametersDTO searchParameters) throws DTOException;
    void sendMatchRequestSubstitute(MatchRequestSubstituteDTO request) throws DTOException;
    void sendMatchRequestEmployer(MatchRequestEmployerDTO request) throws DTOException;

}
