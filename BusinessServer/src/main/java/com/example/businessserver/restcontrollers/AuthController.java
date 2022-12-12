package com.example.businessserver.restcontrollers;

import com.example.businessserver.dtos.auth.*;
import com.example.businessserver.exceptions.DTOException;
import com.example.businessserver.logic.interfaces.AuthLogic;
import com.example.businessserver.services.implementations.AuthServiceImpl;
import com.example.businessserver.services.utils.JWTUtility;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.web.bind.annotation.*;
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

	@PostMapping("/register/employer")
	public void registerEmployer(@RequestBody SignUpEmployerRequestDTO employerRequest) throws DTOException {
		authLogic.signUpEmployer(employerRequest);
	}

	@PostMapping("/register/substitute")
	public void registerSubstitute(@RequestBody SignUpSubstituteRequestDTO substituteRequest) throws DTOException {
		authLogic.signUpSubstitute(substituteRequest);
	}

	@GetMapping("/EmployerInfo/{id}{role}")
	public void getEmployerInformation(@PathVariable int id, @PathVariable LoginUserResponseDTO.Role role)
	{
		try {
			authLogic.getEmployerInfo(new GetEmployerInfoParamsDTO(id, role));
		} catch (DTOException e) {
			throw new RuntimeException(e);
		}
	}
}
