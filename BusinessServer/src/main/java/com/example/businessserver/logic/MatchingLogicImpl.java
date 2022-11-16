package com.example.businessserver.logic;

import com.example.businessserver.dtos.EmployerDTO;
import com.example.businessserver.dtos.SubstituteDTO;
import com.example.businessserver.dtos.WorkPositionDTO;
import com.example.businessserver.logic.interfaces.MatchingLogic;
import com.example.businessserver.services.interfaces.MatchingService;
import org.springframework.beans.factory.annotation.Autowired;

import java.util.List;

public class MatchingLogicImpl implements MatchingLogic {
    @Autowired
    MatchingService service;

    public MatchingLogicImpl(MatchingService service) {
        this.service = service;
    }

    @Override
    public List<SubstituteDTO> getSubstitutesByEmployerId(EmployerDTO employerDTO) {
        if (employerDTO.getId() != null || Integer.parseInt(employerDTO.getId()) < 1)
            return null;
        return null;
    }

    @Override
    public List<WorkPositionDTO> getWorkPositionsBySubstituteId(SubstituteDTO substituteDTO) {
        return null;
    }
}
