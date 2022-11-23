package com.example.businessserver.restcontrollers;

import com.example.businessserver.dtos.matching.*;
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

    @GetMapping("/substitutes")
    public SubstituteMatchingDTOs getSubstitutes(@RequestBody SubstituteSearchParametersDTO searchParameters)
    {
        try {
            return logic.getSubstitutes(searchParameters);
        } catch (DTOException e) {
            throw new RuntimeException(e);
        }
    }

    @PostMapping("/substitutes")
    public MatchValidationDTO substitutesMatchRequest(@RequestBody MatchRequestDTO matchRequest)
    {
        try {
            return logic.substitutesMatchRequest(matchRequest);
        } catch (DTOException e) {
            throw new RuntimeException(e);
        }
    }

    @GetMapping("/gigs")
    public GigMatchingDTOs getGigs(@RequestBody GigSearchParametersDTO searchParameters)
    {
        try {
            return logic.getGigs(searchParameters);
        } catch (DTOException e) {
            throw new RuntimeException(e);
        }
    }

    @PostMapping("/gigs")
    public MatchValidationDTO gigsMatchRequest(@RequestBody MatchRequestDTO matchRequest)
    {
        try {
            return logic.gigsMatchRequest(matchRequest);
        } catch (DTOException e) {
            throw new RuntimeException(e);
        }
    }
}
