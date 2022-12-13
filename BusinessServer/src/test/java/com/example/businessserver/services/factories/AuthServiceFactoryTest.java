package com.example.businessserver.services.factories;

import AuthService.*;
import com.example.businessserver.dtos.auth.*;
import com.example.businessserver.exceptions.BuildingException;
import org.junit.jupiter.api.Test;

import java.time.LocalDateTime;

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
        LocalDateTime age = LocalDateTime.now();
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

    void test_userObjectDTO_SubstituteDTOBuildUp(int id, String firstName, String lastName, String password, String email, LocalDateTime age, String bio, String address) throws Exception {
        SubstituteObject subTest = buildSubstituteObj(age, bio, address);
        UserData userDataTest = buildUserDataObj(firstName, lastName, password, email, subTest);
        UserObject testGrpc = buildUserObj(id, userDataTest);

        LoginSubstituteResponseDTO testResult = (LoginSubstituteResponseDTO) AuthServiceFactory.userObjectDTO(testGrpc);
        assertAll(
                ()-> assertEquals(firstName,testResult.getFirstName()),
                ()-> assertEquals(lastName, testResult.getLastName()),
                ()-> assertEquals(password, testResult.getPasswordHashed()),
                ()-> assertEquals(email, testResult.getEmail()),
                ()-> assertEquals(age.withNano(0), testResult.getBirthDate()),
                ()-> assertEquals(bio, testResult.getBio()),
                ()-> assertEquals(LoginUserResponseDTO.Role.SUBSTITUTE, testResult.getRole())
        );
    }

    @Test
    void test_userObjectDTO_BuildingException()
    {
        UserData data = UserData.newBuilder().build();
        UserObject obj = UserObject.newBuilder().setUserData(data).build();

        assertThrows(BuildingException.class, ()-> AuthServiceFactory.userObjectDTO(obj), "Checks if system throws exception in case more roles being added to UserData");
    }

    @Test //TODO : Generaliser testne
    void test_createUserRequestEmployer() throws Exception {
        String firstName = "firstNameTest";
        String lastName = "lastNameTest";
        String password = "testPassword";
        String email = "test@email.test";
        String title = "testTitle";
        String workplace = "testWorkplace";
        SignUpEmployerRequestDTO testDTO = new SignUpEmployerRequestDTO(
            firstName,
            lastName,
            password,
            email,
            title,
            workplace
        );

        String[] saltAndPassword = new String[]{"testSalt", "testWord"};

        CreateUserRequest test = AuthServiceFactory.createUserRequestEmployer(new SignUpWrapperEmployerDTO(saltAndPassword[0], saltAndPassword[1], testDTO));

        assertAll(
                ()->assertEquals(firstName, test.getUser().getFirstName()),
                ()-> assertEquals(lastName, test.getUser().getLastName()),
                ()-> assertEquals(saltAndPassword[0], test.getUser().getSalt()),
                ()->assertEquals(saltAndPassword[1], test.getUser().getPasswordHash()),
                ()->assertEquals(email, test.getUser().getEmail()),
                ()->assertEquals(title, test.getUser().getEmp().getTitle()),
                ()->assertEquals(workplace, test.getUser().getEmp().getWorkplace())
        );
    }

    @Test
    void test_createUserRequestSubstitute()
    {
        String firstName = "firstNameTest";
        String lastName = "lastNameTest";
        String password = "testPassword";
        String email = "test@email.test";
        LocalDateTime localDateTime = LocalDateTime.now();
        String bio = "This is a test";
        String address = "testDress";
        SignUpSubstituteRequestDTO testDTO = new SignUpSubstituteRequestDTO(
                firstName,
                lastName,
                password,
                email,
                localDateTime,
                bio,
                address
        );

        String[] saltAndPassword = new String[]{"testSalt", "testWord"};

        CreateUserRequest test = AuthServiceFactory.createUserRequestSubstitute(new SignUpWrapperSubstituteDTO(saltAndPassword[0], saltAndPassword[1], testDTO));
        assertAll(
                ()->assertEquals(firstName, test.getUser().getFirstName()),
                ()->assertEquals(lastName, test.getUser().getLastName()),
                ()->assertEquals(saltAndPassword[0], test.getUser().getSalt()),
                ()->assertEquals(saltAndPassword[1], test.getUser().getPasswordHash()),
                ()->assertEquals(email, test.getUser().getEmail()),
                ()->assertEquals(localDateTime.withNano(0), SharedFactory.toLocalDateTime(test.getUser().getSub().getBirthDate())),
                ()->assertEquals(bio, test.getUser().getSub().getBio()),
                ()->assertEquals(address, test.getUser().getSub().getAddress())
        );
    }

    @Test
    void test_getUserRequest()
    {
        LoginUserResponseDTO.Role role = LoginUserResponseDTO.Role.SUBSTITUTE;

        GetUserInfoParamsDTO testInfo = new GetUserInfoParamsDTO(5, role);

        assertAll(
                ()->assertEquals(5, testInfo.getId()),
                ()->assertEquals(role, testInfo.getRole())
        );
    }

    @Test
    void test_employerInfoDTO()
    {
        String firstName = "firstNameTest";
        String lastName = "lastNameTest";
        String title = "testTitle";
        String workplace = "testWorkplace";
        GetUserResponse testResponse = GetUserResponse.newBuilder()
                .setUser(
                        UserInfo.newBuilder()
                                .setFirstName(firstName)
                                .setLastName(lastName)
                                .setEmp(
                                        EmployerObject.newBuilder()
                                                .setTitle(title)
                                                .setWorkplace(workplace)
                                ).build()
                ).build();
        EmployerInfoDTO testDTO = AuthServiceFactory.employerInfoDTO(testResponse);
        assertAll(
                ()->assertEquals(firstName, testDTO.getFirstName()),
                ()->assertEquals(lastName, testDTO.getLastName()),
                ()->assertEquals(title, testDTO.getTitle()),
                ()->assertEquals(workplace, testDTO.getWorkplace())
        );
    }

    @Test
    void test_substituteInfoDTO()
    {
        String firstName = "firstNameTest";
        String lastName = "lastNameTest";
        LocalDateTime localDateTime = LocalDateTime.now();
        String bio = "This is a test";
        String address = "testDress";

        GetUserResponse testResponse = GetUserResponse.newBuilder()
                .setUser(
                        UserInfo.newBuilder()
                                .setFirstName(firstName)
                                .setLastName(lastName)
                                .setSub(
                                        SubstituteObject.newBuilder()
                                                .setBirthDate(SharedFactory.toTimestamp(localDateTime))
                                                .setBio(bio)
                                                .setAddress(address)
                                ).build()
                ).build();

        SubstituteInfoDTO testDTO = AuthServiceFactory.substituteInfoDTO(testResponse);
        assertAll(
                ()->assertEquals(firstName, testDTO.getFirstName()),
                ()->assertEquals(lastName, testDTO.getLastName()),
                ()->assertEquals(localDateTime.withNano(0), testDTO.getBirthDate()),
                ()->assertEquals(bio, testDTO.getBio()),
                ()->assertEquals(address, testDTO.getAddress())
        );
    }

    @Test
    void test_updateUserRequestSub()
    {
        int id = 1;
        String firstName = "firstNameTest";
        String lastName = "lastNameTest";
        LocalDateTime localDateTime = LocalDateTime.now();
        String bio = "This is a test";
        String address = "testDress";

        SubstituteInfoDTO info = new SubstituteInfoDTO(firstName, lastName, localDateTime, bio, address);
        UpdateSubstituteInfoDTO update = new UpdateSubstituteInfoDTO(id, info);
        UpdateUserRequest updateTest = AuthServiceFactory.updateUserRequestSub(update);
        assertAll(
                ()->assertEquals(id, updateTest.getId()),
                ()->assertEquals(firstName, updateTest.getUser().getFirstName()),
                ()->assertEquals(lastName, updateTest.getUser().getLastName()),
                ()->assertEquals(localDateTime.withNano(0), SharedFactory.toLocalDateTime(updateTest.getUser().getSub().getBirthDate())),
                ()->assertEquals(bio, updateTest.getUser().getSub().getBio()),
                ()->assertEquals(address, updateTest.getUser().getSub().getAddress())
        );
    }

    @Test
    void test_updateUserRequestEmp()
    {
        int id = 1;
        String firstName = "firstNameTest";
        String lastName = "lastNameTest";
        String title = "testTitle";
        String workplace = "testWorkplace";

        EmployerInfoDTO info = new EmployerInfoDTO(firstName, lastName, title, workplace);
        UpdateEmployerInfoDTO update = new UpdateEmployerInfoDTO(id, info);
        UpdateUserRequest updateTest = AuthServiceFactory.updateUserRequestEmp(update);

        assertAll(
                ()->assertEquals(id, updateTest.getId()),
                ()->assertEquals(firstName, updateTest.getUser().getFirstName()),
                ()->assertEquals(lastName, updateTest.getUser().getLastName()),
                ()->assertEquals(title, updateTest.getUser().getEmp().getTitle()),
                ()->assertEquals(workplace, updateTest.getUser().getEmp().getWorkplace())
        );
    }

    @Test
    void test_deleteUserRequest()
    {
        int id = 1;
        LoginUserResponseDTO.Role role = LoginUserResponseDTO.Role.SUBSTITUTE;

        DeleteRequestDTO dtoTest = new DeleteRequestDTO(id, role);
        DeleteUserRequest deleteTest = AuthServiceFactory.deleteUserRequest(dtoTest);
        
        assertAll(
                ()->assertEquals(id, deleteTest.getUser().getId()),
                ()->assertEquals(role.toString(), deleteTest.getUser().getRole().toString())
        );
    }

    private SubstituteObject buildSubstituteObj(LocalDateTime age, String bio, String address) {
        return SubstituteObject.newBuilder()
                .setBirthDate(SharedFactory.toTimestamp(age))
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