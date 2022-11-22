package com.example.businessserver.services.builders.interfaces.dto;

import UserService.SubstituteId;
import UserService.SubstitutesForMatching;
import com.example.businessserver.dtos.matching.GigSearchParametersDTO;
import com.example.businessserver.services.builders.DTOBuilder;
import com.example.businessserver.services.builders.GRPCBuilder;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

class MatchingDTOBuilderTest {
    MatchingDTOBuilder dtoBuilder = new DTOBuilder();
    GRPCBuilder grpcBuilder = new GRPCBuilder();

    @Test
    void testBuildSubstituteDatesLength()
    {
        assertAll(
                () -> assertEquals(
                        0,
                        dtoBuilder.substituteMatchingDTOs(
                                        createSubstitutesForMatching(0)
                                )
                                .getPossibleMatches().size()
                ),
                () -> assertEquals(
                        1,
                        dtoBuilder.substituteMatchingDTOs(
                                        createSubstitutesForMatching(1)
                                )
                                .getPossibleMatches().size()
                ),
                () -> assertEquals(
                        5,
                        dtoBuilder.substituteMatchingDTOs(
                                        createSubstitutesForMatching(5)
                                )
                                .getPossibleMatches().size()
                )
        );
    }

    @Test
    void testBuildSubstituteDatesCorrectBuildupId()
    {
        SubstitutesForMatching test = createSubstitutesForMatching(10);
        for (int i = 0; i < test.getSubstitutesList().size(); i++) {
            assertEquals(i, test.getSubstitutes(i).getId());
        }
    }

    private SubstitutesForMatching createSubstitutesForMatching(int length)
    {
        return SubstitutesForMatching.newBuilder().
                addAllSubstitutes(
                        sunnyListOfSubstitutes(length)
                ).build();
    }

    private List<SubstituteId> sunnyListOfSubstitutes(int length)
    {
        List<SubstituteId> toReturn = new ArrayList<>();
        for (int i = 0; i < length; i++) {
            toReturn.add(getSubstituteId(i));
        }
        return toReturn;
    }

    private SubstituteId getSubstituteId(int id)
    {
        return grpcBuilder.buildSubstituteId(new GigSearchParametersDTO(id));
    }
}