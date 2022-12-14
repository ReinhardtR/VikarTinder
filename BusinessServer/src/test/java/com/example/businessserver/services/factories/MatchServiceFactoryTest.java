package com.example.businessserver.services.factories;

import MatchingProto.MatchRequest;
import MatchingProto.MatchValidationResponse;
import MatchingProto.SubstituteToBeMatched;
import MatchingProto.SubstitutesForMatchingResponse;
import com.example.businessserver.dtos.matching.*;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

class MatchServiceFactoryTest {

	@Test
	void test_substituteSearchParameters() {
		assertEquals(
						1,
						MatchServiceFactory.substituteSearchParameters(
														new SubstituteSearchParametersDTO(1))
										.getCurrentUserId()
		);
	}

	@Test
	void test_gigsSearchParameters() {
		assertEquals(
						1,
						MatchServiceFactory.gigsSearchParameters(
														new GigSearchParametersDTO(1))
										.getCurrentUserId()
		);
	}

	@Test
	void test_matchRequest() {
		MatchRequest matchRequest = MatchServiceFactory.matchRequest(
						new MatchRequestDTO(1, 5, true)
		);
		assertAll(
						() -> assertEquals(1, matchRequest.getCurrentUser()),
						() -> assertEquals(5, matchRequest.getToBeMatchedId()),
						() -> assertTrue(matchRequest.getWantToMatch())
		);
	}


	@Test
	void test_substituteMatchingDTOs_PossibleMatchesLength() {
		assertAll(
						() -> assertEquals(
										0,
										MatchServiceFactory.substituteMatchingDTOs(
																		createMatchingSubstitutes(0)
														)
														.getPossibleMatches().size()
										, "Empty list should be made"),
						() -> assertEquals(
										1,
										MatchServiceFactory.substituteMatchingDTOs(
																		createMatchingSubstitutes(1)
														)
														.getPossibleMatches().size()
										, "List should contain 1 match"),
						() -> assertEquals(
										5,
										MatchServiceFactory.substituteMatchingDTOs(
																		createMatchingSubstitutes(5)
														)
														.getPossibleMatches().size()
										, "List should contain 5 matches")
		);
	}

	@Test
	void test_substituteMatchingDTOs_CorrectPossibleMatchesBuildUp() {
		SubstitutesForMatchingResponse grpc = createMatchingSubstitutes(10);
		SubstituteMatchingDTOs test = MatchServiceFactory.substituteMatchingDTOs(grpc);
		for (int i = 0; i < test.getPossibleMatches().size(); i++) {
			assertEquals(i, test.getPossibleMatches().get(i).getId(), "Substitute id should have been " + i);
		}
	}


	@Test
	void test_matchValidationDTO_CorrectBuildUp() {
		MatchValidationResponse validation = MatchValidationResponse.newBuilder()
						.setIsMatched(true)
						.setEmployerId(25)
						.setSubstituteId(5)
						.setGigId(2)
						.build();
		MatchValidationDTO test = MatchServiceFactory.matchValidationDTO(validation);
		assertAll(
						() -> assertTrue(
										test.getIsMatched()
						),
						() -> assertEquals(
										25,
										test.getEmployerId()
						),
						() -> assertEquals(
										5,
										test.getSubstituteId()
						),
						() -> assertEquals(
										2,
										test.getGigId()
						)
		);
	}

	private SubstitutesForMatchingResponse createMatchingSubstitutes(int length) {
		return SubstitutesForMatchingResponse.newBuilder().
						addAllSubstitutes(
										validListOfSubstitutesToBeMatched(length)
						).build();
	}

	private List<SubstituteToBeMatched> validListOfSubstitutesToBeMatched(int length) {
		List<SubstituteToBeMatched> toReturn = new ArrayList<>();
		for (int i = 0; i < length; i++) {
			toReturn.add(makeSubstituteToBeMatched(i));
		}
		return toReturn;
	}

	private SubstituteToBeMatched makeSubstituteToBeMatched(int id) {
		return SubstituteToBeMatched.newBuilder()
						.setId(id).build();
	}
}