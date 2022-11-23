package com.example.businessserver.services.interfaces;

import com.example.businessserver.dtos.*;
import com.example.businessserver.dtos.matching.GigMatchingDTOs;
import com.example.businessserver.dtos.matching.GigSearchParametersDTO;
import com.example.businessserver.dtos.matching.SubstituteMatchingDTOs;
import com.example.businessserver.dtos.matching.SubstituteSearchParametersDTO;


public interface MatchingService {
    SubstituteMatchingDTOs getSubstitutes(SubstituteSearchParametersDTO searchParameters);
    GigMatchingDTOs getGigs(GigSearchParametersDTO searchParameters);

    void sendMatchRequestSubstitute(MatchRequestSubstituteDTO request);

    void sendMatchRequestEmployer(MatchRequestEmployerDTO request);
}
