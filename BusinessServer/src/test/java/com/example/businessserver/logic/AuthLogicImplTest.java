package com.example.businessserver.logic;

import com.example.businessserver.exceptions.DTOException;
import com.example.businessserver.exceptions.DTOOutOfBoundsException;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class AuthLogicImplTest {
    AuthLogicImpl testLogic = new AuthLogicImpl();

    @Test
    void test_checkEmail()
    {
        String email = "test@test.test";
        try {
            testLogic.checkEmail(email);
        } catch (DTOException e) {
            fail(e.getMessage() + "\nShould be a legal email");
        }
    }

    @Test
    void test_checkEmail_DTOOutOfBoundsException()
    {
        String emailNoAtSign = "testtest.test";
        String emailNoDot = "test@testtest";
        String emailNoFront = "@test.test";
        String emailEmpty = "";

        assertAll(
                ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkEmail(emailNoAtSign)),
                ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkEmail(emailNoDot)),
                ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkEmail(emailNoFront)),
                ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkEmail(emailEmpty)),
                ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkEmail(emailEmpty))
        );
    }

    @Test
    void test_checkPassword()
    {
        String password1 = "Test_test";
        String password2 = "test_tEst";
        try {
            testLogic.checkPassword(password1);
            testLogic.checkPassword(password2);
        } catch (DTOException e) {
            fail(e.getMessage() + "\nShould be a legal password");
        }
    }

    @Test
    void test_checkPassword_DTOOutOfBoundsException()
    {
        String passwordNoUppercase = "test_test";
        String passwordShort = "test";
        String passwordEmpty = "";

        assertAll(
                ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkPassword(passwordNoUppercase), "Sees if it can detect no uppercase's"),
                ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkPassword(passwordShort)),
                ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkPassword(passwordEmpty))
                );
    }

    @Test
    void test_checkStringMinimumValues()
    {
        String test = "test";
        try {
            testLogic.checkStringMinimumValues(test, test, 1);
        } catch (DTOException e) {
            fail(e.getMessage() + "\nShould be a legal name");
        }
    }

    @Test //Denne test omfanger ikke alt, hvad nu hvis man skriver i fornavn "Simon             Sohn", trim fjærner kun front af string. TODO hvis vi når til det. Ligenu får næste lag heller ikke et trimmed version af navnet, så databasen kan godt gemme "       Simion  ", hva nu hvis vi giver den en -value af længde?
    void test_checkName_DTOOutOfBoundsException() throws DTOException {
        String name1 = "";
        String name2 = "            ";
        String name3 = "Casper";

        assertAll(
            ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkStringMinimumValues(name1, "empty",1), "Should throw since it is empty"),
            ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkStringMinimumValues(name2, "Empty with many spaces", 1), "Should throw since its trimmed and checked for length"),
            ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkStringMinimumValues(name3, "Long min", 25))
        );
    }








    /*
    //TODO:Skriv om i testene for hashing
    @Test
    void test_generateHashedPassword()
    {
        String password = "This is a test to se if the same hashed code is generated";
        assertNotEquals(testLogic.generateHashedPassword(password), testLogic.generateHashedPassword(password), "Should not be the same");
    }

    @Test
    void test_generatePasswordWithKnownSalt()
    {
        String password = "This is a test to se if the same hashed code is generated";
        String salt = "[B@3cc2931c";
        assertEquals(testLogic.generatePasswordWithKnownSalt(salt,password), testLogic.generatePasswordWithKnownSalt(salt,password), "Should be the same");
    }
    */
}