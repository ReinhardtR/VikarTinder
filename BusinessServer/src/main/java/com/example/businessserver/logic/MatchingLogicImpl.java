package com.example.businessserver.logic;

import com.example.businessserver.dtos.*;
import com.example.businessserver.dtos.matching.GigMatchingDTOs;
import com.example.businessserver.dtos.matching.GigSearchParametersDTO;
import com.example.businessserver.dtos.matching.SubstituteMatchingDTOs;
import com.example.businessserver.dtos.matching.SubstituteSearchParametersDTO;
import com.example.businessserver.exceptions.DTOException;
import com.example.businessserver.exceptions.DTONullPointerException;
import com.example.businessserver.exceptions.DTOOutOfBoundsException;
import com.example.businessserver.logic.interfaces.MatchingLogic;
import com.example.businessserver.services.interfaces.MatchingService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class MatchingLogicImpl implements MatchingLogic {
    @Autowired
    MatchingService service;

    @Override
    public SubstituteMatchingDTOs getSubstitutes(SubstituteSearchParametersDTO searchParameters) throws DTOException {
        objectNullCheck(searchParameters, "Substitute Search Parameters");
        checkId(searchParameters.getCurrentEmployerId());
        return service.getSubstitutes(searchParameters);
    }

    @Override
    public GigMatchingDTOs getGigs(GigSearchParametersDTO searchParameters) throws DTOException {
        objectNullCheck(searchParameters, "Gig Search Parameters");
        checkId(searchParameters.getCurrentSubstituteId());
        return service.getGigs(searchParameters);
    }

    @Override
    public void sendMatchRequestSubstitute(MatchRequestSubstituteDTO request) throws DTOException {
        if (request == null)
            throw new DTONullPointerException("Request cannot be null!");
        if (request.getCurrentSubstitute() == null)
            throw new DTONullPointerException("Substitute cannot be null!");
        if (request.getPositionToMatch() == null)
            throw new DTONullPointerException("Position cannot be null!");
        checkId(request.getCurrentSubstitute().getId());
        checkId(request.getPositionToMatch().getId());
        service.sendMatchRequestSubstitute(request);
    }

    @Override
    public void sendMatchRequestEmployer(MatchRequestEmployerDTO request) throws DTOException {
        if (request == null)
            throw new DTONullPointerException("Request cannot be null!");
        if (request.getCurrentEmployer() == null)
            throw new DTONullPointerException("Employer cannot be null!");
        if (request.getSubstituteToMatch() == null)
            throw new DTONullPointerException("Substitute cannot be null!");
        checkId(request.getCurrentEmployer().getId());
        checkId(request.getSubstituteToMatch().getId());
        service.sendMatchRequestEmployer(request);
    }

    private void checkId(int id) throws DTOOutOfBoundsException {
        if (id < 1)
            throw new DTOOutOfBoundsException("Id cannot be < 1!");
    }

    private void objectNullCheck(Object obj, String subjectName) throws DTONullPointerException {
        if (obj == null)
            throw new DTONullPointerException(subjectName +" cannot be Null!");
    }
}
