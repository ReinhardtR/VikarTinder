package com.example.businessserver.logic;

import com.example.businessserver.exceptions.DTONullPointerException;
import com.example.businessserver.exceptions.DTOOutOfBoundsException;

public abstract class BasicLogic {
    public void objectNullCheck(Object obj, String subjectName) throws DTONullPointerException {
        if (obj == null)
            throw new DTONullPointerException(subjectName + " cannot be Null!");
    }

    public void checkId(int id) throws DTOOutOfBoundsException {
        if (id < 1)
            throw new DTOOutOfBoundsException("Id cannot be < 1!");
    }
}
