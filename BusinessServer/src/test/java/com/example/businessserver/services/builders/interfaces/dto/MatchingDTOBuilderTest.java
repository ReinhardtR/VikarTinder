package com.example.businessserver.services.builders.interfaces.dto;



import MatchingProto.MatchValidation;
import MatchingProto.MatchingSubstitutes;
import MatchingProto.SubstituteToBeMatched;
import com.example.businessserver.services.builders.DTOBuilder;
import com.example.businessserver.services.builders.GRPCBuilder;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

class MatchingDTOBuilderTest {
    MatchingDTOBuilder dtoBuilder = new DTOBuilder();

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
        MatchingSubstitutes test = createSubstitutesForMatching(10);
        for (int i = 0; i < test.getSubstitutesList().size(); i++) {
            assertEquals(i, test.getSubstitutes(i).getId());
        }
    }


    @Test
    void testMatchValidationDTO()
    {
        MatchValidation validation = MatchValidation.newBuilder()
                .setIsMatched(true)
                .setMatchId(25).build();
        assertAll(
                () -> assertEquals(
                        25,
                        validation.getMatchId()
                ),
                () -> assertEquals(
                        true,
                        validation.getIsMatched()
                )
        );
    }

    private MatchingSubstitutes createSubstitutesForMatching(int length)
    {
        return MatchingSubstitutes.newBuilder().
                addAllSubstitutes(
                        sunnyListOfSubstitutes(length)
                ).build();
    }

    private List<SubstituteToBeMatched> sunnyListOfSubstitutes(int length)
    {
        List<SubstituteToBeMatched> toReturn = new ArrayList<>();
        for (int i = 0; i < length; i++) {
            toReturn.add(getSubstituteId(i));
        }
        return toReturn;
    }

    private SubstituteToBeMatched getSubstituteId(int id)
    {
        return SubstituteToBeMatched.newBuilder()
                .setId(id).build();
    }
}