package com.example.businessserver;

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
	public User findById(@PathVariable long id) {
		String responseMessage = userService.receiveGreeting(String.valueOf(id));
		return new User(responseMessage);
	}

	@PostMapping
	public User createUser(@RequestBody User user) {
		// userDAO.createUser(user);
		System.out.println("User created: " + user);
		return new User(userService.receiveGreeting(user.getId()));
	}

}
