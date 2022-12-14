package com.example.businessserver.logic;

import com.example.businessserver.exceptions.DTONullPointerException;
import com.example.businessserver.exceptions.DTOOutOfBoundsException;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class BasicLogicTest {
    BasicLogic logic = new BasicLogic() {
        @Override
        public void objectNullCheck(Object obj, String subjectName) throws DTONullPointerException {
            super.objectNullCheck(obj, subjectName);
        }
    };

    @Test
    void test_checkId_SunnyScenario()
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
    void test_checkId_DTOOutOfBoundsException()
    {
        assertThrows(DTOOutOfBoundsException.class, () -> logic.checkId(0));
    }

    @Test
    void test_objectNullCheck_SunnyScenario()
    {
        try {
            logic.objectNullCheck(new Object(), "test");
        } catch (DTONullPointerException e) {
            fail("Should Not Throw");
        }
    }

    @Test
    void test_objectNullCheck_DTONullPointerException()
    {
        assertThrows(DTONullPointerException.class, () -> logic.objectNullCheck(null, "test"));
    }
}