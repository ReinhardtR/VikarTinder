package com.example.businessserver.logic;

import com.example.businessserver.dtos.auth.JwtResponseDTO;
import com.example.businessserver.dtos.auth.LoginRequestDTO;
import com.example.businessserver.dtos.auth.SignUpEmployerRequestDTO;
import com.example.businessserver.dtos.auth.SignUpSubstituteRequestDTO;
import com.example.businessserver.exceptions.DTOException;
import com.example.businessserver.exceptions.DTOOutOfBoundsException;
import com.example.businessserver.logic.interfaces.AuthLogic;
import com.example.businessserver.services.implementations.AuthServiceImpl;
import com.example.businessserver.services.utils.JWTUtility;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.crypto.password.Pbkdf2PasswordEncoder;
import org.springframework.stereotype.Service;

import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.util.Arrays;
import java.util.regex.Pattern;

@Service
public class AuthLogicImpl extends LogicDaddy implements AuthLogic {
    @Autowired
    private JWTUtility jwtUtility;

    @Autowired
    private AuthServiceImpl userService;

    private Pbkdf2PasswordEncoder encoder = new Pbkdf2PasswordEncoder();

    private final Pattern emailPattern;

    public AuthLogicImpl() {
        //Taget fra geeksforgeeks      https://www.geeksforgeeks.org/check-email-address-valid-not-java/
        String emailRegex = "^[a-zA-Z0-9_+&*-]+(?:\\."+
                "[a-zA-Z0-9_+&*-]+)*@" +
                "(?:[a-zA-Z0-9-]+\\.)+[a-z" +
                "A-Z]{2,7}$";
        emailPattern = Pattern.compile(emailRegex);
    }

    @Override
    public JwtResponseDTO login(LoginRequestDTO loginRequest) throws DTOException {
        objectNullCheck(loginRequest, "loginRequest");
        checkEmail(loginRequest.getEmail());
        checkPassword(loginRequest.getPassword());
        UserDetails userDetails = userService.loadUserByUsername(loginRequest.getEmail());
        String password = userDetails.getPassword();
        return new JwtResponseDTO(jwtUtility.generateToken(userDetails));
    }

    @Override
    public void signUpEmployer(SignUpEmployerRequestDTO requestDTO) throws DTOException {
        objectNullCheck(requestDTO, "signUpRequest");
        checkEmail(requestDTO.getEmail());
        checkPassword(requestDTO.getPassword());
        checkStringMinimumValues(requestDTO.getFirstName(), "First name", 1);
        checkStringMinimumValues(requestDTO.getLastName(), "Last name", 1);
        checkStringMinimumValues(requestDTO.getTitle(), "Title", 1);
        checkStringMinimumValues(requestDTO.getWorkplace(), "Workplace", 1);
        userService.SignUpEmployer(requestDTO);
    }

    @Override
    public void signUpSubstitute(SignUpSubstituteRequestDTO requestDTO) throws DTOException {
        objectNullCheck(requestDTO, "signUpRequest");
        checkEmail(requestDTO.getEmail());
        checkPassword(requestDTO.getPassword());
        checkStringMinimumValues(requestDTO.getFirstName(), "First name", 1);
        checkStringMinimumValues(requestDTO.getLastName(), "Last name", 1);
        //TODO : Check dob når det implementeres
        checkBio(requestDTO.getBio());
        checkStringMinimumValues(requestDTO.getAddress(), "Address", 1);
        userService.SignUpSubstitute(requestDTO);
    }

    private void checkBio(String bio) throws DTOException { //TODO:Burde generalise de er string checks
        objectNullCheck(bio, "Bio");
        if (bio.length() > 300)
            throw new DTOOutOfBoundsException("Bio cant be over 300 characters");
    }

    public void checkPassword(String password) throws DTOException {
        objectNullCheck(password, "Password");
        checkStringMinimumValues(password, "Password", 6);
        char[] characters = password.toCharArray();
        for (int i = 0; i < characters.length; i++) {
            if (Character.isUpperCase(characters[i]))
                break;
            if (i == characters.length-1)
                throw new DTOOutOfBoundsException("Password needs at least one uppercase letter");
        }
    }

    public void checkEmail(String email) throws DTOException {
        objectNullCheck(email, "Email");
        if (!emailPattern.matcher(email).matches())
            throw new DTOOutOfBoundsException("Email does not follow the email standard");
    }


    public void checkStringMinimumValues(String name, String typeOfName, int min) throws DTOException{
        objectNullCheck(name, typeOfName);
        if(name.trim().length() < min)
            throw new DTOOutOfBoundsException(typeOfName + " has to be at least "+ min +" character long");
    }

    public void checkPasswordsForMatch(String savedPassword, String loginRequestPassword, String salt) throws DTOOutOfBoundsException {
        String requestWithSalt = generatePasswordWithKnownSalt(salt, loginRequestPassword);
        if (!savedPassword.equals(loginRequestPassword))
            throw new DTOOutOfBoundsException("Wrong Password");
    }

    //TODO der skal laves lidt om i hvordan kommunikation virker til database, skal også retunere salt som skal gemmes på database
    //https://www.baeldung.com/java-password-hashing
    public String generateHashedPassword(String password)
    {
        SecureRandom random = new SecureRandom();;
        byte[] salt = new byte[16];
        random.nextBytes(salt);
        return generatePasswordWithKnownSalt(salt.toString(), password);
    }

    public String generatePasswordWithKnownSalt(String salt, String password)
    {
        MessageDigest md;
        try {
            md = MessageDigest.getInstance("SHA-512");
        } catch (NoSuchAlgorithmException e) {
            throw new RuntimeException(e);
        }
        md.update(salt.getBytes());
        byte[] hashedPassword = md.digest(password.getBytes(StandardCharsets.UTF_8));
        return Arrays.toString(hashedPassword);
    }
}
