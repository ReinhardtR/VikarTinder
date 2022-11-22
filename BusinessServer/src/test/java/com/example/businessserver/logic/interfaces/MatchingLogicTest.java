package com.example.businessserver.logic.interfaces;

import com.example.businessserver.dtos.matching.SubstituteSearchParametersDTO;
import com.example.businessserver.dtos.matching.GigSearchParametersDTO;
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
        assertThrows(NullPointerException.class, () -> logic.getSubstitutes(new SubstituteSearchParametersDTO(1)));
    }

    @Test
    void testGetSubstitutesByEmployerIdOutOfBoundsId()
    {
        assertThrows(DTOOutOfBoundsException.class, () -> logic.getSubstitutes(new SubstituteSearchParametersDTO(-1)));
    }

    @Test
    void testGetSubstitutesBySubstituteId()
    {
        assertThrows(NullPointerException.class, () -> logic.getGigs(new GigSearchParametersDTO(1)));
    }

    @Test
    void testGetSubstitutesBySubstituteIdOutOfBoundsId()
    {
        assertThrows(DTOOutOfBoundsException.class, () -> logic.getGigs(new GigSearchParametersDTO(-1)));
    }
}