package com.example.businessserver.restcontrollers;

import com.example.businessserver.dtos.SubstituteDTO;
import com.example.businessserver.services.UserServiceImpl;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/tinder")
@CrossOrigin
public class UserController {

	@Autowired
	private UserDAO userDAO;

	@Autowired
	private UserServiceImpl userService;

	@GetMapping("/{id}")
	public SubstituteDTO findById(@PathVariable long id) {
		String responseMessage = userService.receiveGreeting(String.valueOf(id));
		return new SubstituteDTO(responseMessage);
	}

	@PostMapping
	public SubstituteDTO createUser(@RequestBody SubstituteDTO user) {
		// userDAO.createUser(user);
		System.out.println("User created: " + user);
		return new SubstituteDTO(userService.receiveGreeting(user.getId()));
	}

}
