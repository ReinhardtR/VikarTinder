package com.example.businessserver.services.builders;

import com.example.businessserver.dtos.DatingSearchParametersEmployee;
import com.example.businessserver.dtos.DatingSearchParametersSubstitute;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class MatchingBuilderTest {
    GRPCBuilder matchingBuilder = new GRPCBuilder();

    @Test
    void testCreateEmployerId()
    {
        assertEquals(
                1,
                matchingBuilder.buildEmployerId(
                        new DatingSearchParametersEmployee(1))
                        .getId())
        ;
    }

    @Test
    void testCreateSubstituteId()
    {
        assertEquals(
                1,
                matchingBuilder.buildSubstituteId(
                new DatingSearchParametersSubstitute(1))
                .getId()
        );
    }
}