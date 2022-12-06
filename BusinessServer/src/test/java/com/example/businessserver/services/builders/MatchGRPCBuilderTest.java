package com.example.businessserver.services.builders;

import MatchingProto.MatchRequest;
import com.example.businessserver.dtos.matching.GigSearchParametersDTO;
import com.example.businessserver.dtos.matching.MatchRequestDTO;
import com.example.businessserver.dtos.matching.SubstituteSearchParametersDTO;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class MatchGRPCBuilderTest {

    @Test
    void test_substituteSearchParameters()
    {
        assertEquals(
                1,
                MatchGRPCBuilder.substituteSearchParameters(
                                new SubstituteSearchParametersDTO(1))
                        .getCurrentUserId()
        );
    }

    @Test
    void test_gigsSearchParameters()
    {
        assertEquals(
                1,
                MatchGRPCBuilder.gigsSearchParameters(
                                new GigSearchParametersDTO(1))
                        .getCurrentUserId()
        );
    }

    @Test
    void test_matchRequest()
    {
        MatchRequest matchRequest = MatchGRPCBuilder.matchRequest(
                new MatchRequestDTO(1,5, true)
        );
        assertAll(
                ()-> assertEquals(1, matchRequest.getCurrentUser()),
                ()-> assertEquals(5, matchRequest.getToBeMatchedId()),
                ()-> assertTrue(matchRequest.getWantToMatch())
        );
    }
}