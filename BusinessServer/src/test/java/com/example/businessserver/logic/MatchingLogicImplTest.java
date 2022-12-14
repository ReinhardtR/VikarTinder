package com.example.businessserver.logic;

import com.example.businessserver.dtos.matching.*;
import com.example.businessserver.exceptions.DTOException;
import com.example.businessserver.exceptions.DTONullPointerException;
import com.example.businessserver.exceptions.DTOOutOfBoundsException;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class MatchingLogicImplTest {
    MatchingLogicImpl logic = new MatchingLogicImpl();
    @Test
    void test_checkMatch_SunnyScenario()
    {
        try {
            logic.checkMatch(new MatchRequestDTO(1,1, true));
        } catch (DTOException e) {
            fail("Should Not Throw");
        }
    }

    @Test
    void test_checkMatch_DTONullPointerException()
    {
        assertThrows(DTONullPointerException.class, ()-> logic.checkMatch(null));
    }

    @Test
    void test_checkMatch_DTOOutOfBoundsException()
    {
        assertAll(
                ()-> assertThrows(DTOOutOfBoundsException.class, () -> logic.checkMatch(
                        new MatchRequestDTO(
                                0,
                                5,
                                true
                        )
                ), "Testing if currentUser id is caught as < 1"),
                () -> assertThrows(DTOOutOfBoundsException.class, () -> logic.checkMatch(
                        new MatchRequestDTO(
                                5,
                                0,
                                true
                        )
                ), "Testing if match id is caught as < 1")
        );
    }
}