package com.example.businessserver.logic;

import com.example.businessserver.exceptions.DTONullPointerException;

public abstract class LogicDaddy {
    public void objectNullCheck(Object obj, String subjectName) throws DTONullPointerException {
        if (obj == null)
            throw new DTONullPointerException(subjectName + " cannot be Null!");
    }
}
