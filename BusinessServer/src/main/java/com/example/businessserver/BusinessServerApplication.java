package com.example.businessserver;

import com.example.businessserver.restcontrollers.config.RsaKeyProperties;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
@EnableConfigurationProperties(RsaKeyProperties.class)
public class BusinessServerApplication {

	public static void main(String[] args) {
		SpringApplication.run(BusinessServerApplication.class, args);
	}


}
