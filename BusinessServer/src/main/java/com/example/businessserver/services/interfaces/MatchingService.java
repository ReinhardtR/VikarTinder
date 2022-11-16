package com.example.businessserver.services.interfaces;

import com.example.businessserver.dtos.EmployerDTO;
import com.example.businessserver.dtos.SubstituteDTO;
import com.example.businessserver.dtos.WorkPositionDTO;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public interface MatchingService {
    List<SubstituteDTO> GetSubstitutesByEmployerId(EmployerDTO employerDTO);
    List<WorkPositionDTO> GetWorkPositionsBySubstituteId(SubstituteDTO substituteDTO);
}
