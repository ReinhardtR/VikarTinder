package com.example.businessserver.services.builders.interfaces.grpc;

import MatchingProto.MatchRequest;
import com.example.businessserver.dtos.matching.GigSearchParametersDTO;
import com.example.businessserver.dtos.matching.MatchRequestDTO;
import com.example.businessserver.dtos.matching.SubstituteSearchParametersDTO;
import com.example.businessserver.services.builders.GRPCBuilder;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class MatchingGRPCBuilderTest {
    MatchingGRPCBuilder matchingBuilder = new GRPCBuilder();

    @Test
    void testBuildSubstituteSearchParameters()
    {
        assertEquals(
                1,
                matchingBuilder.buildSubstituteSearchParameters(
                                new SubstituteSearchParametersDTO(1))
                        .getCurrentUserId()
        );
    }

    @Test
    void testBuildGigsSearchParameters()
    {
        assertEquals(
                1,
                matchingBuilder.buildGigsSearchParameters(
                                new GigSearchParametersDTO(1))
                        .getCurrentUserId()
        );
    }

    @Test
    void testBuildMatchRequest()
    {
        MatchRequest matchRequest = matchingBuilder.buildMatchRequest(
          new MatchRequestDTO(1,5, true)
        );
        assertAll(
                ()-> assertEquals(1, matchRequest.getCurrentUser()),
                ()-> assertEquals(5, matchRequest.getToBeMatchedId()),
                ()-> assertTrue(matchRequest.getWantToMatch())
        );
    }
}