package com.example.businessserver.services;

import com.example.businessserver.dtos.EmployerDTO;
import com.example.businessserver.dtos.SubstituteDTO;
import com.example.businessserver.dtos.WorkPositionDTO;
import com.example.businessserver.services.interfaces.MatchingService;

import java.util.List;

public class MatchingServiceImpl implements MatchingService {
    @Override
    public List<SubstituteDTO> GetSubstitutesByEmployerId(EmployerDTO employerDTO) {
        return null;
    }

    @Override
    public List<WorkPositionDTO> GetWorkPositionsBySubstituteId(SubstituteDTO substituteDTO) {
        return null;
    }
}
