package com.example.businessserver.restcontrollers;

import com.example.businessserver.dtos.auth.*;
import com.example.businessserver.exceptions.DTOException;
import com.example.businessserver.logic.interfaces.AuthLogic;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
// TODO : Refactor navngivning til gruppens standard

@RestController
@RequestMapping("/auth")
public class AuthController {
	@Autowired
	private AuthLogic authLogic;

	@PostMapping("/login")
	public JwtResponseDTO authenticate(@RequestBody LoginRequestDTO loginRequest) throws Exception {
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

	@GetMapping("/EmployerInfo/{id}")
	public EmployerInfoDTO getEmployerInformation(@PathVariable int id)
	{
		try {
			return authLogic.getEmployerInfo(new GetUserInfoParamsDTO(id, LoginUserResponseDTO.Role.EMPLOYER));
		} catch (DTOException e) {
			throw new RuntimeException(e);
		}
	}

	@PostMapping("/EmployerInfo")
	public void updateEmployerInformation(@RequestBody UpdateEmployerInfoDTO updateRequest)
	{
		try {
			authLogic.updateEmployerInfo(updateRequest);
		} catch (DTOException e) {
			throw new RuntimeException(e);
		}
	}

	@GetMapping("/SubstituteInfo/{id}")
	public SubstituteInfoDTO getSubstituteInformation(@PathVariable int id)
	{
		try {
			return authLogic.getSubstituteInfo(new GetUserInfoParamsDTO(id, LoginUserResponseDTO.Role.SUBSTITUTE));
		} catch (DTOException e) {
			throw new RuntimeException(e);
		}
	}

	@PostMapping("/SubstituteInfo")
	public void updateSubstituteInformation(@RequestBody UpdateSubstituteInfoDTO updateRequest)
	{
		try {
			authLogic.updateSubstituteInfo(updateRequest);
		} catch (DTOException e) {
			throw new RuntimeException(e);
		}
	}

	@DeleteMapping("/User/{id}{role}")
	public void deleteUser(@PathVariable int id, @PathVariable LoginUserResponseDTO.Role role)
	{
		try {
			authLogic.deleteUser(new DeleteRequestDTO(id, role));
		} catch (DTOException e) {
			throw new RuntimeException(e);
		}
	}
}
