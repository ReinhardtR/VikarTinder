package com.example.businessserver.logic;

import com.example.businessserver.dtos.*;
import com.example.businessserver.dtos.matching.GigMatchingDTO;
import com.example.businessserver.dtos.matching.GigSearchParametersDTO;
import com.example.businessserver.dtos.matching.SubstituteMatchingDTO;
import com.example.businessserver.dtos.matching.SubstituteSearchParametersDTO;
import com.example.businessserver.exceptions.DTONullPointerException;
import com.example.businessserver.exceptions.DTOOutOfBoundsException;
import com.example.businessserver.logic.interfaces.MatchingLogic;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class MatchingLogicImplTest {
    MatchingLogic logic;

    @BeforeEach
    void createMatchingLogicImpl()
    {
        logic = new MatchingLogicImpl();
    }

    @Test
    void testGetSubstitutesByEmployerId()
    {
        assertThrows(NullPointerException.class, () -> logic.getSubstitutes(new SubstituteSearchParametersDTO(1)));
    }

    @Test
    void testGetSubstitutesByEmployerIdOutOfBoundsId()
    {
        assertThrows(DTOOutOfBoundsException.class, () -> logic.getSubstitutes(new SubstituteSearchParametersDTO(0)));
    }

    @Test
    void testGetSubstitutesByEmployerIdNullPointer()
    {
        assertThrows(DTONullPointerException.class, () -> logic.getSubstitutes(null));
    }

    @Test
    void testGetSubstitutesBySubstituteId()
    {
        assertThrows(NullPointerException.class, () -> logic.getGigs(new GigSearchParametersDTO(1)));
    }

    @Test
    void testGetSubstitutesBySubstituteIdOutOfBoundsId()
    {
        assertThrows(DTOOutOfBoundsException.class, () -> logic.getGigs(new GigSearchParametersDTO(0)));
    }

    @Test
    void testGetSubstitutesBySubstituteNullPointer()
    {
        assertThrows(DTONullPointerException.class, () -> logic.getGigs(null));
    }

    @Test
    void testSendMatchRequestSubstitute()
    {
        assertThrows(NullPointerException.class, () -> logic.sendMatchRequestSubstitute(
                new MatchRequestSubstituteDTO(
                        new SubstituteMatchingDTO(1),
                        new GigMatchingDTO(1)
                )
        ));
    }

    @Test
    void testSendMatchRequestSubstituteIDsOutOfBounds()
    {
        assertAll(
                ()-> assertThrows(DTOOutOfBoundsException.class, () -> logic.sendMatchRequestSubstitute(
                        new MatchRequestSubstituteDTO(
                                new SubstituteMatchingDTO(0),
                                new GigMatchingDTO(5)
                        )
                ), "Testing if SubstituteDatingDTO id is caught as < 1"),
                () -> assertThrows(DTOOutOfBoundsException.class, () -> logic.sendMatchRequestSubstitute(
                        new MatchRequestSubstituteDTO(
                                new SubstituteMatchingDTO(5),
                                new GigMatchingDTO(0)
                        )
                ), "Testing if WorkPositionDatingDTO id is caught as < 1")
        );
    }

    @Test
    void testSendMatchRequestSubstituteDTONullPointer()
    {
        assertAll(
                () -> assertThrows(DTONullPointerException.class, () -> logic.sendMatchRequestSubstitute(null)),
                () -> assertThrows(DTONullPointerException.class, () -> logic.sendMatchRequestSubstitute(
                        new MatchRequestSubstituteDTO(
                                null,
                                new GigMatchingDTO(1)
                        )
                ), "Testing if SubstituteDatingDTO is caught as null"),
                () -> assertThrows(DTONullPointerException.class, () -> logic.sendMatchRequestSubstitute(
                        new MatchRequestSubstituteDTO(
                                new SubstituteMatchingDTO(1),
                                null)
                ), "Testing if WorkPositionDatingDTO is caught as null")
        );
    }

    @Test
    void testSendMatchRequestEmployer()
    {
        assertThrows(NullPointerException.class, () -> logic.sendMatchRequestEmployer(
                new MatchRequestEmployerDTO(
                        new EmployerDatingDTO(1),
                        new SubstituteMatchingDTO(1)
                )
        ));
    }

    @Test
    void testSendMatchRequestEmployerIDsOutOfBounds()
    {
        assertAll(
                ()-> assertThrows(DTOOutOfBoundsException.class, () -> logic.sendMatchRequestEmployer(
                        new MatchRequestEmployerDTO(
                                new EmployerDatingDTO(0),
                                new SubstituteMatchingDTO(5)
                        )
                ), "Testing if EmployerDatingDTO id is caught as < 1"),
                () -> assertThrows(DTOOutOfBoundsException.class, () -> logic.sendMatchRequestEmployer(
                        new MatchRequestEmployerDTO(
                                new EmployerDatingDTO(5),
                                new SubstituteMatchingDTO(0)
                        )
                ), "Testing if SubstituteDatingDTO id is caught as < 1")
        );
    }

    @Test
    void testSendMatchRequestEmployerDTONullPointer()
    {
        assertAll(
                () -> assertThrows(DTONullPointerException.class, () -> logic.sendMatchRequestEmployer(null)),
                () -> assertThrows(DTONullPointerException.class, () -> logic.sendMatchRequestEmployer(
                        new MatchRequestEmployerDTO(
                                null,
                                new SubstituteMatchingDTO(1)
                        )
                ), "Testing if EmployerDatingDTO is caught as null"),
                () -> assertThrows(DTONullPointerException.class, () -> logic.sendMatchRequestEmployer(
                        new MatchRequestEmployerDTO(
                                new EmployerDatingDTO(1),
                                null)
                ), "Testing if SubstituteDatingDTO is caught as null")
        );
    }
}