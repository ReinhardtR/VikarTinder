package com.example.businessserver.logic;

import com.example.businessserver.dtos.*;
import com.example.businessserver.dtos.matching.*;
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
    void testGetSubstitutes()
    {
        assertThrows(NullPointerException.class, () -> logic.getSubstitutes(new SubstituteSearchParametersDTO(1)));
    }

    @Test
    void testGetSubstitutesOutOfBoundsId()
    {
        assertThrows(DTOOutOfBoundsException.class, () -> logic.getSubstitutes(new SubstituteSearchParametersDTO(0)));
    }

    @Test
    void testGetSubstitutesNullPointer()
    {
        assertThrows(DTONullPointerException.class, () -> logic.getSubstitutes(null));
    }

    @Test
    void testGetGigs()
    {
        assertThrows(NullPointerException.class, () -> logic.getGigs(new GigSearchParametersDTO(1)));
    }

    @Test
    void testGetGigsIdOutOfBoundsId()
    {
        assertThrows(DTOOutOfBoundsException.class, () -> logic.getGigs(new GigSearchParametersDTO(0)));
    }

    @Test
    void testGetGigsNullPointer()
    {
        assertThrows(DTONullPointerException.class, () -> logic.getGigs(null));
    }

    @Test
    void testGigMatchRequest()
    {
        assertThrows(NullPointerException.class, () -> logic.gigsMatchRequest(
                new MatchRequestDTO(
                        1,
                        1
                )
        ));
    }

    @Test
    void testGigMatchRequestIDsOutOfBounds()
    {
        assertAll(
                ()-> assertThrows(DTOOutOfBoundsException.class, () -> logic.gigsMatchRequest(
                        new MatchRequestDTO(
                                0,
                                5
                        )
                ), "Testing if currentUser id is caught as < 1"),
                () -> assertThrows(DTOOutOfBoundsException.class, () -> logic.gigsMatchRequest(
                        new MatchRequestDTO(
                                5,
                                0
                        )
                ), "Testing if match id is caught as < 1")
        );
    }

    @Test
    void testGigMatchRequestDTONullPointer()
    {
        assertThrows(DTONullPointerException.class, () -> logic.gigsMatchRequest(null));
    }
}