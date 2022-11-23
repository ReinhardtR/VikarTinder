package com.example.businessserver.services.builders.interfaces.grpc;

import com.example.businessserver.dtos.matching.GigSearchParametersDTO;
import com.example.businessserver.dtos.matching.SubstituteSearchParametersDTO;
import com.example.businessserver.services.builders.GRPCBuilder;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class MatchingGRPCBuilderTest {
    MatchingGRPCBuilder matchingBuilder = new GRPCBuilder();

    @Test
    void testCreateEmployerId()
    {
        assertEquals(
                1,
                matchingBuilder.buildEmployerId(
                                new SubstituteSearchParametersDTO(1))
                        .getId()
        );
    }

    @Test
    void testCreateSubstituteId()
    {
        assertEquals(
                1,
                matchingBuilder.buildSubstituteId(
                                new GigSearchParametersDTO(1))
                        .getId()
        );
    }
}