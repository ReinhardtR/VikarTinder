package com.example.businessserver.logic.interfaces;

import com.example.businessserver.dtos.DatingSearchParametersEmployee;
import com.example.businessserver.dtos.DatingSearchParametersSubstitute;
import com.example.businessserver.exceptions.DTOOutOfBoundsException;
import com.example.businessserver.logic.MatchingLogicImpl;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class MatchingLogicTest {
    MatchingLogic logic;

    @BeforeEach
    void createMatchingLogicImpl()
    {
        logic = new MatchingLogicImpl();
    }

    @Test
    void testGetSubstitutesByEmployerId()
    {
        assertThrows(NullPointerException.class, () -> logic.getSubstitutesByEmployerId(new DatingSearchParametersEmployee(1)));
    }

    @Test
    void testGetSubstitutesByEmployerIdOutOfBoundsId()
    {
        assertThrows(DTOOutOfBoundsException.class, () -> logic.getSubstitutesByEmployerId(new DatingSearchParametersEmployee(-1)));
    }

    @Test
    void testGetSubstitutesBySubstituteId()
    {
        assertThrows(NullPointerException.class, () -> logic.getWorkPositionsBySubstituteId(new DatingSearchParametersSubstitute(1)));
    }

    @Test
    void testGetSubstitutesBySubstituteIdOutOfBoundsId()
    {
        assertThrows(DTOOutOfBoundsException.class, () -> logic.getWorkPositionsBySubstituteId(new DatingSearchParametersSubstitute(-1)));
    }
}