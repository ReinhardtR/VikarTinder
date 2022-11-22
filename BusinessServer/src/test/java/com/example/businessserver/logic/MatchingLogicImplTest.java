package com.example.businessserver.logic;

import com.example.businessserver.dtos.*;
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
        assertThrows(NullPointerException.class, () -> logic.getSubstitutesByEmployerId(new DatingSearchParametersEmployee(1)));
    }

    @Test
    void testGetSubstitutesByEmployerIdOutOfBoundsId()
    {
        assertThrows(DTOOutOfBoundsException.class, () -> logic.getSubstitutesByEmployerId(new DatingSearchParametersEmployee(0)));
    }

    @Test
    void testGetSubstitutesByEmployerIdNullPointer()
    {
        assertThrows(DTONullPointerException.class, () -> logic.getSubstitutesByEmployerId(null));
    }

    @Test
    void testGetSubstitutesBySubstituteId()
    {
        assertThrows(NullPointerException.class, () -> logic.getWorkPositionsBySubstituteId(new DatingSearchParametersSubstitute(1)));
    }

    @Test
    void testGetSubstitutesBySubstituteIdOutOfBoundsId()
    {
        assertThrows(DTOOutOfBoundsException.class, () -> logic.getWorkPositionsBySubstituteId(new DatingSearchParametersSubstitute(0)));
    }

    @Test
    void testGetSubstitutesBySubstituteNullPointer()
    {
        assertThrows(DTONullPointerException.class, () -> logic.getWorkPositionsBySubstituteId(null));
    }

    @Test
    void testSendMatchRequestSubstitute()
    {
        assertThrows(NullPointerException.class, () -> logic.sendMatchRequestSubstitute(
                new MatchRequestSubstituteDTO(
                        new SubstituteDatingDTO(1),
                        new WorkPositionDatingDTO(1)
                )
        ));
    }

    @Test
    void testSendMatchRequestSubstituteIDsOutOfBounds()
    {
        assertAll(
                ()-> assertThrows(DTOOutOfBoundsException.class, () -> logic.sendMatchRequestSubstitute(
                        new MatchRequestSubstituteDTO(
                                new SubstituteDatingDTO(0),
                                new WorkPositionDatingDTO(5)
                        )
                ), "Testing if SubstituteDatingDTO id is caught as < 1"),
                () -> assertThrows(DTOOutOfBoundsException.class, () -> logic.sendMatchRequestSubstitute(
                        new MatchRequestSubstituteDTO(
                                new SubstituteDatingDTO(5),
                                new WorkPositionDatingDTO(0)
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
                                new WorkPositionDatingDTO(1)
                        )
                ), "Testing if SubstituteDatingDTO is caught as null"),
                () -> assertThrows(DTONullPointerException.class, () -> logic.sendMatchRequestSubstitute(
                        new MatchRequestSubstituteDTO(
                                new SubstituteDatingDTO(1),
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
                        new SubstituteDatingDTO(1)
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
                                new SubstituteDatingDTO(5)
                        )
                ), "Testing if EmployerDatingDTO id is caught as < 1"),
                () -> assertThrows(DTOOutOfBoundsException.class, () -> logic.sendMatchRequestEmployer(
                        new MatchRequestEmployerDTO(
                                new EmployerDatingDTO(5),
                                new SubstituteDatingDTO(0)
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
                                new SubstituteDatingDTO(1)
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