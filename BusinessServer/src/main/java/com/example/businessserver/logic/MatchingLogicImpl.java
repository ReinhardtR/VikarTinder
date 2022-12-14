package com.example.businessserver.logic;

import com.example.businessserver.dtos.matching.*;
import com.example.businessserver.exceptions.DTOException;
import com.example.businessserver.exceptions.DTONullPointerException;
import com.example.businessserver.exceptions.DTOOutOfBoundsException;
import com.example.businessserver.logic.interfaces.MatchingLogic;
import com.example.businessserver.services.interfaces.MatchingService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class MatchingLogicImpl extends BasicLogic implements MatchingLogic {
	@Autowired
	private MatchingService service;

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
	public void gigsMatchRequest(MatchRequestDTO matchRequest) throws DTOException {
		checkMatch(matchRequest);
		service.gigsMatchRequest(matchRequest);
	}

	@Override
	public void substitutesMatchRequest(MatchRequestDTO matchRequest) throws DTOException {
		checkMatch(matchRequest);
		service.substitutesMatchRequest(matchRequest);
	}

	public void checkMatch(MatchRequestDTO matchRequest) throws DTOException {
		objectNullCheck(matchRequest, "Request");
		checkId(matchRequest.getCurrentUser());
		checkId(matchRequest.getMatchId());
	}
}
