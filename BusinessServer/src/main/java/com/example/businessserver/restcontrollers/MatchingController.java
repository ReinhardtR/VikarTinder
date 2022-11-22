package com.example.businessserver.restcontrollers;

import com.example.businessserver.dtos.*;
import com.example.businessserver.exceptions.DTOException;
import com.example.businessserver.logic.interfaces.MatchingLogic;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/matching")
@CrossOrigin
public class MatchingController {

    @Autowired
    private MatchingLogic logic;

    @GetMapping("/Substitutes")
    public SubstituteDatesDTO getSubstitutesFromEmployeeId(@RequestBody DatingSearchParametersEmployee parameters)
    {
        try {
            return logic.getSubstitutesByEmployerId(parameters);
        } catch (DTOException e) {
            throw new RuntimeException(e);
        }
    }

    @PostMapping("/Substitutes")
    public void MatchRequestFromEmployer(@RequestBody MatchRequestEmployerDTO request)
    {
        try {
            logic.sendMatchRequestEmployer(request);
        } catch (DTOException e) {
            throw new RuntimeException(e);
        }
    }

    @GetMapping("/WorkPositions")
    public WorkPositionDatesDTO getWorkPositionsFromSubstituteId(@RequestBody DatingSearchParametersSubstitute parameters)
    {
        try {
            return logic.getWorkPositionsBySubstituteId(parameters);
        } catch (DTOException e) {
            throw new RuntimeException(e);
        }
    }

    @PostMapping("/WorkPositions")
    public void MatchRequestFromSubstitutes(@RequestBody MatchRequestSubstituteDTO request)
    {
        try {
            logic.sendMatchRequestSubstitute(request);
        } catch (DTOException e) {
            throw new RuntimeException(e);
        }
    }
}
