package com.example.businessserver.logic;

import com.example.businessserver.dtos.EmployerDTO;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class MatchingLogicImplTest {
    MatchingLogicImpl logic;

    @BeforeAll
    void createMatchingLogicImpl()
    {
        logic = new MatchingLogicImpl(null);
    }

    @Test
    void testGetSubstitutesByEmployerId()
    {
        //logic.getSubstitutesByEmployerId(new EmployerDTO("1"));
    }
}