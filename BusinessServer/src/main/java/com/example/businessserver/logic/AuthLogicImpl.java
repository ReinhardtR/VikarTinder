package com.example.businessserver.logic;

import com.example.businessserver.dtos.auth.JwtResponseDTO;
import com.example.businessserver.dtos.auth.LoginRequestDTO;
import com.example.businessserver.exceptions.DTOException;
import com.example.businessserver.exceptions.DTOOutOfBoundsException;
import com.example.businessserver.logic.interfaces.AuthLogic;
import com.example.businessserver.services.implementations.AuthServiceImpl;
import com.example.businessserver.services.utils.JWTUtility;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.UserDetails;
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

    public void checkPassword(String password) throws DTOException {
        objectNullCheck(password, "Password");
        if (password.length() < 6)
            throw new DTOOutOfBoundsException("Password is too short, min 6 chars long");
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
        MessageDigest md;
        try {
            md = MessageDigest.getInstance("SHA-512");
        } catch (NoSuchAlgorithmException e) {
            throw new RuntimeException(e);
        }
        md.update(salt);
        byte[] hashedPassword = md.digest(password.getBytes(StandardCharsets.UTF_8));
        return Arrays.toString(hashedPassword);
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
