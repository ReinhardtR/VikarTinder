package com.example.businessserver.logic;

import com.example.businessserver.exceptions.DTOException;
import com.example.businessserver.exceptions.DTOOutOfBoundsException;
import org.junit.jupiter.api.Test;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.Month;

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

    @Test
    void test_checkStringMinimumValues_DTOOutOfBoundsException() {
        String name1 = "";
        String name2 = "            ";
        String name3 = "Casper";
        String name4 = "         Simon    Sohn              ";

        assertAll(
            ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkStringMinimumValues(name1, "empty",1), "Should throw since it is empty"),
            ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkStringMinimumValues(name2, "Empty with many spaces", 1), "Should throw since its trimmed and checked for length"),
            ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkStringMinimumValues(name3, "Long min", 25)),
                ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkStringMinimumValues(name4, "LongStringWithSpaces", 11))
        );
    }

    @Test
    void test_checkAge()
    {
        LocalDate now = LocalDate.now();
        LocalDate time1 = LocalDate.of(now.getYear()-99, now.getMonth(), now.getDayOfMonth());
        LocalDate time2 = LocalDate.of(now.getYear()-18, now.getMonth(), now.getDayOfMonth());

        try {
            testLogic.checkAge(time1);
            testLogic.checkAge(time2);
        } catch (DTOOutOfBoundsException e) {
            fail(e.getMessage());
        }
    }

    @Test
    void test_checkAge_DTOOutOfBoundsException()
    {
        LocalDate now = LocalDate.now();
        LocalDate time1 = LocalDate.of(now.getYear()-100, now.getMonth(), now.getDayOfMonth());
        LocalDate time2 = LocalDate.of(now.getYear()-17, now.getMonth(), now.getDayOfMonth());

        assertAll(
                ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkAge(time1)),
                ()-> assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkAge(time2))
        );
    }

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

    @Test
    void test_checkPasswordsForMatch()
    {
        String password = "This is a test to se if the same hashed code is generated";
        String[] saltAndHashedPassword = testLogic.generateHashedPassword(password);
        try {
            testLogic.checkPasswordsForMatch(saltAndHashedPassword[1], password, saltAndHashedPassword[0]);
        } catch (DTOOutOfBoundsException e) {
            fail("Should match");
        }
    }

    @Test
    void test_checkPasswordsForMatch_DTOOutOfBoundsException()
    {
        String password = "This is a test to se if the same hashed code is generated";
        String[] saltAndHashedPassword = testLogic.generateHashedPassword(password);

        assertThrows(DTOOutOfBoundsException.class, ()-> testLogic.checkPasswordsForMatch("saltAndHashedPassword[1]", password, saltAndHashedPassword[0]));
    }
}