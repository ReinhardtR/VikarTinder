package com.example.businessserver.restcontrollers;

import com.example.businessserver.dtos.auth.JwtResponseDTO;
import com.example.businessserver.dtos.auth.LoginRequestDTO;
import com.example.businessserver.dtos.auth.SignUpEmployerRequestDTO;
import com.example.businessserver.dtos.auth.SignUpSubstituteRequestDTO;
import com.example.businessserver.exceptions.DTOException;
import com.example.businessserver.logic.interfaces.AuthLogic;
import com.example.businessserver.services.implementations.AuthServiceImpl;
import com.example.businessserver.services.utils.JWTUtility;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
// TODO : Refactor navngivning til gruppens standard

@RestController
@RequestMapping("/auth")
public class AuthController {

	@Autowired
	private JWTUtility jwtUtility;

	@Autowired
	private AuthenticationManager authenticationManager;

	@Autowired
	private AuthServiceImpl userService;

	@Autowired
	private AuthLogic authLogic;

	@PostMapping("/login")
	public JwtResponseDTO authenticate(@RequestBody LoginRequestDTO loginRequest) throws Exception {
		//TODO:Ved ikke endnu om denne lille del er nødvendig. Behold den til vi har checked om ROLE lock af metoder virker. BT ser det ud til at virke uden. Måske validater på return
		//TODO: Hvis det er nødvendig, ved ikke om den burde rykkes til logik da password skal hashes
		/*try {
			authenticationManager.authenticate(
							new UsernamePasswordAuthenticationToken(
											loginRequest.getEmail(),
											loginRequest.getPassword()
							)
			);
		} catch (BadCredentialsException e) {
			throw new Exception("INVALID_CREDENTIALS", e);
		}*/

		return authLogic.login(loginRequest);
	}

	@PostMapping("/register")
	public void registerEmployer(@RequestBody SignUpEmployerRequestDTO employerRequest) throws DTOException {
		authLogic.signUpEmployer(employerRequest);
	}

	@PostMapping("/register")
	public void registerEmployer(@RequestBody SignUpSubstituteRequestDTO substituteRequest) throws DTOException {
		authLogic.signUpSubstitute(substituteRequest);
	}
}
