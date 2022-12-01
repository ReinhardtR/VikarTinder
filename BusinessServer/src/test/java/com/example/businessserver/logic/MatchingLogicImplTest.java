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
    void testCheckId()
    {
        try {
            logic.checkId(1);
            logic.checkId(5);
            logic.checkId(10000);
        } catch (DTOOutOfBoundsException e) {
            fail("Should not throw");
        }
    }

    @Test
    void testCheckIdOutOfBoundsException()
    {
        assertThrows(DTOOutOfBoundsException.class, () -> logic.checkId(0));
    }

    @Test
    void testObjectNullCheck()
    {
        try {
            logic.objectNullCheck(new Object(), "test");
        } catch (DTONullPointerException e) {
            fail("Should Not Throw");
        }
    }

    @Test
    void testObjectNullCheckNullPointerException()
    {
        assertThrows(DTONullPointerException.class, () -> logic.objectNullCheck(null, "test"));
    }

    @Test
    void testCheckMatch()
    {
        try {
            logic.checkMatch(new MatchRequestDTO(1,1, true));
        } catch (DTOException e) {
            fail("Should Not Throw");
        }
    }

    @Test
    void testCheckMatchNullPointerException()
    {
        assertThrows(DTONullPointerException.class, ()-> logic.checkMatch(null));
    }

    @Test
    void testCheckMatchOutOfBoundsException()
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