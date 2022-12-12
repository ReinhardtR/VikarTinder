package com.example.businessserver.restcontrollers;

import com.example.businessserver.dtos.auth.LoginRequestDTO;
import com.example.businessserver.dtos.auth.JwtResponseDTO;
import com.example.businessserver.logic.interfaces.AuthLogic;
import com.example.businessserver.services.implementations.AuthServiceImpl;
import com.example.businessserver.services.utils.JWTUtility;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.userdetails.UserDetails;
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
}
