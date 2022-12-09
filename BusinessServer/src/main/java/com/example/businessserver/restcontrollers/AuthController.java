package com.example.businessserver.restcontrollers;

import com.example.businessserver.dtos.auth.JwtRequest;
import com.example.businessserver.dtos.auth.JwtResponse;
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
@CrossOrigin
public class AuthController {

	@Autowired
	private JWTUtility jwtUtility;

	@Autowired
	private AuthenticationManager authenticationManager;

	@Autowired
	private AuthServiceImpl userService;

	@GetMapping("/yo")
	public String test()
	{
		return "Numse";
	}

	@PostMapping("/authenticate")
	public JwtResponse authenticate(@RequestBody JwtRequest jwtRequest) throws Exception {
		try {
			authenticationManager.authenticate(
							new UsernamePasswordAuthenticationToken(
											jwtRequest.getUsername(),
											jwtRequest.getPassword()
							)
			);
		} catch (BadCredentialsException e) {
			throw new Exception("INVALID_CREDENTIALS", e);
		}

		UserDetails userDetails = userService.loadUserByUsername(jwtRequest.getUsername());

		String token = jwtUtility.generateToken(userDetails);

		return new JwtResponse(token);
	}
}
