package com.example.businessserver.logic.interfaces;

import com.example.businessserver.dtos.EmployerDTO;
import com.example.businessserver.dtos.SubstituteDTO;
import com.example.businessserver.dtos.WorkPositionDTO;

import java.util.List;

public interface MatchingLogic {
    List<SubstituteDTO> getSubstitutesByEmployerId(EmployerDTO employerDTO);
    List<WorkPositionDTO> getWorkPositionsBySubstituteId(SubstituteDTO substituteDTO);
}
