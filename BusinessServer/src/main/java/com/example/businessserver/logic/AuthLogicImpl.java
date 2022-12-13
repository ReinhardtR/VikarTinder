package com.example.businessserver.logic;

import com.example.businessserver.dtos.auth.*;
import com.example.businessserver.exceptions.DTOException;
import com.example.businessserver.exceptions.DTOOutOfBoundsException;
import com.example.businessserver.logic.interfaces.AuthLogic;
import com.example.businessserver.services.interfaces.AuthService;
import com.example.businessserver.services.utils.JWTUtility;
import com.example.businessserver.services.utils.UserSaltHolder;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.time.LocalDate;
import java.time.Period;
import java.util.Arrays;
import java.util.regex.Pattern;

@Service
public class AuthLogicImpl extends BasicLogic implements AuthLogic {
	private final Pattern emailPattern;
	private final int nameMinLength = 1;
	@Autowired
	private JWTUtility jwtUtility;
	@Autowired
	private AuthServiceImpl userService;

	public AuthLogicImpl() {
		//Taget fra geeksforgeeks      https://www.geeksforgeeks.org/check-email-address-valid-not-java/
		String emailRegex = "^[a-zA-Z0-9_+&*-]+(?:\\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,7}$";
		emailPattern = Pattern.compile(emailRegex);
	}

	@Override
	public JwtResponseDTO login(LoginRequestDTO loginRequest) throws DTOException {
		System.out.println(loginRequest.getEmail() + " " + loginRequest.getPassword());

		objectNullCheck(loginRequest, "loginRequest");
		checkEmail(loginRequest.getEmail());
		checkPassword(loginRequest.getPassword());

		UserSaltHolder userDetails = (UserSaltHolder) userService.loadUserByUsername(loginRequest.getEmail());
		String hashedPassword = userDetails.getPassword();
		System.out.println("HASHED: " + hashedPassword);

		checkPasswordsForMatch(hashedPassword, loginRequest.getPassword(), userDetails.getSalt());

		return new JwtResponseDTO(jwtUtility.generateToken(userDetails));
	}

    @Override
    public void signUpEmployer(SignUpEmployerRequestDTO requestDTO) throws DTOException {
        objectNullCheck(requestDTO, "signUpRequest");
        checkEmail(requestDTO.getEmail());
        checkPassword(requestDTO.getPassword());
        checkFirstName(requestDTO.getFirstName());
        checkLastName(requestDTO.getLastName());
        checkTitle(requestDTO.getTitle());
        checkWorkplace(requestDTO.getTitle());

        String[] saltHashedPassword = generateHashedPassword(requestDTO.getPassword());
        userService.SignUpEmployer(new SignUpWrapperEmployerDTO(saltHashedPassword[0], saltHashedPassword[1], requestDTO));
    }

    @Override
    public void signUpSubstitute(SignUpSubstituteRequestDTO requestDTO) throws DTOException {
        objectNullCheck(requestDTO, "signUpRequest");
        checkEmail(requestDTO.getEmail());
        checkPassword(requestDTO.getPassword());
        checkFirstName(requestDTO.getFirstName());
        checkLastName(requestDTO.getLastName());
        checkAge(requestDTO.getBirthDate().toLocalDate());
        checkBio(requestDTO.getBio());
        checkAddress(requestDTO.getAddress());

		String[] saltHashedPassword = generateHashedPassword(requestDTO.getPassword());
		userService.SignUpSubstitute(new SignUpWrapperSubstituteDTO(saltHashedPassword[0], saltHashedPassword[1], requestDTO));
	}

	@Override
	public EmployerInfoDTO getEmployerInfo(GetUserInfoParamsDTO getEmployerInfoParamsDTO) throws DTOException {
		objectNullCheck(getEmployerInfoParamsDTO, "employerInfoParams");
		// objectNullCheck(getEmployerInfoParamsDTO.getRole(), "Role");
		checkId(getEmployerInfoParamsDTO.getId());
		return userService.getEmployerInfo(getEmployerInfoParamsDTO);
	}

	@Override
	public SubstituteInfoDTO getSubstituteInfo(GetUserInfoParamsDTO getUserInfoParamsDTO) throws DTOException {
		objectNullCheck(getUserInfoParamsDTO, "substituteInfoParams");
		// objectNullCheck(getUserInfoParamsDTO.getRole(), "Role");
		checkId(getUserInfoParamsDTO.getId());
		return userService.getSubstituteInfo(getUserInfoParamsDTO);
	}

    @Override
    public void updateEmployerInfo(UpdateEmployerInfoDTO updateRequest) throws DTOException {
        objectNullCheck(updateRequest, "updateRequest");
        checkId(updateRequest.getId());

        EmployerInfoDTO updatedInfo = updateRequest.getUpdatedInfo();
        checkFirstName(updatedInfo.getFirstName());
        checkLastName(updatedInfo.getLastName());
        checkTitle(updatedInfo.getTitle());
        checkWorkplace(updatedInfo.getWorkplace());

        userService.updateEmployerInfo(updateRequest);
    }

    @Override
    public void updateSubstituteInfo(UpdateSubstituteInfoDTO updateRequest) throws DTOException {
        objectNullCheck(updateRequest, "updateRequest");
        checkId(updateRequest.getId());

        SubstituteInfoDTO updatedInfo = updateRequest.getUpdatedInfo();
        checkFirstName(updatedInfo.getFirstName());
        checkLastName(updatedInfo.getLastName());
        checkAge(updatedInfo.getBirthDate().toLocalDate());
        checkBio(updatedInfo.getBio());
        checkAddress(updatedInfo.getAddress());

        userService.updateSubstituteInfo(updateRequest);
    }

    private void checkAddress(String address) throws DTOException {
        String type = "Address";
        objectNullCheck(address, type);
        checkStringMinimumValues(address, type, 1);
    }

    private void checkWorkplace(String workplace) throws DTOException{
        String type = "Workplace";
        objectNullCheck(workplace, type);
        checkStringMinimumValues(workplace, type, 1);
    }

    private void checkTitle(String title) throws DTOException {
        String type = "Title";
        objectNullCheck(title, type);
        checkStringMinimumValues(title, type, 1);
    }

    private void checkLastName(String lastName) throws DTOException {
        String type = "Lastname";
        objectNullCheck(lastName, type);
        checkStringMinimumValues(lastName, type, nameMinLength);
    }

    private void checkFirstName(String firstName) throws DTOException {
        String type = "Firstname";
        objectNullCheck(firstName, type);
        checkStringMinimumValues(firstName, type, nameMinLength);
    }

    public void checkAge(LocalDate dob) throws DTOOutOfBoundsException {
        int yearsDifference = Period.between(dob, LocalDate.now()).getYears();
        if (yearsDifference < 18 || yearsDifference > 99)
            throw new DTOOutOfBoundsException("User has to be older than 18 and less than 100: DOB given: " + dob);
    }

	public void checkBio(String bio) throws DTOException {
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
			if (i == characters.length - 1)
				throw new DTOOutOfBoundsException("Password needs at least one uppercase letter");
		}
	}

	public void checkEmail(String email) throws DTOException {
		objectNullCheck(email, "Email");
		if (!emailPattern.matcher(email).matches())
			throw new DTOOutOfBoundsException("Email does not follow the email standard");
	}

    //TODO : Den kan trimme mellemrummene mellem ord/navne. DTO'erne burde få disse rettede strings tilbage
    public void checkStringMinimumValues(String string, String typeOfName, int minLength) throws DTOException{
        string = string.replaceAll("( )+", " "); //Efterlader kun et mellemrum efter ord

		if (string.trim().length() < minLength)
			throw new DTOOutOfBoundsException(typeOfName + " has to be at least " + minLength + " character long");
	}

	public void checkPasswordsForMatch(String savedPassword, String loginRequestPassword, String salt) throws DTOOutOfBoundsException {
		String requestWithSalt = generatePasswordWithKnownSalt(salt, loginRequestPassword);
		System.out.println("SALT: " + salt);
		System.out.println("REQUEST WITH SALT: " + requestWithSalt);
		if (!savedPassword.equals(requestWithSalt))
			throw new DTOOutOfBoundsException("Wrong Password");
	}

    //https://www.baeldung.com/java-password-hashing
    public String[] generateHashedPassword(String password) { //Returnerer string array, [0] = salt, [1] = hashedPassword
        SecureRandom random = new SecureRandom();
        byte[] salt = new byte[16];
        random.nextBytes(salt);
        String saltString = Arrays.toString(salt);
        String hashedPassword = generatePasswordWithKnownSalt(saltString, password);
        return new String[]{saltString, hashedPassword};
    }

	public String generatePasswordWithKnownSalt(String salt, String password) {
		MessageDigest digest;
		try {
			digest = MessageDigest.getInstance("SHA-512");
		} catch (NoSuchAlgorithmException e) {
			throw new RuntimeException(e);
		}
		digest.update(salt.getBytes());
		byte[] hashedPassword = digest.digest(password.getBytes(StandardCharsets.UTF_8));
		return Arrays.toString(hashedPassword);
	}
}
