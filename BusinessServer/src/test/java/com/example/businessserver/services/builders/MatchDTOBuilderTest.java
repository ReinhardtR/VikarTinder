package com.example.businessserver.services.builders;

import MatchingProto.MatchValidation;
import MatchingProto.MatchingSubstitutes;
import MatchingProto.SubstituteToBeMatched;
import com.example.businessserver.dtos.matching.MatchValidationDTO;
import com.example.businessserver.dtos.matching.SubstituteMatchingDTOs;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

class MatchDTOBuilderTest {

    @Test
    void test_substituteMatchingDTOs_PossibleMatchesLength()
    {
        assertAll(
                () -> assertEquals(
                        0,
                        MatchDTOBuilder.substituteMatchingDTOs(
                                        createMatchingSubstitutes(0)
                                )
                                .getPossibleMatches().size()
                , "Empty list should be made"),
                () -> assertEquals(
                        1,
                        MatchDTOBuilder.substituteMatchingDTOs(
                                        createMatchingSubstitutes(1)
                                )
                                .getPossibleMatches().size()
                , "List should contain 1 mtach"),
                () -> assertEquals(
                        5,
                        MatchDTOBuilder.substituteMatchingDTOs(
                                        createMatchingSubstitutes(5)
                                )
                                .getPossibleMatches().size()
                , "List should contain 5 matches")
        );
    }

    @Test
    void test_substituteMatchingDTOs_CorrectPossibleMatchesBuildUp()
    {
        MatchingSubstitutes grpc = createMatchingSubstitutes(10);
        SubstituteMatchingDTOs test = MatchDTOBuilder.substituteMatchingDTOs(grpc);
        for (int i = 0; i < test.getPossibleMatches().size(); i++) {
            assertEquals(i, test.getPossibleMatches().get(i).getId(), "Substitute id should have been " + i);
        }
    }


    @Test
    void test_matchValidationDTO_CorrectBuildUp()
    {
        MatchValidation validation = MatchValidation.newBuilder()
                .setIsMatched(true)
                .setEmployerId(25)
                .setSubstituteId(5)
                .setGigId(2)
                .build();
        MatchValidationDTO test = MatchDTOBuilder.matchValidationDTO(validation);
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

    private MatchingSubstitutes createMatchingSubstitutes(int length)
    {
        return MatchingSubstitutes.newBuilder().
                addAllSubstitutes(
                        validListOfSubstitutesToBeMatched(length)
                ).build();
    }

    private List<SubstituteToBeMatched> validListOfSubstitutesToBeMatched(int length)
    {
        List<SubstituteToBeMatched> toReturn = new ArrayList<>();
        for (int i = 0; i < length; i++) {
            toReturn.add(makeSubstituteToBeMatched(i));
        }
        return toReturn;
    }

    private SubstituteToBeMatched makeSubstituteToBeMatched(int id)
    {
        return SubstituteToBeMatched.newBuilder()
                .setId(id).build();
    }
}