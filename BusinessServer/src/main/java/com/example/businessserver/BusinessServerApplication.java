package com.example.businessserver;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
public class BusinessServerApplication {

	public static void main(String[] args) {
		SpringApplication.run(BusinessServerApplication.class, args);
	}

	@Bean
	UserDAO userDAO()
	{
		return new UserDAO();
	}
}
