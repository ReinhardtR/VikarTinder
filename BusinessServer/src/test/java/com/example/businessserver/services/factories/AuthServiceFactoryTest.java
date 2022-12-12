package com.example.businessserver.services.factories;

import AdministrationService.*;
import com.example.businessserver.dtos.auth.LoginEmployerResponseDTO;
import com.example.businessserver.dtos.auth.LoginSubstituteResponseDTO;
import com.example.businessserver.dtos.auth.LoginUserResponseDTO;
import com.example.businessserver.exceptions.BuildingException;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class AuthServiceFactoryTest {

    @Test
    void test_createLoginRequest() {
        String email = "testEmail";
        CreateLoginRequest testResult = AuthServiceFactory.createLoginRequest(email);
        assertAll(
                ()-> assertTrue(testResult.getEmail().equals(email))
        );
    }

    @Test
    void test_userObjectDTO() throws Exception {
        int id = 1;
        String firstName = "firstNameTest";
        String lastName = "lastNameTest";
        String password = "testPassword";
        String email = "test@email.test";

        //employer
        String title = "testTitle";
        String workplace = "testWorkplace";

        test_userObjectDTO_EmployerDTOBuildUp(id, firstName, lastName, password, email, title, workplace);

        //substitute
        int age = 55;
        String bio = "This is a test";
        String address = "testAddress";

        test_userObjectDTO_SubstituteDTOBuildUp(id, firstName, lastName, password, email, age, bio, address);
    }

    void test_userObjectDTO_EmployerDTOBuildUp(int id, String firstName, String lastName, String password, String email, String title, String workplace) throws Exception {
        EmployerObject empTest = buildEmployerObj(title, workplace);
        UserData userDataTest = buildUserDataObj(firstName, lastName, password, email, empTest);
        UserObject testGrpc = buildUserObj(id, userDataTest);

        LoginEmployerResponseDTO testResult = (LoginEmployerResponseDTO) AuthServiceFactory.userObjectDTO(testGrpc);
        assertAll(
                ()-> assertEquals(testResult.getFirstName(), firstName),
                ()-> assertEquals(testResult.getLastName(), lastName),
                ()-> assertEquals(testResult.getPasswordHashed(), password),
                ()-> assertEquals(testResult.getEmail(), email),
                ()-> assertEquals(testResult.getTitle(), title),
                ()-> assertEquals(workplace,testResult.getWorkplace()),
                ()-> assertEquals(LoginUserResponseDTO.Role.EMPLOYER, testResult.getRole())
        );
    }

    void test_userObjectDTO_SubstituteDTOBuildUp(int id, String firstName, String lastName, String password, String email, int age, String bio, String address) throws Exception {
        SubstituteObject subTest = buildSubstituteObj(age, bio, address);
        UserData userDataTest = buildUserDataObj(firstName, lastName, password, email, subTest);
        UserObject testGrpc = buildUserObj(id, userDataTest);

        LoginSubstituteResponseDTO testResult = (LoginSubstituteResponseDTO) AuthServiceFactory.userObjectDTO(testGrpc);
        assertAll(
                ()-> assertEquals(firstName,testResult.getFirstName()),
                ()-> assertEquals(lastName, testResult.getLastName()),
                ()-> assertEquals(password, testResult.getPasswordHashed()),
                ()-> assertEquals(email, testResult.getEmail()),
                ()-> assertEquals(age, testResult.getAge()),
                ()-> assertEquals(bio, testResult.getBio()),
                ()-> assertEquals(LoginUserResponseDTO.Role.SUBSTITUTE, testResult.getRole())
        );
    }

    @Test
    void test_userObjectDTO_SubstituteDTOBuildUp()
    {
        UserData data = UserData.newBuilder().build();
        UserObject obj = UserObject.newBuilder().setUserData(data).build();

        assertThrows(BuildingException.class, ()-> AuthServiceFactory.userObjectDTO(obj), "Checks if system throws exception in case more roles being added to UserData");
    }

    private SubstituteObject buildSubstituteObj(int age, String bio, String address) {
        return SubstituteObject.newBuilder()
                .setAge(age)
                .setBio(bio)
                .setAddress(address)
                .build();
    }

    private EmployerObject buildEmployerObj(String title, String workplace) {
        return EmployerObject.newBuilder()
                .setTitle(title)
                .setWorkplace(workplace)
                .build();
    }

    private UserData buildUserDataObj(String firstName, String lastName, String password, String email, EmployerObject emp) {
        return UserData.newBuilder()
                .setFirstName(firstName)
                .setLastName(lastName)
                .setPasswordHash(password)
                .setEmail(email)
                .setEmp(emp)
                .build();
    }

    private UserData buildUserDataObj(String firstName, String lastName, String password, String email, SubstituteObject sub) {
        return UserData.newBuilder()
                .setFirstName(firstName)
                .setLastName(lastName)
                .setPasswordHash(password)
                .setEmail(email)
                .setSub(sub)
                .build();
    }

    private UserObject buildUserObj(int id, UserData userData) {
        return UserObject.newBuilder()
                .setId(id)
                .setUserData(userData)
                .build();
    }
}